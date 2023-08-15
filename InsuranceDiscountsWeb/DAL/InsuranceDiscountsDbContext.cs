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
        public DbSet<AppUser> AppUsers { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Company> Companies { get; set; }

        public DbSet<Agent> Agents { get; set; }

        public DbSet<Policy> Policies { get; set; }

        public DbSet<UserBadHabits> UserBadHabits { get; set; }

        public DbSet<PeriodicMeasurments> PeriodicMeasurments { get; set; }

        public DbSet<StaticMeasurments> StaticMeasurments { get; set; }

        public DbSet<Activity> Activities { get; set; }

        public DbSet<Nutrition> Nutritions { get; set; }

        public DbSet<UserPolicies> UserPolicies { get; set; }

        public DbSet<BadHabit> BadHabits { get; set; }

        public DbSet<AgentRaiting> AgentRaitings { get; set; }

        public InsuranceDiscountsDbContext(DbContextOptions<InsuranceDiscountsDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
