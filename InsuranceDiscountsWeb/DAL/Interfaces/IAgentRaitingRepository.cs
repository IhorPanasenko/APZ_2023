using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IAgentRaitingRepository
    {
        public Task<List<AgentRaiting>> GetAgentRaiting(Guid agentId);

        public Task AddAgentRaiting(AgentRaiting agentRaiting); 
    }
}
