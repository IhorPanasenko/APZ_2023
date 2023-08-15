using BLL.Interfaces;
using Core.Models;
using Core.Models.UpdateModels;
using DAL.Interfaces;
using DAL.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class PolicyService : IPolicyService
    {
        private readonly ILogger<AgentService> logger;
        private readonly IPolicyRepository policyRepository;
        private readonly ICompanyRepository companyRepository;
        private readonly ICategoryRepository categoryRepository;
        public PolicyService(
            ILogger<AgentService> logger,
            IPolicyRepository policyRepository,
            ICompanyRepository companyRepository
,
            ICategoryRepository categoryRepository)
        {
            this.logger = logger;
            this.policyRepository = policyRepository;
            this.companyRepository = companyRepository;
            this.categoryRepository = categoryRepository;
        }

        public async Task<Policy?> Create(Policy policy)
        {
            try
            {
                var resultPolicy = await policyRepository.Create(policy);

                if (resultPolicy is null)
                {
                    throw new Exception("No policy was create in DAL layer see logging");
                }

                return resultPolicy;
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
                var result = await policyRepository.Delete(id);
                return result;
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return false;
            }
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
                policies = await policyRepository.GetAll(searchString, sortParameter, sortDirection, categoryId, companyId, minCoverageAmount, maxCoverageAmount, minPrice, maxPrice);
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
                var policy = await policyRepository.GetById(id);

                if (policy == null)
                {
                    throw new Exception($"Can't find policy with id: {id}\nSee console logging for more information");
                }

                return policy;
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return null;
            }
        }

        public async Task<Policy?> Update(UpdatePolicyModel updatePolicy)
        {
            try
            {
                var oldPolicy = await policyRepository.GetById(updatePolicy.Id);

                if (oldPolicy is null)
                {
                    throw new ArgumentException($"No policy with id {updatePolicy.Id} was found for update");
                }

                await update(oldPolicy, updatePolicy);
                var result = await policyRepository.Update(oldPolicy);
                return result;
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return null;
            }
        }

        private async Task update(Policy oldPolicy, UpdatePolicyModel updatePolicy)
        {
            oldPolicy.Name = String.IsNullOrEmpty(updatePolicy.Name) ? oldPolicy.Name : updatePolicy.Name;
            oldPolicy.Description = String.IsNullOrEmpty(updatePolicy.Description) ? oldPolicy.Description : updatePolicy.Description;
            oldPolicy.CoverageAmount = updatePolicy.CoverageAmount <= 0 ? oldPolicy.CoverageAmount : updatePolicy.CoverageAmount;
            oldPolicy.Price = updatePolicy.Price <= 0 ? oldPolicy.Price : updatePolicy.Price;
            oldPolicy.TimePeriod = updatePolicy.TimePeriod <= 0 ? oldPolicy.TimePeriod : updatePolicy.TimePeriod;

            if (updatePolicy.CompanyId != null)
            {
                oldPolicy.CompanyId = (Guid)updatePolicy.CompanyId;
                oldPolicy.Company = await companyRepository.GetById((Guid)updatePolicy.CompanyId);
            }

            if (updatePolicy.CategoryId != null)
            {
                oldPolicy.CategoryId = (Guid)updatePolicy.CategoryId;
                oldPolicy.Category = await categoryRepository.GetById((Guid)updatePolicy.CategoryId);
            }
        }
    }
}
