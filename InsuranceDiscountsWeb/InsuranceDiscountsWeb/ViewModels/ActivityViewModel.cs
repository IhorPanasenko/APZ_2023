using Core.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace InsuranceDiscountsWeb.ViewModels
{
    public class ActivityViewModel
    {
        [Required]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string Type { get; set; } = String.Empty;

        [Required]
        public double Duration { get; set; }

        [Required]
        public double Distance { get; set; }

        [Required]
        public double Calories { get; set; }

        [ForeignKey("UserId")]
        public Guid UserId { get; set; }

        public AppUser? AppUser { get; set; }
    }
}
