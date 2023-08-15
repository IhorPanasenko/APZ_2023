using Core.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace InsuranceDiscountsWeb.ViewModels
{
    public class AgentViewModel
    {
        public Guid? Id { get; set; }

        [Required]
        public string FirstName { get; set; } = String.Empty;

        [Required]
        public string SecondName { get; set; } = String.Empty;

        [Required]
        [Phone]
        public string PhoneNumber { get; set; } = String.Empty;

        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; } = String.Empty;

        [Required]
        [Range(0,10)]
        public double Raiting { get; set; }

        [Required]
        [ForeignKey("CompanyId")]
        public Guid CompanyId { get; set; }

        [NotMapped]
        public Company? Company { get; set; }
    }
}
