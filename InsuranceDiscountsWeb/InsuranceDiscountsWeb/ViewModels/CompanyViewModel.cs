using System.ComponentModel.DataAnnotations;

namespace InsuranceDiscountsWeb.ViewModels
{
    public class CompanyViewModel
    {
        public Guid? Id { get; set; }

        [Required]
        [MinLength(3)]
        public string CompanyName { get; set; } = String.Empty;

        [Required]
        public string Description { get; set; } = String.Empty;

        [Required]
        [MinLength(3)]
        public string Address { get; set; } = String.Empty;

        [Required]
        [Phone]
        [MinLength(6)]
        public string PhoneNumber { get; set; } = String.Empty;

        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; } = String.Empty;

        [Required]
        public string WebsiteAddress { get; set; } = String.Empty;

        [Required]
        public int MaxDiscountPercentage { get; set; } = 30;
    }
}
