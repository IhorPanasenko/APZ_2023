using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class UserPolicies
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public DateTime StartDate { get; set; } = DateTime.UtcNow;

        [Required] 
        public DateTime EndDate { get; set; } = DateTime.UtcNow;

        [ForeignKey("UserId")]
        public Guid UserId { get; set; }

        public AppUser? AppUser { get; set; }

        [ForeignKey("PolicyId")]
        public Guid PolicyId { get; set; }

        public Policy? Policy { get; set; }
    }
}
