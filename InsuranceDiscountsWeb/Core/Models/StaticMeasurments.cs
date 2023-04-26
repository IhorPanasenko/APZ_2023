using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class StaticMeasurments
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public double Height { get; set; }

        [Required]
        public double Weight { get; set; }

        [Required]
        public double Waist { get; set; }

        [Required]
        public DateTime MesurmentDate { get; set; } = DateTime.UtcNow;

        [ForeignKey("UserId")]
        public Guid UserId { get; set; }

        public AppUser? AppUser { get; set; }
    }
}
