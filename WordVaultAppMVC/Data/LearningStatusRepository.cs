using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using WordVaultAppMVC.Models;

namespace WordVaultAppMVC.Data
{
    public class LearningStatusRepository
    {
        // Lấy trạng thái học của từ vựng theo WordId
        public LearningStatus GetLearningStatusByWordId(string wordId)
        {
            LearningStatus status = null;

            try
            {
                using (SqlConnection conn = DatabaseContext.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT * FROM LearningStatuses WHERE WordId = @WordId";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.Add("@WordId", SqlDbType.NVarChar, 50).Value = wordId;
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                status = new LearningStatus
                                {
                                    Id = reader["Id"] != DBNull.Value ? (int)reader["Id"] : 0,
                                    WordId = reader["WordId"].ToString(),
                                    UserId = reader["UserId"].ToString(),
                                    Status = reader["Status"].ToString(),
                                    DateLearned = reader["DateLearned"] == DBNull.Value ? DateTime.MinValue : (DateTime)reader["DateLearned"]
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi lấy LearningStatus: {ex.Message}");
            }

            return status;
        }


        // Thêm trạng thái học mới vào cơ sở dữ liệu
        public void AddLearningStatus(LearningStatus status)
        {
            try
            {
                using (SqlConnection conn = DatabaseContext.GetConnection())
                {
                    conn.Open();
                    string query = @"INSERT INTO LearningStatuses (WordId, UserId, Status, DateLearned)
                                     VALUES (@WordId, @UserId, @Status, @DateLearned)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.Add("@WordId", SqlDbType.NVarChar, 50).Value = status.WordId;
                        cmd.Parameters.Add("@UserId", SqlDbType.NVarChar, 50).Value = status.UserId;
                        cmd.Parameters.Add("@Status", SqlDbType.NVarChar, 50).Value = status.Status;
                        cmd.Parameters.Add("@DateLearned", SqlDbType.DateTime).Value = status.DateLearned;

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi hoặc ghi log nếu cần
                Console.WriteLine($"Lỗi khi thêm LearningStatus: {ex.Message}");
            }
        }

        // Cập nhật trạng thái học hiện có
        public void UpdateLearningStatus(LearningStatus status)
        {
            try
            {
                using (SqlConnection conn = DatabaseContext.GetConnection())
                {
                    conn.Open();
                    string query = @"UPDATE LearningStatuses 
                                     SET UserId = @UserId, Status = @Status, DateLearned = @DateLearned 
                                     WHERE WordId = @WordId";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.Add("@UserId", SqlDbType.NVarChar, 50).Value = status.UserId;
                        cmd.Parameters.Add("@Status", SqlDbType.NVarChar, 50).Value = status.Status;
                        cmd.Parameters.Add("@DateLearned", SqlDbType.DateTime).Value = status.DateLearned;
                        cmd.Parameters.Add("@WordId", SqlDbType.NVarChar, 50).Value = status.WordId;

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi hoặc ghi log nếu cần
                Console.WriteLine($"Lỗi khi cập nhật LearningStatus: {ex.Message}");
            }
        }

        // Lấy danh sách tất cả trạng thái học
        public List<LearningStatus> GetAllLearningStatus()
        {
            List<LearningStatus> statuses = new List<LearningStatus>();

            try
            {
                using (SqlConnection conn = DatabaseContext.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT * FROM LearningStatuses";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            statuses.Add(new LearningStatus
                            {
                                Id = (int)reader["Id"],
                                WordId = reader["WordId"].ToString(),
                                UserId = reader["UserId"].ToString(),
                                Status = reader["Status"].ToString(),
                                DateLearned = reader["DateLearned"] == DBNull.Value ? DateTime.MinValue : (DateTime)reader["DateLearned"]
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi hoặc ghi log nếu cần
                Console.WriteLine($"Lỗi khi lấy danh sách LearningStatus: {ex.Message}");
            }

            return statuses;
        }
    }
}
