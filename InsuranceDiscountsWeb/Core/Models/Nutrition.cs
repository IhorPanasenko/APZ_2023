using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Nutrition
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string Meal { get; set; } = String.Empty;

        [Required]
        public string Food { get; set; } = String.Empty;

        [Required]
        public double Calories { get; set; }

        [Required]
        public double Fat { get; set; }

        [Required]
        public double Protein { get; set; }

        [Required]
        public double Cards { get; set; }

        [ForeignKey("UserId")]
        public Guid UserId { get; set; }

        public AppUser AppUser { get; set; }
    }
}
