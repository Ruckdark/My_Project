using System;

namespace WordVaultAppMVC.Models
{
    public class LearningStatus
    {
        public int Id { get; set; }
        public string WordId { get; set; }    // ID của từ vựng (có thể là string nếu bạn lưu dưới dạng NVARCHAR)
        public string UserId { get; set; }    // ID người dùng (nếu áp dụng)
        public string Status { get; set; }    // Ví dụ: "Chưa học", "Đang học", "Đã học"
        public DateTime DateLearned { get; set; } // Ngày học hoặc ôn tập (nếu có)
    }
}
