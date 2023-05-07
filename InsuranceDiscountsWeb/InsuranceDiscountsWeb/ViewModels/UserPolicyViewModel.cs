using Core.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace InsuranceDiscountsWeb.ViewModels
{
    public class UserPolicyViewModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public DateTime StartDate { get; set; } = DateTime.UtcNow;

        [Required]
        public DateTime EndDate { get; set; } = DateTime.UtcNow.AddDays(30);

        [Required]
        public Guid UserId { get; set; }

        public AppUser? AppUser { get; set; }

        [Required]
        public Guid PolicyId { get; set; }

        public Policy? Policy { get; set; }
    }
}
