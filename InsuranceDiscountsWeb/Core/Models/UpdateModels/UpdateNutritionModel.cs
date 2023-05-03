using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.UpdateModels
{
    public class UpdateNutritionModel
    {
        public Guid Id { get; set; }

        public string? Meal { get; set; }

        public string? Food { get; set; } 

        public double Calories { get; set; }

        public double Fat { get; set; }

        public double Protein { get; set; }

        public double Cards { get; set; }

        public Guid? UserId { get; set; }

        public AppUser? AppUser { get; set; }
    }
}
