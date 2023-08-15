using Core.Models.UpdateModels;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IPolicyService
    {
        public Task<Policy?> GetById(Guid id);

        public Task<List<Policy>> GetAll(string? searchString = null, string? sortParameter = null, string? sortDirection = null, Guid? categoryId = null, Guid? companyId = null, double? minCoverageAmount = null, double? maxCoverageAmount = null, double? minPrice = null, double? maxPrice = null);

        public Task<Policy?> Create(Policy policy);

        public Task<bool> Delete(Guid id);

        public Task<Policy?> Update(UpdatePolicyModel policy);
    }
}
