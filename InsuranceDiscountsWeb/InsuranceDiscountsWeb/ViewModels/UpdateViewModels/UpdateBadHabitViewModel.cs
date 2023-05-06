using System.ComponentModel.DataAnnotations;

namespace InsuranceDiscountsWeb.ViewModels.UpdateViewModels
{
    public class UpdateBadHabitViewModel
    {
        [Required]
        public Guid Id { get; set; }

        public string? Name { get; set; }

        [Range(0, 100)]
        public int Level { get; set; } = -1;
    }
}
