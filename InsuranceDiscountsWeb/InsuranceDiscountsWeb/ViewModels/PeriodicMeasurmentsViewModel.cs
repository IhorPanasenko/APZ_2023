using Core.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace InsuranceDiscountsWeb.ViewModels
{
    public class PeriodicMeasurmentsViewModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [Range(40,400)]
        public int Pulse { get; set; }

        [Required]
        [Range(0,1000)]
        public double Glucose { get; set; }

        [Required]
        [Range(0,1000)]
        public double Cholesterol { get; set; }

        [Required]
        [Range(0,500)]
        public double BloodPreasure { get; set; }

        [Required]
        public DateTime MesurmentDate { get; set; } = DateTime.UtcNow;

        [Required]
        public Guid UserId { get; set; }

        public AppUser? AppUser { get; set; }
    }
}
