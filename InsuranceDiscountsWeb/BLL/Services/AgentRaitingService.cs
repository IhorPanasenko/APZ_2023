using BLL.Interfaces;
using Core.Models;
using DAL.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class AgentRaitingService : IAgentRaitingService
    {
        private readonly ILogger<AgentRaitingService> logger;
        private readonly IAgentRaitingRepository agentRaitingRepository;

        public AgentRaitingService(
            ILogger<AgentRaitingService> logger,
            IAgentRaitingRepository agentRaitingRepository
            )
        {
            this.logger = logger;
            this.agentRaitingRepository = agentRaitingRepository;
        }
        public async Task AddAgentRaiting(AgentRaiting agentRaiting)
        {
            try
            {
                await agentRaitingRepository.AddAgentRaiting(agentRaiting);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
            }
        }

        public async Task<List<AgentRaiting>> GetAgentRaiting(Guid agentId)
        {
            List<AgentRaiting> agentRaitingList = new List<AgentRaiting>();

            try
            {
                agentRaitingList = await agentRaitingRepository.GetAgentRaiting(agentId);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
            }

            return agentRaitingList;
        }
    }
}
