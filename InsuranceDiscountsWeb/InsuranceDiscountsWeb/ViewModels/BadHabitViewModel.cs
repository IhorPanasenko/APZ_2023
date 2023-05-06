using System.ComponentModel.DataAnnotations;

namespace InsuranceDiscountsWeb.ViewModels
{
    public class BadHabitViewModel
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; } = String.Empty;

        [Required]
        [Range(0, 100)]
        public int Level { get; set; }
    }
}
