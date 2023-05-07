using Core.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace InsuranceDiscountsWeb.ViewModels
{
    public class UserBadHabitViewModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [ForeignKey("UserId")]
        public Guid UserId { get; set; }

        public AppUser? AppUser { get; set; }

        [ForeignKey("BadHabitId")]
        public Guid BadHabitId { get; set; }

        public BadHabit? BadHabit { get; set; }
    }
}
