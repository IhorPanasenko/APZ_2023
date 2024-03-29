﻿using Core.Models;
using Core.Models.UpdateModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ICategoryService
    {
        public Task<bool> Create(Category category);

        public Task<bool> Update(UpdateCategoryModel category);

        public Task<List<Category>> GetAll();

        public Task<bool> Delete(Guid id);

        public Task<Category?> GetById(Guid id);
    }
}
