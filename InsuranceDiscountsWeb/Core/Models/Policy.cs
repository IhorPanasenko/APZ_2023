using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Policy
    {
        [Key]
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

        [ForeignKey("CompanyId")]
        public Guid CompanyId { get; set; }

        public Company? Company { get; set; }

        [ForeignKey("CategoryId")]
        public Guid CategoryId { get; set; }

        public Category? Category { get; set; }
        
    }
}
