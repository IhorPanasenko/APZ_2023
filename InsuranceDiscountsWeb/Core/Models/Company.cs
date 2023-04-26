using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Company
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string CompanyName { get; set; } = String.Empty;

        [Required]
        public string Description { get; set; } = String.Empty;

        [Required]
        public string Address { get; set; } = String.Empty;

        [Required]
        [Phone]
        public string PhoneNumber { get; set; } = String.Empty;

        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; } = String.Empty;

        [Required]
        [Url]
        public string WebsiteAddress { get; set; } = String.Empty;
    }
}
