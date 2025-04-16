using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using WordVaultAppMVC.Controllers;
using WordVaultAppMVC.Models;

namespace WordVaultAppMVC.Data
{
    public class QuizRepository
    {
        // Lấy danh sách tất cả các câu hỏi quiz từ bảng QuizQuestions
        public List<QuizQuestion> GetAllQuizQuestions()
        {
            List<QuizQuestion> questions = new List<QuizQuestion>();
            try
            {
                using (SqlConnection conn = DatabaseContext.GetConnection())
                {
                    conn.Open();
                    // Giả sử bảng QuizQuestions có các cột như mô tả
                    string query = "SELECT QuizId, QuestionText, Option1, Option2, Option3, Option4, CorrectOption FROM QuizQuestions";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            QuizQuestion question = new QuizQuestion
                            {
                                QuizId = (int)reader["QuizId"],
                                QuestionText = reader["QuestionText"].ToString(),
                                Options = new List<string>
                                {
                                    reader["Option1"].ToString(),
                                    reader["Option2"].ToString(),
                                    reader["Option3"].ToString(),
                                    reader["Option4"].ToString()
                                },
                                CorrectOption = Convert.ToInt32(reader["CorrectOption"])
                            };
                            questions.Add(question);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in GetAllQuizQuestions: " + ex.Message);
            }
            return questions;
        }

        // Lấy một câu hỏi quiz theo ID
        public QuizQuestion GetQuizQuestionById(int quizId)
        {
            QuizQuestion question = null;
            try
            {
                using (SqlConnection conn = DatabaseContext.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT QuizId, QuestionText, Option1, Option2, Option3, Option4, CorrectOption FROM QuizQuestions WHERE QuizId = @QuizId";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.Add("@QuizId", SqlDbType.Int).Value = quizId;
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                question = new QuizQuestion
                                {
                                    QuizId = (int)reader["QuizId"],
                                    QuestionText = reader["QuestionText"].ToString(),
                                    Options = new List<string>
                                    {
                                        reader["Option1"].ToString(),
                                        reader["Option2"].ToString(),
                                        reader["Option3"].ToString(),
                                        reader["Option4"].ToString()
                                    },
                                    CorrectOption = Convert.ToInt32(reader["CorrectOption"])
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in GetQuizQuestionById: " + ex.Message);
            }
            return question;
        }

        // Thêm kết quả làm quiz vào bảng QuizResults
        public void AddQuizResult(QuizResult result)
        {
            try
            {
                using (SqlConnection conn = DatabaseContext.GetConnection())
                {
                    conn.Open();
                    string query = "INSERT INTO QuizResults (QuizId, IsCorrect, DateTaken, UserId) VALUES (@QuizId, @IsCorrect, @DateTaken, @UserId)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.Add("@QuizId", SqlDbType.Int).Value = result.QuizId;
                        cmd.Parameters.Add("@IsCorrect", SqlDbType.Bit).Value = result.IsCorrect;
                        cmd.Parameters.Add("@DateTaken", SqlDbType.DateTime).Value = result.DateTaken;
                        // Nếu không có UserId, có thể truyền chuỗi rỗng hoặc NULL tùy thiết kế của bạn
                        cmd.Parameters.Add("@UserId", SqlDbType.NVarChar, 50).Value = string.IsNullOrEmpty(result.UserId) ? (object)DBNull.Value : result.UserId;
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in AddQuizResult: " + ex.Message);
            }
        }
    }
}
