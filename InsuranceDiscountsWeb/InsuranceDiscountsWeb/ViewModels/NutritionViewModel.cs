using Core.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace InsuranceDiscountsWeb.ViewModels
{
    public class NutritionViewModel
    {
        [Required]
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Meal { get; set; } = String.Empty;

        public string Food { get; set; } = String.Empty;

        public double Calories { get; set; }

        public double Fat { get; set; }

        public double Protein { get; set; }

        public double Cards { get; set; }

        public Guid UserId { get; set; }

        public AppUser? AppUser { get; set; }
    }
}
