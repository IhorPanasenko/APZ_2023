using Core.Models;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class AgentRaitingRepository : IAgentRaitingRepository
    {
        private readonly ILogger<AgentRaitingRepository> logger;
        private readonly InsuranceDiscountsDbContext dbContext;

        public AgentRaitingRepository(
            ILogger<AgentRaitingRepository> logger,
            InsuranceDiscountsDbContext dbContext
            )
        {
            this.logger = logger;
            this.dbContext = dbContext;
        }

        public async Task AddAgentRaiting(AgentRaiting agentRaiting)
        {
            try
            {
                await dbContext.AgentRaitings.AddAsync(agentRaiting);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
            }
        }

        public async Task<List<AgentRaiting>> GetAgentRaiting(Guid agentId)
        {
            List<AgentRaiting> agentRaitings = new List<AgentRaiting>();

            try
            {
                agentRaitings = await dbContext.AgentRaitings.Where(ar => ar.AgentId == agentId).ToListAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
            }

            return agentRaitings;
        }
    }
}
