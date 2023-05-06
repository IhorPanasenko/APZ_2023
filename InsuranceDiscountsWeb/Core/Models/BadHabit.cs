using System.ComponentModel.DataAnnotations;

namespace Core.Models
{
    public class BadHabit
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; } = String.Empty;

        [Required]
        [Range(0,100)]
        public int Level { get; set; }
    }
}