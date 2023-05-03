using Core.Models;
using Core.Models.UpdateModels;
using DAL.Interfaces;
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

        public async Task<Nutrition> Create(Nutrition nutrition)
        {
            try
            {

            }
            catch(Exception e)
            {
                logger.LogError(e.Message);
            }
        }

        public async Task<bool> Delete()
        {
            try
            {

            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
            }
        }

        public async Task<List<Nutrition>> GetAll()
        {
            try
            {

            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
            }
        }

        public async Task<Nutrition> GetById(Guid id)
        {
            try
            {

            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
            }
        }

        public async Task<Nutrition> Update(UpdateNutritionModel updateNutrition)
        {
            try
            {

            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
            }
        }

        public async Task<List<Nutrition>> UserNutritions(Guid userId)
        {
            try
            {

            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
            }
        }
    }
}
