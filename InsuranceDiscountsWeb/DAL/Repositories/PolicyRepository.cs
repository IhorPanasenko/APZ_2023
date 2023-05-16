using Core.Models;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class PolicyRepository : IPolicyRepository
    {
        private readonly ILogger<AgentRepository> logger;
        private readonly InsuranceDiscountsDbContext dbContext;

        public PolicyRepository(ILogger<AgentRepository> logger, InsuranceDiscountsDbContext dbContext)
        {
            this.logger = logger;
            this.dbContext = dbContext;
        }

        public async Task<Policy?> Create(Policy policy)
        {
            try
            {
                await dbContext.Policies.AddAsync(policy);
                await dbContext.SaveChangesAsync();

                var company = await dbContext.Companies.FindAsync(policy.CompanyId);
                policy.Company = company;
                policy.Category = await dbContext.Categories.FindAsync(policy.CategoryId);
                return policy;
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return null;
            }
        }

        public async Task<bool> Delete(Guid id)
        {
            try
            {
                var policy = await dbContext.Policies.FindAsync(id);

                if (policy is null)
                {
                    throw new Exception($"No policy with id {id} was found");
                }

                dbContext.Policies.Remove(policy);
                await dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return false;
            }
        }

        public async Task<List<Policy>> GetAll()
        {
            List<Policy> policies = new List<Policy>();

            try
            {
                policies = await dbContext.Policies.ToListAsync();

                foreach (var policy in policies)
                {
                    var company = await dbContext.Companies.FindAsync(policy.CompanyId);

                    if (company is not null)
                    {
                        policy.Company = company;
                    }

                    var category = await dbContext.Categories.FindAsync(policy.CategoryId);

                    if (category is not null)
                    {
                        policy.Category = category;
                    }
                }
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
            }

            return policies;
        }

        public async Task<Policy?> GetById(Guid id)
        {
            try
            {
                var policy = await dbContext.Policies.FindAsync(id);

                if (policy is null)
                {
                    throw new Exception($"Can't get policy with id {id}");
                }

                var company = await dbContext.Companies.FindAsync(policy.CompanyId);

                if (company is not null)
                {
                    policy.Company = company;
                }

                var category = await dbContext.Categories.FindAsync(policy.CategoryId);

                if (category is not null)
                {
                    policy.Category = category;
                }

                return policy;
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return null;
            }
        }

        public async Task<Policy?> Update(Policy policy)
        {
            try
            {
                dbContext.Policies.Update(policy);
                await dbContext.SaveChangesAsync();
                return policy;
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return null;
            }
        }
    }
}
