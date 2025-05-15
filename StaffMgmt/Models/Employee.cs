using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StaffMgmt.Models
{
    [Table("Employees")]
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Họ tên là bắt buộc.")]
        [StringLength(150, ErrorMessage = "Họ tên không được vượt quá 150 ký tự.")]
        public string FullName { get; set; } = null!;

        [DataType(DataType.Date)] // Chỉ định kiểu dữ liệu là Date
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)] // Định dạng hiển thị và chỉnh sửa
        public DateTime? DateOfBirth { get; set; } // Kiểu DateTime nullable (hoặc DateOnly? nếu dùng .NET 6+)

        // --- Khóa ngoại và Navigation Properties cho Dân tộc/Nghề nghiệp (Nếu dùng bảng riêng) ---
        public int? EthnicityId { get; set; } // Nullable FK
        [ForeignKey("EthnicityId")]
        public virtual Ethnicity? Ethnicity { get; set; } // Nullable navigation

        public int? OccupationId { get; set; } // Nullable FK
        [ForeignKey("OccupationId")]
        public virtual Occupation? Occupation { get; set; } // Nullable navigation

        // --- Hoặc dùng string nếu không dùng bảng riêng ---
        // [StringLength(50)]
        // public string? EthnicityName { get; set; }
        // [StringLength(100)]
        // public string? OccupationName { get; set; }
        // ----------------------------------------------------

        [StringLength(20, ErrorMessage = "Số CCCD không được vượt quá 20 ký tự.")]
        // Lưu ý: UNIQUE constraint nên được cấu hình bằng Fluent API trong DbContext để xử lý tốt hơn
        public string? IdentityCardNumber { get; set; } // Nullable

        [StringLength(15, ErrorMessage = "Số điện thoại không được vượt quá 15 ký tự.")]
        [Phone(ErrorMessage = "Định dạng số điện thoại không hợp lệ.")] // Validate cơ bản kiểu Phone
        public string? PhoneNumber { get; set; } // Nullable

        // --- Khóa ngoại và Navigation Properties cho Địa chỉ ---
        public int? ProvinceId { get; set; } // Nullable FK
        [ForeignKey("ProvinceId")]
        public virtual Province? Province { get; set; } // Nullable navigation

        public int? DistrictId { get; set; } // Nullable FK
        [ForeignKey("DistrictId")]
        public virtual District? District { get; set; } // Nullable navigation

        public int? WardId { get; set; } // Nullable FK
        [ForeignKey("WardId")]
        public virtual Ward? Ward { get; set; } // Nullable navigation

        [StringLength(255, ErrorMessage = "Địa chỉ cụ thể không được vượt quá 255 ký tự.")]
        public string? StreetAddress { get; set; } // Nullable

        // --- Navigation property cho Văn bằng ---
        // Một nhân viên có nhiều văn bằng
        public virtual ICollection<Certificate> Certificates { get; set; } = new List<Certificate>();
    }
}