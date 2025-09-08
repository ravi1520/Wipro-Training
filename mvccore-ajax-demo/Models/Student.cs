using System.ComponentModel.DataAnnotations;

namespace mvccore_ajax_demo.Models
{
    public class Student
    {
        [Key]
        public int studentId { get; set; }

        [Required]
        public string studentName { get; set; } = string.Empty;   // ✅ no warning

        [Required]
        public string studentAddress { get; set; } = string.Empty; // ✅ no warning
    }
}
