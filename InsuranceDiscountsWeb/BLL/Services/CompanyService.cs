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
        public async Task<Company?> Create(Company company)
        {
            try
            {
                var res =await companyRepository.Create(company);

                if(res is null)
                {
                    throw new Exception("No category was created see Console Logging");
                }

                return res;
            }
            catch(Exception e)
            {
                logger.LogError(e.Message);
                return null;
            }
        }

        public async Task<bool> Delete(Guid id)
        {
            try
            {
                var result = await companyRepository.Delete(id);
                return result;
            }
            catch(Exception e)
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
                companies = await companyRepository.GetAll();

            }
            catch(Exception e)
            {
                logger.LogError(e.Message);
            }

            return companies;
        }

        public async Task<Company?> GetById(Guid id)
        {
            try
            {
                var company = await companyRepository.GetById(id);
                return company;
            }
            catch(Exception e)
            {
                logger.LogError(e.Message);
                return null;
            }
        }

        public async Task<bool> Update(UpdateCompanyModel updateCompany)
        {
            try
            {
                var oldCompany = await companyRepository.GetById(updateCompany.Id);

                if(oldCompany is null)
                {
                    throw new ArgumentException($"No company with id {updateCompany.Id}");
                }

                update(oldCompany, updateCompany);
                var res = await companyRepository.Update(oldCompany);
                return res;
            }
            catch(Exception e)
            {
                logger.LogError(e.Message);
                return false;
            }
        }

        private void update(Company oldCompany, UpdateCompanyModel updateCompany) 
        {
            oldCompany.Id = updateCompany.Id;
            oldCompany.CompanyName = String.IsNullOrEmpty(updateCompany.CompanyName) ? oldCompany.CompanyName: updateCompany.CompanyName;
            oldCompany.Description = String.IsNullOrEmpty(updateCompany.Description) ? oldCompany.Description : updateCompany.Description;
            oldCompany.Address = String.IsNullOrEmpty(updateCompany.Address) ? oldCompany.Address : updateCompany.Address;
            oldCompany.PhoneNumber = String.IsNullOrEmpty (updateCompany.PhoneNumber) ? oldCompany.PhoneNumber : updateCompany.PhoneNumber;
            oldCompany.EmailAddress = String.IsNullOrEmpty(updateCompany.EmailAddress) ? oldCompany.EmailAddress : updateCompany.EmailAddress;
            oldCompany.WebsiteAddress = String.IsNullOrEmpty(updateCompany.WebsiteAddress) ? oldCompany.WebsiteAddress : updateCompany.WebsiteAddress; 
        }
    }
}
