// File: Models/Ward.cs
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StaffMgmt.Models
{
    [Table("Wards")]
    public class Ward
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên xã/phường là bắt buộc.")]
        [StringLength(100, ErrorMessage = "Tên xã/phường không được vượt quá 100 ký tự.")]
        public string Name { get; set; } = null!;

        // --- Thêm thuộc tính Code ---
        [StringLength(10, ErrorMessage = "Mã xã/phường không được vượt quá 10 ký tự.")]
        public string? Code { get; set; } // Cho phép null nếu cần
                                          // --------------------------

        [Required(ErrorMessage = "Quận/huyện là bắt buộc.")]
        public int DistrictId { get; set; }

        [ForeignKey("DistrictId")]
        public virtual District District { get; set; } = null!;

        public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}
