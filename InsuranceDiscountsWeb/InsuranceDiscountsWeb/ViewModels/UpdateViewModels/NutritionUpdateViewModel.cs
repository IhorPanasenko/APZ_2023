using Core.Models;

namespace InsuranceDiscountsWeb.ViewModels.UpdateViewModels
{
    public class NutritionUpdateViewModel
    {
        public Guid Id { get; set; }

        public string? Meal { get; set; }

        public string? Food { get; set; }

        public double Calories { get; set; } = -1;

        public double Fat { get; set; } = -1;

        public double Protein { get; set; } = -1;

        public double Cards { get; set; } = -1; 

        public Guid? UserId { get; set; }
    }
}
