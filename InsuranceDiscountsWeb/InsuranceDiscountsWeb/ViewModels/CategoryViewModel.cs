using System.ComponentModel.DataAnnotations;

namespace InsuranceDiscountsWeb.ViewModels
{
    public class CategoryViewModel
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        [MinLength(3)]
        public string CategoryName { get; set; } = String.Empty;
    }
}
