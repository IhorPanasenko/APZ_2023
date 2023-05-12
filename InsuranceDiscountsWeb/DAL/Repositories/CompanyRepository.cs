using Core.Models;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly InsuranceDiscountsDbContext dbContext;
        private readonly ILogger<CompanyRepository> logger;

        public CompanyRepository(
            InsuranceDiscountsDbContext dbContext,
            ILogger<CompanyRepository> logger
            )
        {
            this.dbContext = dbContext;
            this.logger = logger;
        }
        public async Task<Company?> Create(Company company)
        {
            try
            {
                await dbContext.Companies.AddAsync(company);
                await dbContext.SaveChangesAsync();
                return company;
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
                var company = await dbContext.Companies.FindAsync(id);

                if (company is null)
                {
                    throw new Exception($"Can't find company with id {id}");
                }

                dbContext.Companies.Remove(company);
                await dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return false;
            }
        }

        public async Task<List<Company>> GetAll()
        {
            List<Company> companies = new List<Company>();
            try
            {
                companies = await dbContext.Companies.ToListAsync();
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
            }

            return companies;
        }

        public async Task<Company?> GetById(Guid id)
        {
            try
            {
                var company = await dbContext.Companies.FindAsync(id);

                if (company is null)
                {
                    throw new ArgumentException($"Can't find company with id: {id}");
                }

                return company;
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return null;
            }
        }

        public async Task<List<Company>> SearchByName(string searchString)
        {
            List<Company> companies = new List<Company>();

            try
            {
                companies = await dbContext.Companies.Where(c=>c.CompanyName.Contains(searchString)).ToListAsync();
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
            }

            return companies;
        }

        public async Task<bool> Update(Company company)
        {
            try
            {
                dbContext.Update(company);
                await dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return false;
            }
        }
    }
}
