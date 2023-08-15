using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Activity
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string Type { get; set; } = String.Empty;

        [Required]
        public double Duration { get; set; }

        [Required]
        public double Distance { get; set; }

        [Required]
        public double Calories { get; set; }

        [ForeignKey("UserId")]
        public Guid UserId { get; set; }

        public AppUser? AppUser { get; set; }
    }
}
