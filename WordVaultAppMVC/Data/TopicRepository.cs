using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics; // Thêm using cho Debug
using WordVaultAppMVC.Models;

namespace WordVaultAppMVC.Data
{
    public class TopicRepository
    {
        // Lấy tất cả chủ đề từ bảng Topics
        public List<Topic> GetAllTopics()
        {
            List<Topic> topics = new List<Topic>();
            SqlConnection conn = null; // Khai báo ngoài để dùng trong finally

            try
            {
                conn = DatabaseContext.GetConnection();
                conn.Open();
                string query = "SELECT Id, Name FROM Topics ORDER BY Name"; // Thêm ORDER BY
                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        topics.Add(new Topic
                        {
                            Id = (int)reader["Id"],
                            Name = reader["Name"].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error in GetAllTopics: " + ex.Message);
                // Cân nhắc ném lại lỗi hoặc trả về danh sách rỗng tùy logic ứng dụng
                // throw;
            }
            finally
            {
                if (conn?.State == ConnectionState.Open) // Kiểm tra null và trạng thái
                {
                    conn.Close();
                }
            }
            return topics;
        }

        // Thêm một chủ đề mới vào bảng Topics
        public void AddTopic(string topicName)
        {
            if (string.IsNullOrWhiteSpace(topicName))
            {
                // Có thể ném ArgumentNullException hoặc xử lý khác
                Debug.WriteLine("Error in AddTopic: topicName is null or empty.");
                return;
            }

            SqlConnection conn = null;
            try
            {
                conn = DatabaseContext.GetConnection();
                conn.Open();
                // Kiểm tra xem topic đã tồn tại chưa (tránh trùng lặp) - Tùy chọn
                // string checkQuery = "SELECT COUNT(*) FROM Topics WHERE Name = @Name";
                // using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn)) { ... }

                string query = "INSERT INTO Topics (Name) OUTPUT INSERTED.Id VALUES (@Name)"; // Thêm OUTPUT nếu muốn lấy ID topic mới
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 100).Value = topicName.Trim(); // Trim tên topic
                    // object newId = cmd.ExecuteScalar(); // Lấy ID nếu cần
                    cmd.ExecuteNonQuery(); // Hoặc chỉ thực thi nếu không cần ID
                    Debug.WriteLine($"Added new topic: {topicName}");
                }
            }
            catch (SqlException sqlEx) // Bắt lỗi SQL cụ thể (ví dụ: trùng tên nếu có UNIQUE constraint)
            {
                Debug.WriteLine($"SQL Error in AddTopic for '{topicName}': {sqlEx.Message}");
                // Ném lại hoặc thông báo lỗi tùy logic
                // throw;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in AddTopic for '{topicName}': {ex.Message}");
                // throw;
            }
            finally
            {
                if (conn?.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        // --- PHƯƠNG THỨC MỚI ĐỂ XÓA LIÊN KẾT TỪ KHỎI CHỦ ĐỀ ---
        /// <summary>
        /// Xóa liên kết giữa một từ vựng và một chủ đề khỏi bảng VocabularyTopic.
        /// </summary>
        /// <param name="vocabularyId">ID của từ vựng.</param>
        /// <param name="topicId">ID của chủ đề.</param>
        /// <returns>True nếu xóa thành công (ít nhất 1 dòng bị ảnh hưởng), False nếu không.</returns>
        public bool RemoveWordFromTopic(int vocabularyId, int topicId)
        {
            if (vocabularyId <= 0 || topicId <= 0)
            {
                Debug.WriteLine("Error in RemoveWordFromTopic: Invalid vocabularyId or topicId.");
                return false;
            }

            int rowsAffected = 0;
            SqlConnection conn = null;
            try
            {
                conn = DatabaseContext.GetConnection();
                conn.Open();
                string query = "DELETE FROM VocabularyTopic WHERE VocabularyId = @VocabularyId AND TopicId = @TopicId";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add("@VocabularyId", SqlDbType.Int).Value = vocabularyId;
                    cmd.Parameters.Add("@TopicId", SqlDbType.Int).Value = topicId;

                    rowsAffected = cmd.ExecuteNonQuery(); // Thực thi lệnh DELETE và lấy số dòng bị ảnh hưởng
                    Debug.WriteLine($"Attempted to remove VocabID={vocabularyId} from TopicID={topicId}. Rows affected: {rowsAffected}");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in RemoveWordFromTopic (VocabID={vocabularyId}, TopicID={topicId}): {ex.Message}");
                return false; // Trả về false nếu có lỗi
            }
            finally
            {
                if (conn?.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }

            // Trả về true nếu có ít nhất một dòng đã bị xóa
            return rowsAffected > 0;
        }

        // Bạn có thể bổ sung thêm các phương thức UpdateTopic, DeleteTopic (xóa hẳn chủ đề) nếu cần
    }
}