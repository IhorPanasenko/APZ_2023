using System.ComponentModel.DataAnnotations;

namespace InsuranceDiscountsWeb.ViewModels
{
    public class RoleViewModel
    {

        [Required]
        public string Name { get; set; } = string.Empty;
    }
}
