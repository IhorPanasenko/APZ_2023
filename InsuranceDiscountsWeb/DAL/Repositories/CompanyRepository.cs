using Core.Models;
using DAL.Interfaces;
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
            catch(Exception e)
            {
                logger.LogError(e.Message);
                return null;
            }
        }

        public Task<bool> Delete(Guid id)
        {
            
        }

        public Task<List<Company>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Company?> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(Company company)
        {
            throw new NotImplementedException();
        }
    }
}
