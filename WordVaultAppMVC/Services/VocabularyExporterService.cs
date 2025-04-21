#region Using Directives
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Configuration;
using WordVaultAppMVC.Models; // Namespace của Vocabulary model
using System.Text;
using System.Windows.Forms; // Cần cho MessageBox
#endregion

namespace WordVaultAppMVC.Services
{
    /// <summary>
    /// Cung cấp chức năng xuất dữ liệu từ vựng ra file.
    /// </summary>
    public static class VocabularyExporterService // Dùng static class cho tiện
    {
        #region Constants
        private const string VocabularyTableName = "Vocabulary";
        private const string ColId = "Id";
        private const string ColWord = "Word";
        private const string ColMeaning = "Meaning";
        private const string ColPronunciation = "Pronunciation";
        private const string ColAudioUrl = "AudioUrl";
        private const string OutputFileName = "vocabulary_export.txt";
        #endregion

        #region Public Export Method
        /// <summary>
        /// Thực hiện toàn bộ quá trình: lấy dữ liệu và xuất ra file TXT.
        /// Hiển thị thông báo cho người dùng.
        /// </summary>
        public static void ExportAllVocabularyToFile()
        {
            string connectionString = GetConnectionString();
            if (string.IsNullOrEmpty(connectionString))
            {
                MessageBox.Show("LỖI: Không tìm thấy hoặc chuỗi kết nối 'WordVaultDb' không hợp lệ trong App.config.",
                                "Lỗi Cấu hình", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Debug.WriteLine("[Exporter] Đang lấy dữ liệu từ cơ sở dữ liệu...");
            List<Vocabulary> vocabularyList = GetAllVocabularyData(connectionString);

            if (vocabularyList == null)
            {
                // Lỗi đã được log, chỉ cần thông báo chung
                MessageBox.Show("Đã xảy ra lỗi khi lấy dữ liệu từ vựng. Vui lòng kiểm tra log.",
                               "Lỗi Dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (vocabularyList.Count == 0)
            {
                MessageBox.Show("Không tìm thấy dữ liệu từ vựng nào để xuất.",
                               "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                Debug.WriteLine($"[Exporter] Tìm thấy {vocabularyList.Count} từ vựng. Đang chuẩn bị xuất ra file...");
                WriteDataToFile(vocabularyList); // Gọi hàm ghi file
            }
            Debug.WriteLine("[Exporter] Quá trình xuất kết thúc.");
        }
        #endregion

        #region Data Access (Private)
        private static string GetConnectionString()
        {
            try
            {
                ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings["WordVaultDb"];
                if (settings != null && !string.IsNullOrWhiteSpace(settings.ConnectionString))
                {
                    return settings.ConnectionString;
                }
                Debug.WriteLine("[Exporter] Connection string 'WordVaultDb' not found or empty in App.config.");
                return null;
            }
            catch (ConfigurationErrorsException ex)
            {
                Debug.WriteLine($"[Exporter] Configuration Error: {ex}");
                MessageBox.Show($"LỖI cấu hình App.config: {ex.Message}", "Lỗi Cấu hình", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            catch (Exception ex) // Bắt các lỗi khác
            {
                Debug.WriteLine($"[Exporter] GetConnectionString General Error: {ex}");
                MessageBox.Show($"Lỗi không mong muốn khi đọc cấu hình: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        private static List<Vocabulary> GetAllVocabularyData(string connectionString)
        {
            List<Vocabulary> vocabularies = new List<Vocabulary>();
            string query = $"SELECT {ColId}, {ColWord}, {ColMeaning}, {ColPronunciation}, {ColAudioUrl} FROM {VocabularyTableName}";
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Vocabulary vocab = MapReaderToVocabulary(reader);
                            if (vocab != null) vocabularies.Add(vocab);
                        }
                    }
                }
                return vocabularies;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[Exporter] DB Error: {ex}");
                MessageBox.Show($"LỖI khi truy cập cơ sở dữ liệu: {ex.Message}", "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        private static Vocabulary MapReaderToVocabulary(SqlDataReader reader)
        {
            try
            {
                int idOrdinal = reader.GetOrdinal(ColId);
                int wordOrdinal = reader.GetOrdinal(ColWord);
                int meaningOrdinal = reader.GetOrdinal(ColMeaning);
                int pronunciationOrdinal = reader.GetOrdinal(ColPronunciation);
                int audioUrlOrdinal = reader.GetOrdinal(ColAudioUrl);
                return new Vocabulary
                { /* ... mapping như cũ ... */
                    Id = reader.IsDBNull(idOrdinal) ? 0 : reader.GetInt32(idOrdinal),
                    Word = reader.IsDBNull(wordOrdinal) ? string.Empty : reader.GetString(wordOrdinal),
                    Meaning = reader.IsDBNull(meaningOrdinal) ? string.Empty : reader.GetString(meaningOrdinal),
                    Pronunciation = reader.IsDBNull(pronunciationOrdinal) ? string.Empty : reader.GetString(pronunciationOrdinal),
                    AudioUrl = reader.IsDBNull(audioUrlOrdinal) ? string.Empty : reader.GetString(audioUrlOrdinal)
                };
            }
            catch (Exception ex) { Debug.WriteLine($"[Exporter] Mapping Error: {ex.Message}"); return null; }
        }
        #endregion

        #region File Export (Private)
        private static void WriteDataToFile(List<Vocabulary> vocabularyList)
        {
            string outputDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string filePath = Path.Combine(outputDirectory, OutputFileName);
            Debug.WriteLine($"[Exporter] Chuẩn bị ghi vào file: {filePath}");
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath, false, Encoding.UTF8))
                {
                    writer.WriteLine($"{ColId}\t{ColWord}\t{ColMeaning}\t{ColPronunciation}\t{ColAudioUrl}");
                    foreach (var vocab in vocabularyList)
                    {
                        string line = $"{vocab.Id}\t{EscapeTsvField(vocab.Word)}\t{EscapeTsvField(vocab.Meaning)}\t{EscapeTsvField(vocab.Pronunciation)}\t{EscapeTsvField(vocab.AudioUrl)}";
                        writer.WriteLine(line);
                    }
                }
                MessageBox.Show($"Xuất dữ liệu thành công! ({vocabularyList.Count} từ)\nFile đã được lưu tại:\n{filePath}",
                                "Xuất Hoàn tất", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) // Bắt các lỗi IO, UnauthorizedAccess, etc.
            {
                Debug.WriteLine($"[Exporter] File Write Error: {ex}");
                MessageBox.Show($"LỖI khi ghi file: {ex.Message}\nĐường dẫn: {filePath}\n\nHãy kiểm tra quyền ghi và file có đang được mở không.",
                               "Lỗi Ghi File", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static string EscapeTsvField(string value)
        {
            if (string.IsNullOrEmpty(value)) return string.Empty;
            return value.Replace('\t', ' ').Replace("\r\n", " ").Replace('\n', ' ').Replace('\r', ' ');
        }
        #endregion
    }
}