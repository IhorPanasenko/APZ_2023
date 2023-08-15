using Core.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace InsuranceDiscountsWeb.ViewModels
{
    public class UserBadHabitViewModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid UserId { get; set; }

        public AppUser? AppUser { get; set; }

        public Guid BadHabitId { get; set; }

        public BadHabit? BadHabit { get; set; }
    }
}
