using System.ComponentModel.DataAnnotations;

namespace StudentInfoSystem.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Range(1, 150)]
        public int Age { get; set; }

        [Range(0, 100)]
        public decimal Marks { get; set; }

        [StringLength(2)]
        public string Grade { get; set; } = string.Empty;
    }
}
