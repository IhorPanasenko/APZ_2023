using System.ComponentModel.DataAnnotations;

namespace InsuranceDiscountsWeb.ViewModels.UpdateViewModels
{
    public class UpdateActivityViewModel
    {
        [Required]
        public Guid Id { get; set; } = Guid.NewGuid();

        [MinLength(1)]
        public string? Type { get; set; }

        [Range(1,100000000)]
        public double Duration { get; set; } = -1;

        [Range(1, 100000000)]
        public double Distance { get; set; } = -1;

        [Range(1, 100000000)]
        public double Calories { get; set; } = -1;

        public Guid? UserId { get; set; }
    }
}
