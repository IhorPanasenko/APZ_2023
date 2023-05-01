using Core.Models;
using Core.Models.UpdateModels;
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
    public class AgentRepository:IAgentRepository
    {
        private readonly ILogger<AgentRepository> logger;
        private readonly InsuranceDiscountsDbContext dbContext;

        public AgentRepository(
            ILogger<AgentRepository> logger,
            InsuranceDiscountsDbContext dbContext
            )
        {
            this.logger = logger;
            this.dbContext = dbContext;
        }

        public async Task<Agent?> Create(Agent agent)
        {
            try
            {
                await dbContext.Agents.AddAsync(agent);
                await dbContext.SaveChangesAsync();

                var company = await dbContext.Companies.FindAsync(agent.CompanyId);
                agent.Company = company;
                return agent;
            }
            catch(Exception e)
            {
                logger.LogError(e.Message);
                return null;
            }

        }

        public async Task<bool> Delete(Guid id)
        {
            try
            {
               var agent = await dbContext.Agents.FindAsync(id);

                if (agent is null)
                {
                    throw new Exception($"No agent with id {id} was found");
                }

                dbContext.Agents.Remove(agent);
                await dbContext.SaveChangesAsync();
                return true;
            }
            catch(Exception e)
            {
                logger.LogError(e.Message);
                return false;
            }
        }

        public async Task<List<Agent>> GetAll()
        {
            List<Agent> agents = new List<Agent>();

            try
            {
                 agents =await dbContext.Agents.ToListAsync();
            }
            catch(Exception e)
            {
                logger.LogError(e.Message);
            }

            return agents;
        }

        public async Task<Agent?> GetById(Guid id)
        {
            try
            {
                var agent =await dbContext.Agents.FindAsync(id);

                if(agent is null)
                {
                    throw new Exception("Can't create new Agents");
                }

                return agent;
            }
            catch(Exception e)
            {
                logger.LogError(e.Message);
                return null;
            }
        }

        public async Task<Agent?> Update(Agent agent)
        {
            try
            {
                dbContext.Agents.Update(agent);
                await dbContext.SaveChangesAsync();
                return agent;
            }  
            catch(Exception e)
            {
                logger.LogError(e.Message);
                return null;
            }
        }
    }
}
