using Core.Models;
using DAL.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly InsuranceDiscountsDbContext dbContext;
        private readonly ILogger<CategoryRepository> logger;

        public CategoryRepository(
            InsuranceDiscountsDbContext dbContext,
            ILogger<CategoryRepository> logger)
        {
            this.dbContext = dbContext;
            this.logger = logger;
        }
        public async Task<bool> Create(Category category)
        {
            try
            {
                await dbContext.Categories.AddAsync(category);
                await dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return false;
            }
        }

        public async Task<bool> Delete(Guid id)
        {
            try
            {
                var user = await dbContext.Users.FindAsync(id);

                if(user == null)
                {
                    throw new Exception($"No user with Id {id}");
                }

                dbContext.Remove(user);
                await dbContext.SaveChangesAsync();
                return true;

            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return false;
            }
        }

        public async Task<List<Category>> GetAll()
        {
            List<Category> categories = new List<Category>();

            try
            {
                categories.AddRange(dbContext.Categories.AsEnumerable());
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
            }

            return categories;
        }

        public async Task<Category?> GetById(Guid id)
        {
            try
            {
                Category? category = await dbContext.Categories.FindAsync(id);

                if (category is null)
                {
                    throw new Exception($"No categories with id {id} was found");  
                } 

                return category;

            }
            catch(Exception e)
            {
                logger.LogError(e.Message);
                return null;
            }
        }

        public async Task<bool> Update(Category category)
        {
            try
            {
                dbContext.Categories.Update(category);
                await dbContext.SaveChangesAsync();
                return true;
            }
            catch(Exception e)
            {
                logger.LogError(e.Message);
                return false;
            }
        }
    }
}
