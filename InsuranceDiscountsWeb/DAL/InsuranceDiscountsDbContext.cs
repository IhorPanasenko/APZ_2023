using Core.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class InsuranceDiscountsDbContext : IdentityDbContext
    {
        public InsuranceDiscountsDbContext(DbContextOptions options) : base (options)
        {
                
        }

        public DbSet<User> Users { get; set; }
    }
}
