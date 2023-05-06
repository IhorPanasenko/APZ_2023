using System.ComponentModel.DataAnnotations;

namespace InsuranceDiscountsWeb.ViewModels.UpdateViewModels
{
    public class UpdatePeriodicMeasurmentsViewModel
    {

        [Required]
        public Guid Id { get; set; }

        public int Pulse { get; set; } = -1;

        public double Glucose { get; set; } = -1;

        public double Cholesterol { get; set; } = -1;

        public double BloodPreasure { get; set; } = -1;
        public DateTime? MesurmentDate { get; set; }
    }
}
