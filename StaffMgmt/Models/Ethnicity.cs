using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StaffMgmt.Models
{
    [Table("Ethnicities")]
    public class Ethnicity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên dân tộc là bắt buộc.")]
        [StringLength(50, ErrorMessage = "Tên dân tộc không được vượt quá 50 ký tự.")]
        public string Name { get; set; } = null!;

        // Navigation property
        public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}