using System.ComponentModel.DataAnnotations;

namespace InsuranceDiscountsWeb.ViewModels.UpdateViewModels
{
    public class UpdateCompanyViewModel
    {
        [Required]
        public Guid Id { get; set; } = Guid.NewGuid();

        public string? CompanyName { get; set; }

        public string? Description { get; set; }

        public string? Address { get; set; }

        [Phone]
        public string? PhoneNumber { get; set; }

        [EmailAddress]
        public string? EmailAddress { get; set; }

        [Url]
        public string? WebsiteAddress { get; set; }
    }
}
