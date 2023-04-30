using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.UpdateModels
{
    public class UpdateCompanyModel
    {
        [Required]
        public Guid Id { get; set; } = Guid.NewGuid();

        public string? CompanyName { get; set; }

        public string? Description { get; set; }

        public string? Address { get; set; } 

        [Phone]
        public string? PhoneNumber { get; set; }

        [EmailAddress]
        public string? EmailAddress { get; set; } 

        [Url]
        public string? WebsiteAddress { get; set; }
    }
}
