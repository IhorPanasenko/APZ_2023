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
    public class NutritionService : INutritionService
    {
        private readonly INutritionRepository nutritionRepository;
        private readonly ILogger<NutritionService> logger;

        public NutritionService(
            INutritionRepository nutritionRepository,
            ILogger<NutritionService> logger
            )
        {
            this.nutritionRepository = nutritionRepository;
            this.logger = logger;
        }

        public Task<Nutrition?> Create(Nutrition nutrition)
        {
            try
            {

            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
            }
        }

        public Task<bool> Delete(Guid id)
        {
            try
            {

            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
            }
        }

        public Task<List<Nutrition>> GetAll()
        {
            try
            {

            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
            }
        }

        public Task<Nutrition?> GetById(Guid id)
        {
            try
            {

            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
            }
        }

        public Task<Nutrition?> Update(UpdateNutritionModel updateNutrition)
        {
            try
            {

            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
            }
        }

        public Task<List<Nutrition>> UserNutritions(Guid userId)
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
