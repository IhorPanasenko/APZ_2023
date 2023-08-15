using System.ComponentModel.DataAnnotations;

namespace InsuranceDiscountsWeb.ViewModels
{
    public class UpdateCategoryViewModel
    {
        [Required]
        public Guid Id { get; set; }

        public string? CategoryName { get; set; }
    }
}
