using Core.Models;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
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

        public async Task<List<Policy>> Filter(Guid? categoryId = null, Guid? companyId = null, double? minCoverageAmount = null, double? maxCoverageAmount = null, double? minPrice = null, double? maxPrice = null)
        {
            List<Policy> policies = new List<Policy>();

            try
            {
                policies = await dbContext.Policies.ToListAsync();

                if (categoryId != null)
                {
                    policies = policies.Where(p => p.CategoryId == categoryId).ToList();
                }

                if (companyId != null)
                {
                    policies = policies.Where(p => p.CompanyId == companyId).ToList();
                }

                if (minCoverageAmount != null)
                {
                    policies = policies.Where(p => p.CoverageAmount > minCoverageAmount).ToList();
                }

                if (maxCoverageAmount != null)
                {
                    policies = policies.Where(p => p.CoverageAmount < maxCoverageAmount).ToList();
                }

                if (minPrice != null)
                {
                    policies = policies.Where(p => p.Price > minPrice).ToList();
                }

                if (maxPrice != null)
                {
                    policies = policies.Where(p => p.Price < maxPrice).ToList();
                }

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

        public async Task<List<Policy>> GetAll(
            string? searchString = null,
            string? sortParameter = null,
            string? sortDirection = null,
            Guid? categoryId = null,
            Guid? companyId = null,
            double? minCoverageAmount = null,
            double? maxCoverageAmount = null,
            double? minPrice = null,
            double? maxPrice = null
            )
        {
            List<Policy> policies = new List<Policy>();

            try
            {
                if (searchString != null)
                {
                    policies = policies = await dbContext.Policies.Where(p => p.Name.Contains(searchString) || p.Description.Contains(searchString)).ToListAsync();
                }
                else
                {
                    policies = await dbContext.Policies.ToListAsync();
                }

                policies = filterPolicies(policies, categoryId, companyId, minCoverageAmount, maxCoverageAmount, minPrice, maxPrice);
                if (sortParameter != null)
                {
                    policies = sortPolicies(policies, sortParameter, sortDirection);
                }

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

        private List<Policy> filterPolicies(List<Policy> policies, Guid? categoryId, Guid? companyId, double? minCoverageAmount, double? maxCoverageAmount, double? minPrice, double? maxPrice)
        {
            if (categoryId != null)
            {
                policies = policies.Where(p => p.CategoryId == categoryId).ToList();
            }

            if (companyId != null)
            {
                policies = policies.Where(p => p.CompanyId == companyId).ToList();
            }

            if (minCoverageAmount != null)
            {
                policies = policies.Where(p => p.CoverageAmount > minCoverageAmount).ToList();
            }

            if (maxCoverageAmount != null)
            {
                policies = policies.Where(p => p.CoverageAmount < maxCoverageAmount).ToList();
            }

            if (minPrice != null)
            {
                policies = policies.Where(p => p.Price > minPrice).ToList();
            }

            if (maxPrice != null)
            {
                policies = policies.Where(p => p.Price < maxPrice).ToList();
            }

            return policies;
        }

        private List<Policy> sortPolicies(List<Policy> policies, string? sortParameter, string? sortDirection)
        {
            if(sortDirection == null)
            {
                sortDirection = "asc";
            }
            if (sortDirection!.ToLower() == "desc".ToLower())
            {
                if (sortParameter!.ToLower() == "price".ToLower())
                {
                    policies = policies.OrderByDescending(p => p.Price).ToList();
                }
                else
                {
                    if (sortParameter!.ToLower() == "name".ToLower())
                    {
                        policies = policies.OrderByDescending(p => p.Name).ToList();
                    }
                    else
                    {
                        if (sortParameter!.ToLower() == "coverageAmount".ToLower())
                        {
                            policies = policies.OrderByDescending(p => p.CoverageAmount).ToList();
                        }
                    }
                }
            }
            else
            {
                if (sortParameter!.ToLower() == "price".ToLower())
                {
                    policies = policies.OrderBy(p => p.Price).ToList();
                }
                else
                {
                    if (sortParameter!.ToLower() == "name".ToLower())
                    {
                        policies = policies.OrderBy(p => p.Name).ToList();
                    }
                    else
                    {
                        if (sortParameter!.ToLower() == "coverageAmount".ToLower())
                        {
                            policies = policies.OrderBy(p => p.CoverageAmount).ToList();
                        }
                    }
                }
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
