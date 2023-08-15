using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.UpdateModels
{
    public class UpdateActivityModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string? Type { get; set; }

        public double Duration { get; set; } = -1;

        public double Distance { get; set; } = -1;

        public double Calories { get; set; } = -1;

        public Guid? UserId { get; set; }
    }
}
