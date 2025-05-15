// File: Models/Province.cs
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StaffMgmt.Models
{
    [Table("Provinces")]
    public class Province
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên tỉnh/thành phố là bắt buộc.")]
        [StringLength(100, ErrorMessage = "Tên tỉnh/thành phố không được vượt quá 100 ký tự.")]
        public string Name { get; set; } = null!;

        // --- Thêm thuộc tính Code ---
        [Required(ErrorMessage = "Mã tỉnh/thành phố là bắt buộc.")]
        [StringLength(10, ErrorMessage = "Mã tỉnh/thành phố không được vượt quá 10 ký tự.")]
        // TODO: Cân nhắc thêm Index Unique bằng Fluent API trong DbContext nếu mã là duy nhất
        public string Code { get; set; } = null!;
        // --------------------------

        // Navigation properties
        public virtual ICollection<District> Districts { get; set; } = new List<District>();
        public virtual ICollection<Certificate> IssuedCertificates { get; set; } = new List<Certificate>();
        public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}
