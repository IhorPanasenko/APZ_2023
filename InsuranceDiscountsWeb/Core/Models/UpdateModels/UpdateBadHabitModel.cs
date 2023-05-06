using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.UpdateModels
{
    public class UpdateBadHabitModel
    {
        [Required]
        public Guid Id { get; set; }

        public string? Name { get; set; }

        [Range(0, 100)]
        public int Level { get; set; } = -1;
    }
}
