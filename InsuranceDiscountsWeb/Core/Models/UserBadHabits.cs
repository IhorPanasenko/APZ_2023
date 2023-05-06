using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models
{
    public class UserBadHabits
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [ForeignKey("UserId")]
        public Guid UserId { get; set; }

        public AppUser? AppUser { get; set; }

        [ForeignKey("BadHabitId")]
        public Guid BadHabitId { get; set; }

        public BadHabit? BadHabit { get; set; }
    }
}
