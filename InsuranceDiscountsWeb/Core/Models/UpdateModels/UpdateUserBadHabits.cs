using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.UpdateModels
{
    public class UpdateUserBadHabits
    {
        [Required]
        public Guid Id { get; set; }

        public bool? IsSmoking { get; set; }

        public bool? IsDrinking { get; set; }

    }
}
