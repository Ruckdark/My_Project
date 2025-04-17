using System;
using System.Linq;
using System.Windows.Forms;
using WordVaultAppMVC.Models;
using WordVaultAppMVC.Data;

namespace WordVaultAppMVC.Controllers
{
    public class LearningController
    {
        private readonly VocabularyRepository _vocabularyRepository;
        private readonly LearningStatusRepository _learningStatusRepository;

        public LearningController()
        {
            _vocabularyRepository = new VocabularyRepository();
            _learningStatusRepository = new LearningStatusRepository();
        }

        // Phân loại từ vựng dựa trên nghĩa người dùng nhập
        public void ClassifyVocabulary(string wordId, string userMeaning)
        {
            // Chuyển đổi wordId từ string sang int
            if (!int.TryParse(wordId, out int id))
            {
                MessageBox.Show("ID từ không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var word = _vocabularyRepository.GetWordById(id);
            if (word == null)
            {
                MessageBox.Show("Từ vựng không tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string correctMeaning = word.Meaning;

            if (string.IsNullOrEmpty(userMeaning))
            {
                MessageBox.Show("Vui lòng nhập nghĩa của từ!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (userMeaning.Trim().Equals(correctMeaning, StringComparison.OrdinalIgnoreCase))
            {
                UpdateLearningStatus(wordId, "Đã học");
                MessageBox.Show("Chính xác! Từ vựng đã được phân loại là đã học.", "Kết quả", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                UpdateLearningStatus(wordId, "Đang học");
                MessageBox.Show($"Sai rồi! Nghĩa đúng là: {correctMeaning}", "Kết quả", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void UpdateLearningStatus(string wordId, string status)
        {
            var existingStatus = _learningStatusRepository.GetLearningStatusByWordId(wordId);
            if (existingStatus != null)
            {
                existingStatus.Status = status;
                _learningStatusRepository.UpdateLearningStatus(existingStatus);
            }
            else
            {
                var newStatus = new LearningStatus
                {
                    WordId = wordId,
                    Status = status,
                    DateLearned = DateTime.Now
                };
                _learningStatusRepository.AddLearningStatus(newStatus);
            }
        }

        public void GetLearnedVocabulary()
        {
            var learnedWords = _learningStatusRepository.GetAllLearningStatus()
                .Where(ls => ls.Status == "Đã học")
                .Select(ls =>
                {
                    // Chuyển đổi ls.WordId từ string sang int
                    if (int.TryParse(ls.WordId, out int id))
                        return _vocabularyRepository.GetWordById(id);
                    return null;
                })
                .Where(w => w != null)
                .ToList();

            if (learnedWords.Count > 0)
            {
                string learnedWordsList = string.Join(Environment.NewLine, learnedWords.Select(w => w.Word));
                MessageBox.Show($"Các từ vựng đã học:\n{learnedWordsList}", "Danh sách từ vựng đã học", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Chưa có từ vựng nào được học!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void GetLearningVocabulary()
        {
            var learningWords = _learningStatusRepository.GetAllLearningStatus()
                .Where(ls => ls.Status == "Đang học")
                .Select(ls =>
                {
                    if (int.TryParse(ls.WordId, out int id))
                        return _vocabularyRepository.GetWordById(id);
                    return null;
                })
                .Where(w => w != null)
                .ToList();

            if (learningWords.Count > 0)
            {
                string learningWordsList = string.Join(Environment.NewLine, learningWords.Select(w => w.Word));
                MessageBox.Show($"Các từ vựng đang học:\n{learningWordsList}", "Danh sách từ vựng đang học", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Không có từ vựng nào đang học!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void GetUnlearnedVocabulary()
        {
            var unlearnedWords = _learningStatusRepository.GetAllLearningStatus()
                .Where(ls => ls.Status == "Chưa học")
                .Select(ls =>
                {
                    if (int.TryParse(ls.WordId, out int id))
                        return _vocabularyRepository.GetWordById(id);
                    return null;
                })
                .Where(w => w != null)
                .ToList();

            if (unlearnedWords.Count > 0)
            {
                string unlearnedWordsList = string.Join(Environment.NewLine, unlearnedWords.Select(w => w.Word));
                MessageBox.Show($"Các từ vựng chưa học:\n{unlearnedWordsList}", "Danh sách từ vựng chưa học", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Tất cả từ vựng đã được học!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
