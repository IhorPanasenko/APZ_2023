using Core.Models;
using Core.Models.UpdateModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface INutritionRepository
    {
        public Task<Nutrition?> Create(Nutrition nutrition);

        public Task<Nutrition?> GetById(Guid id);

        public Task<List<Nutrition>> GetAll();
        
        public Task<List<Nutrition>> UserNutritions(Guid userId);

        public Task<bool> Delete(Guid id);

        public Task<Nutrition?> Update(Nutrition updateNutrition);
    }
}
