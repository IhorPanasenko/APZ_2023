using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IAgentRaitingService
    {
        public Task<List<AgentRaiting>> GetAgentRaiting(Guid id);

        public Task AddAgentRaiting(AgentRaiting agentRaiting);
    }
}
