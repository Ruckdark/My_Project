using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StaffMgmt.Models
{
    [Table("Certificates")]
    public class Certificate
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        // Foreign Key property
        [Required]
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "Tên văn bằng là bắt buộc.")]
        [StringLength(200, ErrorMessage = "Tên văn bằng không được vượt quá 200 ký tự.")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Ngày cấp là bắt buộc.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime IssueDate { get; set; }

        // Foreign Key property for Issuer Province
        public int? IssuerProvinceId { get; set; } // Nullable FK

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? ExpiryDate { get; set; } // Nullable

        // --- Navigation Properties ---
        [ForeignKey("EmployeeId")] // Liên kết FK với Navigation Property
        public virtual Employee Employee { get; set; } = null!; // Một văn bằng thuộc về một Employee

        [ForeignKey("IssuerProvinceId")] // Liên kết FK với Navigation Property
        public virtual Province? IssuerProvince { get; set; } // Đơn vị cấp (Tỉnh), có thể null
    }
}