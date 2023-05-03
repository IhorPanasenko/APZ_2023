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

        public async Task<Nutrition?> Create(Nutrition nutrition)
        {
            try
            {
                var newNutrition = await nutritionRepository.Create(nutrition);
                return newNutrition;
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
                var res = await nutritionRepository.Delete(id);
                return res;
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
                nutritions = await nutritionRepository.GetAll();
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
                var nutrition = await nutritionRepository.GetById(id);
                return nutrition;
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return null;
            }
        }

        public async Task<Nutrition?> Update(UpdateNutritionModel updateNutrition)
        {
            try
            {
                var oldNutrition = await nutritionRepository.GetById(updateNutrition.Id);

                if(oldNutrition is null)
                {
                    throw new ArgumentException($"No nutrition with id {updateNutrition.Id}");
                }

                update(oldNutrition, updateNutrition);
                var newNutrition = await nutritionRepository.Update(oldNutrition);
                return newNutrition;
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return null;
            }
        }

        private void update(Nutrition oldNutrition, UpdateNutritionModel updateNutrition)
        {
            oldNutrition.Id = updateNutrition.Id;
            oldNutrition.Meal = String.IsNullOrEmpty(updateNutrition.Meal) ? oldNutrition.Meal : updateNutrition.Meal;
            oldNutrition.Food = String.IsNullOrEmpty(updateNutrition.Food) ? oldNutrition.Food : updateNutrition.Food;
            oldNutrition.Calories = updateNutrition.Calories <= 0 ? oldNutrition.Calories : updateNutrition.Calories;
            oldNutrition.Fat = updateNutrition.Fat <= 0 ? oldNutrition.Fat : updateNutrition.Fat;
            oldNutrition.Protein = updateNutrition.Protein <=0 ? oldNutrition.Protein : updateNutrition.Protein;
            oldNutrition.UserId = updateNutrition.UserId == null ? oldNutrition.UserId : (Guid)updateNutrition.UserId;
        }

        public async Task<List<Nutrition>> UserNutritions(Guid userId)
        {
            List<Nutrition> nutritions = new List<Nutrition>();

            try
            {
                nutritions  = await nutritionRepository.GetAll();
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
            }

            return nutritions;
        }
    }
}
