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
    public class UserBadHabitService : IUserBadHabitService
    {
        private readonly IUserBadHabitRepository userBadHabitRepository;
        private readonly ILogger<UserBadHabitService> logger;

        public UserBadHabitService(
            IUserBadHabitRepository userBadHabitRepository,
            ILogger<UserBadHabitService> logger
            )
        {
            this.userBadHabitRepository = userBadHabitRepository;
            this.logger = logger;
        }

        public async Task<UserBadHabits?> Create(UserBadHabits userBadHabits)
        {
            try
            {
               var result =await userBadHabitRepository.Create(userBadHabits);

                if(result == null)
                {
                    throw new Exception($"Can't create userBadHabit now, see logging for more derailds");
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
                var result = await userBadHabitRepository.Delete(id);
                return result;
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
                userBadHabits =await  userBadHabitRepository.GetAll();
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
                userBadHabits = await userBadHabitRepository.GetByUser(userId);
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
            }

            return userBadHabits;
        }
    }
}
