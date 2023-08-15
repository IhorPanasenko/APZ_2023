using System.ComponentModel.DataAnnotations;

namespace InsuranceDiscountsWeb.ViewModels.UpdateViewModels
{
    public class UpdateAgentViewModel
    {

        [Required]
        public Guid Id { get; set; } = Guid.NewGuid();

        public string? FirstName { get; set; }

        public string? SecondName { get; set; }

        [Phone]
        public string? PhoneNumber { get; set; }

        [EmailAddress]
        public string? EmailAddress { get; set; }

        [Range(0, 10)]
        public double Raiting { get; set; } = -1;

        public Guid? CompanyId { get; set; }
    }
}
