using Core.Models;
using Core.Models.UpdateModels;
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
    public class NutritionRepository : INutritionRepository
    {
        private readonly InsuranceDiscountsDbContext dbContext;
        private readonly ILogger<NutritionRepository> logger;

        public NutritionRepository(
            InsuranceDiscountsDbContext dbContext,
            ILogger<NutritionRepository> logger
            )
        {
            this.dbContext = dbContext;
            this.logger = logger;
        }

        public async Task<Nutrition?> Create(Nutrition nutrition)
        {
            try
            {
                await dbContext.AddAsync(nutrition);
                await dbContext.SaveChangesAsync();

                var createdNutrition = await dbContext.Nutritions.FindAsync(nutrition.Id);

                if (createdNutrition is null)
                {
                    throw new ApplicationException("Can't creat nutrition for user now");
                }

                return createdNutrition;
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
                var nutrition = await dbContext.Nutritions.FindAsync(id);

                if (nutrition is null)
                {
                    throw new ArgumentException($"No Nutrition with Id {id} was found");
                }

                dbContext.Nutritions.Remove(nutrition);
                await dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return false;
            }
        }

        public async Task<List<Nutrition>> GetAll()
        {
            List<Nutrition> nutritions = new List<Nutrition>();

            try
            {
                nutritions = await dbContext.Nutritions.ToListAsync();

                foreach(var nutrition in nutritions)
                {
                    await addUserInfoToNutrition(nutrition);
                }    
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
            }

            return nutritions;
        }

        public async Task<Nutrition?> GetById(Guid id)
        {
            try
            {
                var nutrition = await dbContext.Nutritions.FindAsync(id);

                if (nutrition is null)
                {
                    throw new ArgumentException($"Nutrition with id {id} was not found");
                }

                await addUserInfoToNutrition(nutrition);
                return nutrition;
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return null;
            }
        }

        public async Task<Nutrition?> Update(Nutrition updateNutrition)
        {
            try
            {
                dbContext.Nutritions.Update(updateNutrition);
                await dbContext.SaveChangesAsync();

                var nutrition = await dbContext.Nutritions.FindAsync(updateNutrition.Id);

                if (nutrition is null)
                {
                    throw new ArgumentException($"Can't find nutrition with id {updateNutrition.Id} id for updating");
                }

                return nutrition;
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return null;
            }
        }

        public async Task<List<Nutrition>> UserNutritions(Guid userId)
        {
            List<Nutrition> nutritions = new List<Nutrition>();

            try
            {
                nutritions = await dbContext.Nutritions.Where(n => n.UserId == userId).ToListAsync();

                foreach (var nutrition in nutritions)
                {
                    await addUserInfoToNutrition(nutrition);
                }
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
            }

            return nutritions;
        }

        private async Task addUserInfoToNutrition(Nutrition nutrition)
        {
            var user = await dbContext.AppUsers.FindAsync(nutrition.UserId);

            if (user is not null)
            {
                nutrition.AppUser = user;
            }
        }
    }
}
