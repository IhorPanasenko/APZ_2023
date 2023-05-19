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

        public Task<List<Policy>> GetAll();

        public Task<Policy?> Create(Policy policy);

        public Task<bool> Delete(Guid id);

        public Task<Policy?> Update(UpdatePolicyModel policy);
    }
}
