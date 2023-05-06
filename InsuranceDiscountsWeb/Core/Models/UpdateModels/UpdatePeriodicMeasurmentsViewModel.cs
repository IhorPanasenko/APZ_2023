using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.UpdateModels
{
    public class UpdatePeriodicMeasurmentsViewModel
    {
        [Required]
        public Guid Id { get; set; } = Guid.NewGuid();

        public int Pulse { get; set; } = -1;

        public double Glucose { get; set; } = -1;

        public double Cholesterol { get; set; } = -1;

        public double BloodPreasure { get; set; } = -1;
        public DateTime? MesurmentDate { get; set; }
    }
}
