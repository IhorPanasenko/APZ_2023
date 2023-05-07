using BLL.Interfaces;
using Core.Models;
using DAL.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class UserPolicyService: IUserPolicyService
    {
        private readonly IUserPoliciesRepository userPoliciesRepository;
        private readonly ILogger<UserPolicyService> logger;

        public UserPolicyService(
            IUserPoliciesRepository userPoliciesRepository,
            ILogger<UserPolicyService> logger
            )
        {
            this.userPoliciesRepository = userPoliciesRepository;
            this.logger = logger;
        }

        public async Task<UserPolicies?> Create(UserPolicies userPoliciess)
        {
            try
            {
                var result = await userPoliciesRepository.Create(userPoliciess);

                if (result == null)
                {
                    throw new Exception($"Can't create userPolicies now, see logging for more derailds");
                }

                return result;
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
                var result = await userPoliciesRepository.Delete(id);
                return result;
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return false;
            }
        }

        public async Task<List<UserPolicies>> GetAll()
        {
            List<UserPolicies> userPolicies = new List<UserPolicies>();

            try
            {
                userPolicies = await userPoliciesRepository.GetAll();
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
            }

            return userPolicies;
        }

        public async Task<List<UserPolicies>> GetByUser(Guid userId)
        {
            List<UserPolicies> userPoliciess = new List<UserPolicies>();

            try
            {
                userPoliciess = await userPoliciesRepository.GetByUser(userId);
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
            }

            return userPoliciess;
        }
    }
}
