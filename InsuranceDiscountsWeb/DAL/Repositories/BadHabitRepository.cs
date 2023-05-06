using Core.Models;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DAL.Repositories
{
    public class BadHabitRepository : IBadHabitRepository
    {
        private readonly InsuranceDiscountsDbContext dbContext;
        private readonly ILogger<BadHabitRepository> logger;

        public BadHabitRepository(
            InsuranceDiscountsDbContext dbContext,
            ILogger<BadHabitRepository> logger
            )
        {
            this.dbContext = dbContext;
            this.logger = logger;
        }
        public async Task<BadHabit?> Create(BadHabit badHabit)
        {
            try
            {
                await dbContext.BadHabits.AddAsync(badHabit);
                await dbContext.SaveChangesAsync();

                var resultHabit = await dbContext.BadHabits.FindAsync(badHabit.Id);

                if (resultHabit is null)
                {
                    throw new Exception("Can't Create new Bad habit now? please? try again later");
                }

                return resultHabit;
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
                var badHabit = dbContext.BadHabits.Find(id);

                if(badHabit is null)
                {
                    throw new Exception("No Bad Habits with id {id} for deleting");
                }

                dbContext.BadHabits.Remove(badHabit);
                await dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return false;
            }
        }

        public async Task<List<BadHabit>> GetAll()
        {
            List<BadHabit> badHabits = new List<BadHabit>();

            try
            {
                badHabits = await dbContext.BadHabits.ToListAsync();
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
            }

            return badHabits;
        }

        public async Task<BadHabit?> GetById(Guid id)
        {
            try
            {
                var badHabit = await dbContext.BadHabits.FindAsync(id);

                if(badHabit == null)
                {
                    throw new Exception($"No Habits with id {id}");
                }

                return badHabit;
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return null;
            }
        }

        public async Task<BadHabit?> Update(BadHabit badHabit)
        {
            try
            {
                dbContext.BadHabits.Update(badHabit);
                await dbContext.SaveChangesAsync();

                var newBadHabit = await dbContext.BadHabits.FindAsync(badHabit.Id);

                if(newBadHabit == null)
                {
                    throw new Exception("unable to update bad habit");
                }

                return newBadHabit;
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return null;
            }
        }
    }
}
