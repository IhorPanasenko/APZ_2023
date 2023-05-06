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
    public class BadHabitService : IBadHabitService
    {
        private readonly IBadHabitRepository badHabitRepository;
        private readonly ILogger<BadHabitService> logger;

        public BadHabitService(
            IBadHabitRepository badHabitRepository,
            ILogger<BadHabitService> logger

            )
        {
            this.badHabitRepository = badHabitRepository;
            this.logger = logger;
        }
        public async Task<BadHabit?> Create(BadHabit badHabit)
        {
            try
            {
               var res = await badHabitRepository.Create(badHabit);

                if(res == null)
                {
                    throw new Exception("Can't create Habit now, see Logging");
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
                var res = await badHabitRepository.Delete(id);
                return res;
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return false;
            }
        }

        public async Task<BadHabit?> GetById(Guid id)
        {
            try
            {
                var res =await  badHabitRepository.GetById(id);
                
                if(res == null)
                {
                    throw new Exception($"No category with Id: {id}");
                }

                return res;
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return null;
            }
        }

        public async Task<List<BadHabit>> GetAll()
        {
            List<BadHabit> badHabits = new List<BadHabit>();

            try
            {
                badHabits = await badHabitRepository.GetAll();
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
            }

            return badHabits;
        }

        public async Task<BadHabit?> Update(UpdateBadHabitModel badHabit)
        {
            try
            {
                var oldBadHabit = await badHabitRepository.GetById(badHabit.Id);

                if (oldBadHabit is null)
                {
                    throw new Exception($"No Habits with id {badHabit.Id}");
                }

                update(oldBadHabit, badHabit);
                var res = await badHabitRepository.Update(oldBadHabit);

                if(res == null)
                {
                    throw new Exception("Can't update Habit now, See logging for details");
                }

                return res;
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return null;
            }
        }

        private void update(BadHabit oldBadHabit, UpdateBadHabitModel badHabit)
        {
            oldBadHabit.Id = badHabit.Id;
            oldBadHabit.Name = badHabit.Name == null ? oldBadHabit.Name : badHabit.Name;
            oldBadHabit.Level = badHabit.Level <= 0 ? oldBadHabit.Level : badHabit.Level;
        }
    }
}
