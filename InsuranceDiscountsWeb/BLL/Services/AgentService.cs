using BLL.Interfaces;
using Core.Models;
using Core.Models.UpdateModels;
using DAL.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class AgentService: IAgentService
    {
        private readonly ILogger<AgentService> logger;
        private readonly IAgentRepository agentRepository;

        public AgentService(
            ILogger<AgentService> logger,
            IAgentRepository agentRepository
            )
        {
            this.logger = logger;
            this.agentRepository = agentRepository;
        }

        public Task<Agent?> Create(Agent agent)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Agent>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Agent?> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Agent?> Update(UpdateAgentModel agent)
        {
            throw new NotImplementedException();
        }
    }
}
