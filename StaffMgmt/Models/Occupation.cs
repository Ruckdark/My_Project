using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StaffMgmt.Models
{
    [Table("Occupations")]
    public class Occupation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên nghề nghiệp là bắt buộc.")]
        [StringLength(100, ErrorMessage = "Tên nghề nghiệp không được vượt quá 100 ký tự.")]
        public string Name { get; set; } = null!;

        // Navigation property
        public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}