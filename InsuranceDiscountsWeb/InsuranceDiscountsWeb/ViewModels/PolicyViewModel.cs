using Core.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace InsuranceDiscountsWeb.ViewModels
{
    public class PolicyViewModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string Name { get; set; } = String.Empty;

        [Required]
        public string Description { get; set; } = String.Empty;

        [Required]
        public double CoverageAmount { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public int TimePeriod { get; set; } = 12;

        public Guid CompanyId { get; set; }

        public Company? Company { get; set; }

        public Guid CategoryId { get; set; }

        public Category? Category { get; set; }
    }
}
