using Core.Models.UpdateModels;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IActivityService
    {
        public Task<Activity?> Create(Activity Activity);

        public Task<Activity?> GetById(Guid id);

        public Task<List<Activity>> GetAll();

        public Task<List<Activity>> UserActivitys(Guid userId);

        public Task<bool> Delete(Guid id);

        public Task<Activity?> Update(UpdateActivityModel updateActivity);
    }
}
