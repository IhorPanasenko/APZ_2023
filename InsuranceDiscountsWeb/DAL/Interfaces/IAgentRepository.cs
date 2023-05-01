using Core.Models;
using Core.Models.UpdateModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IAgentRepository
    {
        public Task<Agent?> GetById(Guid id);
        
        public Task<List<Agent>> GetAll();

        public Task<Agent?> Create(Agent agent);

        public Task<bool> Delete(Guid id);

        public Task<Agent?> Update(UpdateAgentModel agent);
    }
}
