using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.UpdateModels
{
    public class UpdateStaticMeasurmentsModel
    {
        [Required]
        public Guid Id { get; set; }

        public double Height { get; set; } = -1;

        public double Weight { get; set; } = -1;

        public double Waist { get; set; } = -1;

        public DateTime? MesurmentDate { get; set; } = DateTime.UtcNow;
    }
}
