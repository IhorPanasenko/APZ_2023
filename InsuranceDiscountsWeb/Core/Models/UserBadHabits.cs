using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class UserBadHabits
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public bool IsSmoking { get; set; } = false;

        [Required]
        public bool IsDrinking { get; set; } = false;

        [ForeignKey("UserId")]
        public Guid UserId { get; set; }

        public AppUser? AppUser { get; set; }
    }
}
