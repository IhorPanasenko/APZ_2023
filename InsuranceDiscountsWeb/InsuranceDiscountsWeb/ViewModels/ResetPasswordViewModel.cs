using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace InsuranceDiscountsWeb.ViewModels
{
    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = String.Empty;

        [Required]
        [DataType(DataType.Password)]
        [MinLength(5)]
        public string NewPassword { get; set; } = String.Empty;

        [Required]
        [DataType(DataType.Password)]
        [MinLength(5)]
        public string ConfirmationNewPassword { get; set; } = String.Empty;

        [Required]
        public string Code { get; set; } = String.Empty;
    }
}
