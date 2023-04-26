using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Agent
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string FirstName { get; set; } = String.Empty;

        [Required]
        public string SecondName { get; set; } = String.Empty;

        [Required]
        [Phone]
        public string PhoneNumber { get; set; } = String.Empty;

        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; } = String.Empty;

        [Required]
        public double Raiting { get; set; }

        [Required]
        [ForeignKey("CompanyId")]
        public Guid CompanyId { get; set; }

        [Required]
        public Company? company { get; set; }
    }
}
