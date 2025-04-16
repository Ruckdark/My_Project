using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using WordVaultAppMVC.Models;

namespace WordVaultAppMVC.Data
{
    public class VocabularyRepository
    {
        // Constants for column and table names
        private const string TableName = "Vocabulary";
        private const string ColumnId = "Id";
        private const string ColumnWord = "Word";
        private const string ColumnMeaning = "Meaning";
        private const string ColumnPronunciation = "Pronunciation";
        private const string ColumnAudioUrl = "AudioUrl";

        // Lấy danh sách tất cả các từ vựng từ cơ sở dữ liệu
        public List<Vocabulary> GetAllVocabulary()
        {
            List<Vocabulary> vocabularies = new List<Vocabulary>();

            try
            {
                using (SqlConnection conn = DatabaseContext.GetConnection())
                {
                    conn.Open();
                    string query = $"SELECT * FROM {TableName}";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            vocabularies.Add(new Vocabulary
                            {
                                Id = (int)reader[ColumnId],
                                Word = reader[ColumnWord].ToString(),
                                Meaning = reader[ColumnMeaning].ToString(),
                                Pronunciation = reader[ColumnPronunciation].ToString(),
                                AudioUrl = reader[ColumnAudioUrl].ToString()
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log lỗi vào hệ thống log chuyên dụng
                Console.WriteLine($"Error in GetAllVocabulary: {ex.Message}");
            }

            return vocabularies;
        }

        // Lấy từ vựng theo từ khóa
        public Vocabulary GetVocabularyByWord(string word)
        {
            try
            {
                using (SqlConnection conn = DatabaseContext.GetConnection())
                {
                    conn.Open();
                    string query = $"SELECT * FROM {TableName} WHERE {ColumnWord} = @Word";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.Add("@Word", SqlDbType.NVarChar, 100).Value = word;
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new Vocabulary
                                {
                                    Id = (int)reader[ColumnId],
                                    Word = reader[ColumnWord].ToString(),
                                    Meaning = reader[ColumnMeaning].ToString(),
                                    Pronunciation = reader[ColumnPronunciation].ToString(),
                                    AudioUrl = reader[ColumnAudioUrl].ToString()
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetVocabularyByWord: {ex.Message}");
            }
            return null;
        }

        // Thêm một từ vựng mới vào cơ sở dữ liệu
        public void AddVocabulary(string word, string meaning, string pronunciation, string audioUrl)
        {
            try
            {
                using (SqlConnection conn = DatabaseContext.GetConnection())
                {
                    conn.Open();
                    string query = $"INSERT INTO {TableName} ({ColumnWord}, {ColumnMeaning}, {ColumnPronunciation}, {ColumnAudioUrl}) " +
                                   "VALUES (@Word, @Meaning, @Pronunciation, @AudioUrl)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.Add("@Word", SqlDbType.NVarChar, 100).Value = word;
                        cmd.Parameters.Add("@Meaning", SqlDbType.NVarChar, 500).Value = meaning;
                        cmd.Parameters.Add("@Pronunciation", SqlDbType.NVarChar, 100).Value = pronunciation;
                        cmd.Parameters.Add("@AudioUrl", SqlDbType.NVarChar, 300).Value = audioUrl;
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in AddVocabulary: {ex.Message}");
            }
        }

        // Cập nhật một từ vựng vào cơ sở dữ liệu (có thể cần trong tương lai)
        public void UpdateVocabulary(int id, string word, string meaning, string pronunciation, string audioUrl)
        {
            try
            {
                using (SqlConnection conn = DatabaseContext.GetConnection())
                {
                    conn.Open();
                    string query = $"UPDATE {TableName} SET {ColumnWord} = @Word, {ColumnMeaning} = @Meaning, " +
                                   $"{ColumnPronunciation} = @Pronunciation, {ColumnAudioUrl} = @AudioUrl WHERE {ColumnId} = @Id";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                        cmd.Parameters.Add("@Word", SqlDbType.NVarChar, 100).Value = word;
                        cmd.Parameters.Add("@Meaning", SqlDbType.NVarChar, 500).Value = meaning;
                        cmd.Parameters.Add("@Pronunciation", SqlDbType.NVarChar, 100).Value = pronunciation;
                        cmd.Parameters.Add("@AudioUrl", SqlDbType.NVarChar, 300).Value = audioUrl;
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in UpdateVocabulary: {ex.Message}");
            }
        }

        // Lấy từ vựng theo Id (theo kiểu int)
        public Vocabulary GetVocabularyById(int id)
        {
            Vocabulary vocab = null;
            try
            {
                using (SqlConnection conn = DatabaseContext.GetConnection())
                {
                    conn.Open();
                    string query = $"SELECT * FROM {TableName} WHERE {ColumnId} = @Id";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                vocab = new Vocabulary
                                {
                                    Id = (int)reader[ColumnId],
                                    Word = reader[ColumnWord].ToString(),
                                    Meaning = reader[ColumnMeaning].ToString(),
                                    Pronunciation = reader[ColumnPronunciation].ToString(),
                                    AudioUrl = reader[ColumnAudioUrl].ToString()
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in GetVocabularyById: " + ex.Message);
            }
            return vocab;
        }

        // Phương thức xóa từ vựng theo ID
        public void DeleteVocabulary(int id)
        {
            using (var connection = DatabaseContext.GetConnection())
            {
                connection.Open();
                string query = $"DELETE FROM {TableName} WHERE {ColumnId} = @Id";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Phương thức lấy từ vựng theo Id (có thể trả về thông tin bổ sung)
        public Vocabulary GetWordById(int id)
        {
            using (var connection = DatabaseContext.GetConnection())
            {
                connection.Open();
                string query = $"SELECT * FROM {TableName} WHERE {ColumnId} = @Id";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Vocabulary
                            {
                                Id = (int)reader["Id"],
                                Word = reader["Word"].ToString(),
                                // Nếu có các cột riêng biệt cho nghĩa và phát âm (ví dụ: MeaningVN, PronunciationUS)
                                // kiểm tra và thay đổi cho phù hợp:
                                Meaning = reader["Meaning"].ToString(),
                                Pronunciation = reader["Pronunciation"].ToString(),
                                AudioUrl = reader["AudioUrl"].ToString()
                            };
                        }
                    }
                }
            }
            return null;
        }

        // Lấy danh sách từ vựng theo chủ đề sử dụng JOIN với bảng VocabularyTopic và Topics
        public List<Vocabulary> GetVocabularyByTopic(string topic)
        {
            var vocabularyList = new List<Vocabulary>();

            using (var connection = DatabaseContext.GetConnection())
            {
                connection.Open();
                string query = @"SELECT v.Id, v.Word, v.Meaning, v.Pronunciation, v.AudioUrl 
                                 FROM Vocabulary v
                                 INNER JOIN VocabularyTopic vt ON v.Id = vt.VocabularyId
                                 INNER JOIN Topics t ON vt.TopicId = t.Id
                                 WHERE t.Name = @Topic";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Topic", topic);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var vocabulary = new Vocabulary
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Word = reader["Word"].ToString(),
                                Meaning = reader["Meaning"].ToString(),
                                Pronunciation = reader["Pronunciation"].ToString(),
                                AudioUrl = reader["AudioUrl"].ToString()
                            };
                            vocabularyList.Add(vocabulary);
                        }
                    }
                }
            }

            return vocabularyList;
        }
    }
}
