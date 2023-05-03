using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IActivityRepository
    {
        public Task<Activity?> Create(Activity activity);

        public Task<Activity?> GetById(Guid id);

        public Task<List<Activity>> GetAll();

        public Task<List<Activity>> UserActivities(Guid userId);

        public Task<bool> Delete(Guid id);

        public Task<Activity?> Update(Activity updateActivity);
    }
}
