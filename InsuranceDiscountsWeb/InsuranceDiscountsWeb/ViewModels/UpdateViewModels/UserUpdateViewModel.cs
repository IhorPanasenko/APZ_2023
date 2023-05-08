using System.ComponentModel.DataAnnotations;

namespace InsuranceDiscountsWeb.ViewModels.UpdateViewModels
{
    public class UserUpdateViewModel
    {
        [Required]
        public Guid Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? UserName { get; set; }

        public DateTime? BirthdayDate { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        [Phone]
        public string? PhoneNumber { get; set; }

        public string? Address { get; set; }
    }
}
