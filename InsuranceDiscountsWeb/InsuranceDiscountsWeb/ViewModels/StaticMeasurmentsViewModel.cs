using Core.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace InsuranceDiscountsWeb.ViewModels
{
    public class StaticMeasurmentsViewModel
    {
        [Required]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [Range(0,3000)]
        public double Height { get; set; }

        [Required]
        [Range(0,400)]
        public double Weight { get; set; }

        [Required]
        [Range(0,500)]
        public double Waist { get; set; }

        [Required]
        public DateTime MesurmentDate { get; set; } = DateTime.UtcNow;

        [ForeignKey("UserId")]
        public Guid UserId { get; set; }

        public AppUser? AppUser { get; set; }
    }
}
