using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class AppUser :IdentityUser
    {
        public List<string> UserRoles { get; set; } = new List<string>();
    }
}
