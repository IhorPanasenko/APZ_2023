using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IUserBadHabitService
    {
        public Task<UserBadHabits?> Create(UserBadHabits userBadHabits);

        public Task<bool> Delete(Guid id);

        public Task<List<UserBadHabits>> GetAll();

        public Task<List<UserBadHabits>> GetByUser(Guid userId);
    }
}
