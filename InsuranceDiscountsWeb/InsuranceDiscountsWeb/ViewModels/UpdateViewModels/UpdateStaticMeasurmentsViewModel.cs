using System.ComponentModel.DataAnnotations;

namespace InsuranceDiscountsWeb.ViewModels.UpdateViewModels
{
    public class UpdateStaticMeasurmentsViewModel
    {
        [Required]
        public Guid Id { get; set; }

        public double Height { get; set; } = -1;

        public double Weight { get; set; } = -1;

        public double Waist { get; set; } = -1;

        public DateTime? MesurmentDate { get; set; }
    }
}
