using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace InsuranceDiscountsWeb.ViewModels
{
    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(5)]
        public string NewPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(5)]
        public string ConfirmationNewPassword { get; set; }

        [Required]
        public string Code { get; set; }
    }
}
