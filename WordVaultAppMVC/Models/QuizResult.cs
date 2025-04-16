using System;

namespace WordVaultAppMVC.Models
{
    public class QuizResult
    {
        public int Id { get; set; }
        public int QuizId { get; set; }       // ID của câu hỏi quiz
        public bool IsCorrect { get; set; }   // True nếu đáp án đúng, False nếu sai
        public DateTime DateTaken { get; set; } // Ngày làm bài quiz
        public string UserId { get; set; }    // ID người dùng (nếu cần)
    }
}
