using Core.Models;
using DAL.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class UserBadHabitRepository : IUserBadHabitRepository
    {
        private readonly InsuranceDiscountsDbContext dbContext;
        private readonly ILogger<UserBadHabitRepository> logger;

        public UserBadHabitRepository(
            InsuranceDiscountsDbContext dbContext,
            ILogger<UserBadHabitRepository> logger
            )
        {
            this.dbContext = dbContext;
            this.logger = logger;
        }

        public async Task<UserBadHabits?> Create(UserBadHabits userBadHabits)
        {
            try
            {
                await dbContext.UserBadHabits.AddAsync(userBadHabits);
                await dbContext.SaveChangesAsync();

                var res = await dbContext.UserBadHabits.FindAsync(userBadHabits.Id);
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
                var userBadHabit = await dbContext.UserBadHabits.FindAsync(id);

                if (userBadHabit is null)
                {
                    throw new Exception($"Can't find user bad habit with Id: {id}");
                }

                dbContext.UserBadHabits.Remove(userBadHabit);
                await dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return false;
            }
        }

        public async Task<List<UserBadHabits>> GetAll()
        {
            List<UserBadHabits> userBadHabits = new List<UserBadHabits>();

            try
            {
                userBadHabits = await dbContext.UserBadHabits.ToListAsync();

                foreach (var userBad in userBadHabits)
                {
                    var habit = await dbContext.BadHabits.FindAsync(userBad.BadHabitId);
                    var user = await dbContext.AppUsers.FindAsync(userBad.UserId);

                    userBad.BadHabit = habit;
                    userBad.AppUser = user;
                }
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
            }

            return userBadHabits;
        }

        public async Task<List<UserBadHabits>> GetByUser(Guid userId)
        {
            List<UserBadHabits> userBadHabits = new List<UserBadHabits>();

            try
            {
                userBadHabits =await dbContext.UserBadHabits.Where(ubh => ubh.UserId == userId).ToListAsync();

                foreach (var userBad in userBadHabits)
                {
                    var habit = await dbContext.BadHabits.FindAsync(userBad.BadHabitId);
                    var user = await dbContext.AppUsers.FindAsync(userBad.UserId);

                    userBad.BadHabit = habit;
                    userBad.AppUser = user;
                }
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
            }

            return userBadHabits;
        }
    }
}
