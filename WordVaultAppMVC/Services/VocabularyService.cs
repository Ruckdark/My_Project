using System;
using System.Collections.Generic;
using System.Diagnostics; // Thêm using cho Debug
using System.Linq; // Thêm using cho .Any()
using WordVaultAppMVC.Data;
using WordVaultAppMVC.Models;

namespace WordVaultAppMVC.Services
{
    public class VocabularyService
    {
        private readonly VocabularyRepository _vocabularyRepository;
        // Khởi tạo Random một lần để tránh các giá trị giống nhau nếu gọi liên tục
        private static readonly Random rnd = new Random();

        public VocabularyService()
        {
            // Khởi tạo Repository khi Service được tạo
            _vocabularyRepository = new VocabularyRepository();
        }

        /// <summary>
        /// Lấy một đối tượng Vocabulary ngẫu nhiên từ cơ sở dữ liệu.
        /// </summary>
        /// <returns>Một đối tượng Vocabulary ngẫu nhiên, hoặc null nếu không có từ nào hoặc có lỗi.</returns>
        // Đã cập nhật: Trả về Vocabulary, bỏ out parameter
        public Vocabulary GetRandomWord()
        {
            List<Vocabulary> vocabularies = null;
            try
            {
                // Gọi repository để lấy tất cả từ vựng
                vocabularies = _vocabularyRepository.GetAllVocabulary();
            }
            catch (Exception ex)
            {
                // Ghi log lỗi nếu không lấy được danh sách từ repository
                Debug.WriteLine($"[VocabularyService] Error calling GetAllVocabulary: {ex.Message}");
                return null; // Trả về null nếu có lỗi ở tầng Repository
            }

            // Kiểm tra danh sách trả về có hợp lệ và có phần tử không
            if (vocabularies == null || !vocabularies.Any()) // Dùng Linq.Any() cho hiệu quả
            {
                Debug.WriteLine("[VocabularyService] No vocabularies found or returned list is null.");
                return null; // Trả về null nếu không có từ vựng
            }

            // Lấy một index ngẫu nhiên trong phạm vi danh sách
            int index = rnd.Next(vocabularies.Count);

            // Trả về toàn bộ đối tượng Vocabulary tại index đó
            Debug.WriteLine($"[VocabularyService] Returning random word: {vocabularies[index].Word} (ID: {vocabularies[index].Id})");
            return vocabularies[index];
        }

        /// <summary>
        /// Lấy nghĩa của từ vựng theo ID (giữ lại từ code gốc của bạn).
        /// </summary>
        public string GetWordMeaning(string wordId) // Giữ nguyên hàm này từ code bạn cung cấp
        {
            // Cố gắng chuyển đổi wordId (string) sang id (int)
            if (int.TryParse(wordId, out int id))
            {
                try
                {
                    // Gọi repository để lấy từ theo Id
                    Vocabulary vocab = _vocabularyRepository.GetVocabularyById(id);

                    // Trả về Meaning nếu tìm thấy vocab, ngược lại trả về chuỗi thông báo
                    // Sử dụng toán tử "?." (null-conditional) để tránh lỗi nếu vocab là null
                    // Sử dụng toán tử "??" (null-coalescing) để cung cấp giá trị mặc định nếu Meaning là null
                    return vocab?.Meaning ?? "Không tìm thấy nghĩa (từ tồn tại nhưng không có nghĩa)!";
                }
                catch (Exception ex)
                {
                    // Ghi log lỗi nếu không lấy được từ theo Id
                    Debug.WriteLine($"[VocabularyService] Error calling GetVocabularyById for ID {wordId}: {ex.Message}");
                    return "Lỗi khi truy vấn nghĩa!"; // Trả về thông báo lỗi chung
                }
            }
            // Trả về thông báo nếu wordId không phải là số hợp lệ
            return "ID từ không hợp lệ!";
        }


        // Bạn có thể thêm các hàm khác vào Service này nếu cần,
        // ví dụ: hàm lấy từ ngẫu nhiên nhưng ưu tiên từ chưa học.
    }
}