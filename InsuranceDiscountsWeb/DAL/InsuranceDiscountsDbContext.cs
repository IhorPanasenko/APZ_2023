using Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class InsuranceDiscountsDbContext : IdentityDbContext<IdentityUser>
    {
        public InsuranceDiscountsDbContext(DbContextOptions<InsuranceDiscountsDbContext> options) : base (options)
        {
                
        }

        public DbSet<User> Users { get; set; }
    }
}
