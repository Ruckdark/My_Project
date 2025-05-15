// File: Models/District.cs
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StaffMgmt.Models
{
    [Table("Districts")]
    public class District
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên quận/huyện là bắt buộc.")]
        [StringLength(100, ErrorMessage = "Tên quận/huyện không được vượt quá 100 ký tự.")]
        public string Name { get; set; } = null!;

        // --- Thêm thuộc tính Code ---
        [StringLength(10, ErrorMessage = "Mã quận/huyện không được vượt quá 10 ký tự.")]
        // Code có thể NULL ban đầu nếu nhập liệu sau, nhưng nên là Required nếu đã seed
        public string? Code { get; set; } // Cho phép null nếu cần
                                          // --------------------------

        [Required(ErrorMessage = "Tỉnh/thành phố là bắt buộc.")]
        public int ProvinceId { get; set; }

        [ForeignKey("ProvinceId")]
        public virtual Province Province { get; set; } = null!;

        public virtual ICollection<Ward> Wards { get; set; } = new List<Ward>();
        public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}
