using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using WordVaultAppMVC.Models;

namespace WordVaultAppMVC.Data
{
    public class TopicRepository
    {
        // Lấy tất cả chủ đề từ bảng Topics
        public List<Topic> GetAllTopics()
        {
            List<Topic> topics = new List<Topic>();

            try
            {
                using (SqlConnection conn = DatabaseContext.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT * FROM Topics";
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
            }
            catch (Exception ex)
            {
                // Log lỗi hoặc xử lý ngoại lệ theo yêu cầu
                Console.WriteLine("Error in GetAllTopics: " + ex.Message);
            }

            return topics;
        }

        // Thêm một chủ đề mới vào bảng Topics
        public void AddTopic(string topicName)
        {
            try
            {
                using (SqlConnection conn = DatabaseContext.GetConnection())
                {
                    conn.Open();
                    string query = "INSERT INTO Topics (Name) VALUES (@Name)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 100).Value = topicName;
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                // Log lỗi hoặc xử lý ngoại lệ theo yêu cầu
                Console.WriteLine("Error in AddTopic: " + ex.Message);
            }
        }

        // Bạn có thể bổ sung thêm các phương thức UpdateTopic, DeleteTopic nếu cần
    }
}
