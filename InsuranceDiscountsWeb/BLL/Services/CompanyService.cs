using BLL.Interfaces;
using Core.Models;
using Core.Models.UpdateModels;
using DAL.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository companyRepository;
        private readonly ILogger<CompanyService> logger;

        public CompanyService(
            ICompanyRepository companyRepository,
            ILogger<CompanyService> logger
            )
        {
            this.companyRepository = companyRepository;
            this.logger = logger;
        }
        public Task<Company?> Create(Company company)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Company>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Company?> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(UpdateCompanyModel company)
        {
            throw new NotImplementedException();
        }
    }
}
