using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IUserPoliciesRepository
    {
        public Task<UserPolicies?> Create(UserPolicies userPolicies);

        public Task<bool> Delete(Guid id);

        public Task<List<UserPolicies>> GetAll();

        public Task<List<UserPolicies>> GetByUser(Guid userId);

        public Task<List<UserPolicies>> GetUsersByCompany(Guid companyId);

    }
}
