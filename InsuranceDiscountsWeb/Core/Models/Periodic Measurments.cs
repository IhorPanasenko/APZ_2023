using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Periodic_Measurments
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public int Pulse { get; set; }

        [Required]
        public double Glucose { get; set; }

        [Required]
        public double Cholesterol { get; set; }

        [Required]
        public double BloodPreasure { get; set; }

        [Required]
        public DateTime MesurmentDate { get; set; } = DateTime.UtcNow;

        [ForeignKey("UserId")]
        public Guid UserId { get; set; }

        public AppUser? AppUser { get; set; }
    }
}
