
using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Windows.Forms; // Cần cho MessageBox (hoặc tách riêng logic thông báo)
using WordVaultAppMVC.Data; // Để dùng DatabaseContext

namespace WordVaultAppMVC.Services // Hoặc WordVaultAppMVC.Data
{
    public static class DataService // Dùng static cho tiện nếu không cần quản lý trạng thái
    {
        /// <summary>
        /// Thực hiện sao lưu cơ sở dữ liệu vào đường dẫn được chỉ định.
        /// </summary>
        /// <param name="backupFilePath">Đường dẫn đầy đủ đến file .bak để lưu.</param>
        /// <returns>True nếu thành công, False nếu thất bại.</returns>
        public static bool BackupDatabase(string backupFilePath)
        {
            if (string.IsNullOrWhiteSpace(backupFilePath)) return false;

            SqlConnection conn = null;
            try
            {
                conn = DatabaseContext.GetConnection();
                conn.Open();

                // Lấy tên Database từ connection hiện tại
                string dbName = conn.Database;
                if (string.IsNullOrWhiteSpace(dbName))
                {
                    throw new Exception("Không thể xác định tên cơ sở dữ liệu từ chuỗi kết nối.");
                }

                // Câu lệnh T-SQL để sao lưu
                // Lưu ý: WITH FORMAT sẽ ghi đè file backup cũ nếu tồn tại
                string backupCommand = $"BACKUP DATABASE [{dbName}] TO DISK = @backupPath WITH FORMAT, NAME = N'{dbName}-Full Database Backup', DESCRIPTION = N'Full backup of {dbName} database';";

                Debug.WriteLine($"[DataService] Executing Backup: {backupCommand} to {backupFilePath}");

                // Chạy lệnh backup (có thể cần chạy trên master DB tùy cấu hình server)
                // Thử chạy trên connection hiện tại trước
                using (SqlCommand cmd = new SqlCommand(backupCommand, conn))
                {
                    cmd.Parameters.AddWithValue("@backupPath", backupFilePath);
                    cmd.CommandTimeout = 300; // Tăng timeout cho backup (5 phút)
                    cmd.ExecuteNonQuery();
                }
                Debug.WriteLine($"[DataService] Backup successful to {backupFilePath}");
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[DataService] Backup Error: {ex.ToString()}");
                MessageBox.Show($"Lỗi khi sao lưu cơ sở dữ liệu:\n{ex.Message}\n\nKiểm tra quyền hạn ghi file của SQL Server Service Account và đường dẫn sao lưu.", "Lỗi Sao lưu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                if (conn?.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        /// <summary>
        /// Thực hiện phục hồi cơ sở dữ liệu từ file backup.
        /// !!! CẢNH BÁO: Thao tác này rất nguy hiểm, sẽ ghi đè dữ liệu hiện tại !!!
        /// </summary>
        /// <param name="backupFilePath">Đường dẫn đầy đủ đến file .bak để phục hồi.</param>
        /// <returns>True nếu thành công, False nếu thất bại.</returns>
        public static bool RestoreDatabase(string backupFilePath)
        {
            if (string.IsNullOrWhiteSpace(backupFilePath)) return false;

            SqlConnection conn = null;
            string dbName = ""; // Cần lấy tên DB

            try
            {
                // Kết nối đến master để thực hiện restore
                conn = DatabaseContext.GetConnection(); // Lấy connection string gốc
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(conn.ConnectionString);
                dbName = builder.InitialCatalog; // Lấy tên DB từ connection string
                builder.InitialCatalog = "master"; // Chuyển sang master
                string masterConnStr = builder.ConnectionString;

                using (conn = new SqlConnection(masterConnStr))
                {
                    conn.Open();

                    if (string.IsNullOrWhiteSpace(dbName))
                    {
                        throw new Exception("Không thể xác định tên cơ sở dữ liệu để phục hồi.");
                    }

                    // 1. Đưa DB về Single User Mode để đảm bảo không ai đang kết nối
                    string singleUserCmd = $"ALTER DATABASE [{dbName}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;";
                    Debug.WriteLine($"[DataService] Setting DB to Single User: {singleUserCmd}");
                    using (SqlCommand cmdSU = new SqlCommand(singleUserCmd, conn))
                    {
                        cmdSU.ExecuteNonQuery();
                        Debug.WriteLine($"[DataService] Database '{dbName}' set to SINGLE_USER.");
                    }

                    // Delay nhỏ để đảm bảo các kết nối cũ đã bị ngắt
                    System.Threading.Thread.Sleep(1000);

                    // 2. Thực hiện Restore
                    // Lưu ý: WITH REPLACE sẽ ghi đè DB hiện có
                    string restoreCommand = $"RESTORE DATABASE [{dbName}] FROM DISK = @backupPath WITH REPLACE;";
                    Debug.WriteLine($"[DataService] Executing Restore: {restoreCommand} from {backupFilePath}");
                    using (SqlCommand cmdRestore = new SqlCommand(restoreCommand, conn))
                    {
                        cmdRestore.Parameters.AddWithValue("@backupPath", backupFilePath);
                        cmdRestore.CommandTimeout = 600; // Tăng timeout cho restore (10 phút)
                        cmdRestore.ExecuteNonQuery();
                        Debug.WriteLine($"[DataService] Restore successful from {backupFilePath}");
                    }

                    // 3. Đưa DB về Multi User Mode
                    string multiUserCmd = $"ALTER DATABASE [{dbName}] SET MULTI_USER;";
                    Debug.WriteLine($"[DataService] Setting DB back to Multi User: {multiUserCmd}");
                    using (SqlCommand cmdMU = new SqlCommand(multiUserCmd, conn))
                    {
                        cmdMU.ExecuteNonQuery();
                        Debug.WriteLine($"[DataService] Database '{dbName}' set back to MULTI_USER.");
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[DataService] Restore Error: {ex.ToString()}");
                MessageBox.Show($"Lỗi nghiêm trọng khi phục hồi cơ sở dữ liệu:\n{ex.Message}\n\nKiểm tra quyền hạn restore, đường dẫn file backup, và đảm bảo không có kết nối nào khác đến CSDL.", "Lỗi Phục hồi", MessageBoxButtons.OK, MessageBoxIcon.Error);

                // CỐ GẮNG đưa về Multi-User nếu bị lỗi giữa chừng
                try
                {
                    if (!string.IsNullOrWhiteSpace(dbName))
                    {
                        SqlConnection emergencyConn = null;
                        try
                        {
                            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(DatabaseContext.GetConnection().ConnectionString);
                            builder.InitialCatalog = "master";
                            emergencyConn = new SqlConnection(builder.ConnectionString);
                            emergencyConn.Open();
                            string multiUserCmd = $"ALTER DATABASE [{dbName}] SET MULTI_USER;";
                            using (SqlCommand cmdMU = new SqlCommand(multiUserCmd, emergencyConn))
                            {
                                cmdMU.ExecuteNonQuery();
                                Debug.WriteLine($"[DataService] Attempted to set DB '{dbName}' back to MULTI_USER after error.");
                            }
                        }
                        catch (Exception innerEx) { Debug.WriteLine($"[DataService] Failed to set DB back to MULTI_USER after error: {innerEx.Message}"); }
                        finally { if (emergencyConn?.State == ConnectionState.Open) emergencyConn.Close(); }
                    }
                }
                catch { }

                return false;
            }
            // Không cần finally đóng connection vì đã dùng using
        }

        /// <summary>
        /// Xóa dữ liệu trong các bảng LearningStatuses và QuizResults.
        /// </summary>
        /// <returns>True nếu thành công, False nếu có lỗi.</returns>
        public static bool ClearLearningData()
        {
            SqlConnection conn = null;
            SqlTransaction transaction = null; // Dùng transaction để đảm bảo cả 2 lệnh cùng thành công hoặc thất bại
            try
            {
                conn = DatabaseContext.GetConnection();
                conn.Open();
                transaction = conn.BeginTransaction();

                string deleteLearningCmd = "DELETE FROM dbo.LearningStatuses;"; // Xóa tất cả trạng thái học
                string deleteQuizCmd = "DELETE FROM dbo.QuizResults;";       // Xóa tất cả kết quả quiz

                Debug.WriteLine("[DataService] Executing: " + deleteLearningCmd);
                using (SqlCommand cmdL = new SqlCommand(deleteLearningCmd, conn, transaction))
                {
                    cmdL.ExecuteNonQuery();
                }

                Debug.WriteLine("[DataService] Executing: " + deleteQuizCmd);
                using (SqlCommand cmdQ = new SqlCommand(deleteQuizCmd, conn, transaction))
                {
                    cmdQ.ExecuteNonQuery();
                }

                transaction.Commit(); // Hoàn tất transaction nếu không có lỗi
                Debug.WriteLine("[DataService] Learning data cleared successfully.");
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[DataService] Error clearing learning data: {ex.Message}");
                try
                {
                    transaction?.Rollback(); // Hoàn tác transaction nếu có lỗi
                }
                catch (Exception rollbackEx) { Debug.WriteLine($"[DataService] Rollback failed: {rollbackEx.Message}"); }

                MessageBox.Show($"Lỗi khi xóa dữ liệu học tập: {ex.Message}", "Lỗi Xóa Dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                if (conn?.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }
    }
}