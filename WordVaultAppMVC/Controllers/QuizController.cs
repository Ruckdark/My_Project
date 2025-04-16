using System;
using System.Collections.Generic;
using WordVaultAppMVC.Data;
using WordVaultAppMVC.Models;

namespace WordVaultAppMVC.Controllers
{
    public class QuizController
    {
        private readonly QuizRepository _quizRepository;

        public QuizController()
        {
            _quizRepository = new QuizRepository();
        }

        // Lấy danh sách các câu hỏi quiz
        public List<QuizQuestion> GetQuizQuestions()
        {
            // Giả sử QuizRepository có phương thức GetAllQuizQuestions() trả về danh sách QuizQuestion
            return _quizRepository.GetAllQuizQuestions();
        }

        // Xử lý việc nộp đáp án cho một câu hỏi quiz
        // selectedOption là số thứ tự của đáp án người dùng chọn (ví dụ: 1,2,3,4)
        public bool SubmitQuizAnswer(int quizId, int selectedOption)
        {
            // Lấy câu hỏi quiz theo ID
            QuizQuestion quizQuestion = _quizRepository.GetQuizQuestionById(quizId);
            if (quizQuestion == null)
            {
                throw new Exception("Câu hỏi quiz không tồn tại!");
            }

            // Kiểm tra đáp án
            bool isCorrect = (quizQuestion.CorrectOption == selectedOption);

            // Ghi nhận kết quả làm quiz
            QuizResult result = new QuizResult
            {
                QuizId = quizId,
                IsCorrect = isCorrect,
                DateTaken = DateTime.Now
                // Nếu có thông tin UserId, bạn có thể thêm vào đây
            };

            _quizRepository.AddQuizResult(result);
            return isCorrect;
        }
    }

    // Model phụ đại diện cho câu hỏi quiz (có thể mở rộng)
    public class QuizQuestion
    {
        public int QuizId { get; set; }
        public string QuestionText { get; set; }
        public List<string> Options { get; set; }
        public int CorrectOption { get; set; }
    }
}
