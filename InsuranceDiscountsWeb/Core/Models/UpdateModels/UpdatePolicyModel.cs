using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.UpdateModels
{
    public class UpdatePolicyModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string? Name { get; set; }

        public string? Description { get; set; }

        public double CoverageAmount { get; set; }

        public double Price { get; set; }

        public int TimePeriod { get; set; }

        public Guid? CompanyId { get; set; }

        public Guid? CategoryId { get; set; }
    }
}
