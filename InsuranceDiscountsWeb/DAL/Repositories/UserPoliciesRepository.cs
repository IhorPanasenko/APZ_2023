﻿using Core.Models;
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
    public class UserPoliciesRepository:IUserPoliciesRepository
    {
        private readonly InsuranceDiscountsDbContext dbContext;
        private readonly ILogger<UserPoliciesRepository> logger;

        public UserPoliciesRepository(
            InsuranceDiscountsDbContext dbContext,
            ILogger<UserPoliciesRepository> logger
            )
        {
            this.dbContext = dbContext;
            this.logger = logger;
        }

        public async Task<UserPolicies?> Create(UserPolicies userPoliciess)
        {
            try
            {
                await dbContext.UserPolicies.AddAsync(userPoliciess);
                await dbContext.SaveChangesAsync();

                var res = await dbContext.UserPolicies.FindAsync(userPoliciess.Id);
                return res;
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
                var userPolicies = await dbContext.UserPolicies.FindAsync(id);

                if (userPolicies is null)
                {
                    throw new Exception($"Can't find user bad habit with Id: {id}");
                }

                dbContext.UserPolicies.Remove(userPolicies);
                await dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return false;
            }
        }

        public async Task<List<UserPolicies>> GetAll()
        {
            List<UserPolicies> userPoliciess = new List<UserPolicies>();

            try
            {
                userPoliciess = await dbContext.UserPolicies.ToListAsync();

                foreach (var userPolicy in userPoliciess)
                {
                    var habit = await dbContext.Policies.FindAsync(userPolicy.PolicyId);
                    var user = await dbContext.AppUsers.FindAsync(userPolicy.UserId);

                    userPolicy.Policy = habit;
                    userPolicy.AppUser = user;
                }
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
            }

            return userPoliciess;
        }

        public async Task<List<UserPolicies>> GetByUser(Guid userId)
        {
            List<UserPolicies> userPoliciess = new List<UserPolicies>();

            try
            {
                userPoliciess = await dbContext.UserPolicies.Where(ubh => ubh.UserId == userId).ToListAsync();

                foreach (var userPolicy in userPoliciess)
                {
                    var habit = await dbContext.Policies.FindAsync(userPolicy.PolicyId);
                    var user = await dbContext.AppUsers.FindAsync(userPolicy.UserId);

                    userPolicy.Policy = habit;
                    userPolicy.AppUser = user;
                }
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
            }

            return userPoliciess;
        }
    }
}
