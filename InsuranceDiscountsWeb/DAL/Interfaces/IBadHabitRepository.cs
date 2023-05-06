using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IBadHabitRepository
    {
        public Task<List<BadHabit>> GetAll();

        public Task<BadHabit> GetById(Guid id);

        public Task<BadHabit> Create(BadHabit badHabit);

        public Task<bool> Delete(Guid id);

        public Task<BadHabit> Update(BadHabit badHabit);
    }
}
