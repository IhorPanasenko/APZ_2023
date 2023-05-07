using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class AppUser :IdentityUser
    {
        public string FirstName { get; set; } = String.Empty;

        public string LastName { get; set; } = String.Empty;

        public string Address { get; set; } = String.Empty;

        [Range(0,50)]
        public double Discount { get; set; } = 0;

        [NotMapped]
        public List<string> UserRoles { get; set; } = new List<string>();
    }
}
