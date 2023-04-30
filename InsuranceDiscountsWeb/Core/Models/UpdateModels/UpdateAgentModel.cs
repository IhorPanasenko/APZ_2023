using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.UpdateModels
{
    public class UpdateAgentModel
    {
        [Required]
        public Guid Id { get; set; } = Guid.NewGuid();

        public string? FirstName { get; set; }

        public string? SecondName { get; set; }

        [Phone]
        public string? PhoneNumber { get; set; }

        [EmailAddress]
        public string? EmailAddress { get; set; }

        public double Raiting { get; set; }

        public Guid? CompanyId { get; set; }
    }
}
