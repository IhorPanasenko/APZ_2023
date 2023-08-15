using System.ComponentModel.DataAnnotations;

namespace InsuranceDiscountsWeb.ViewModels
{
    public class AppUserViewModel
    {
        public Guid Id { get; set; }

        [Required]
        public string FirstName { get; set; } = String.Empty;

        [Required]
        public string LastName { get; set; } = String.Empty;

        [Required]
        public string UserName { get; set; } = String.Empty;

        [Required]
        public DateTime BirthdayDate { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; } = String.Empty;

        [Required]
        [Phone]
        public string? PhoneNumber { get; set; }

        [Required]
        public string Address { get; set; } = String.Empty;

        public List<string> Roles { get; set; } = new List<string>();
    }
}
