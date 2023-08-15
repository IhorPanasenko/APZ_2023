using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class LoginModel
    {
        [Required]
        [EmailAddress]
        [StringLength(50)]
        public string? Email { get; set; }

        [Required]
        [StringLength(50, MinimumLength =5)]
        public string? Password { get; set; }

    }
}
