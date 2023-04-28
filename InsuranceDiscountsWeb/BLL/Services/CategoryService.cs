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
    public class CategoryService : ICategoryService
    {
        private readonly ILogger<CategoryService> logger;
        private readonly ICategoryRepository categoryRepository;

        public CategoryService(
            ILogger<CategoryService> logger,
            ICategoryRepository categoryRepository
            )
        {
            this.logger = logger;
            this.categoryRepository = categoryRepository;
        }
        public async Task<bool> Create(Category category)
        {
            try
            {
                var res = await categoryRepository.Create(category);
                return res;
            }
            catch(Exception e)
            {
                logger.LogError(e.Message);
                return false;
            }
        }

        public async Task<bool> Delete(Guid id)
        {
            try
            {
                var res = await categoryRepository.Delete(id);
                return res;
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return false;
            }
        }

        public async Task<List<Category>> GetAll()
        {
            try
            {
                var categories = await categoryRepository.GetAll();
                return categories;
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return new List<Category>();
            }
        }

        public async Task<Category?> GetById(Guid id)
        {
            try
            {
                var res = await categoryRepository.GetById(id);
                return res;
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return null;
            }
        }

        public async Task<bool> Update(Category category)
        {
            try
            {
                var res = await categoryRepository.Update(category);
                return res;
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return false;
            }
        }
    }
}
