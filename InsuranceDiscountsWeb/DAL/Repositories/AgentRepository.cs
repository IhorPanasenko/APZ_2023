﻿using Core.Models;
using Core.Models.UpdateModels;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class AgentRepository : IAgentRepository
    {
        private readonly ILogger<AgentRepository> logger;
        private readonly InsuranceDiscountsDbContext dbContext;
        private readonly IAgentRaitingRepository agentRaitingRepository;

        public AgentRepository(
            ILogger<AgentRepository> logger,
            InsuranceDiscountsDbContext dbContext,
            IAgentRaitingRepository agentRaitingRepository
            )
        {
            this.agentRaitingRepository = agentRaitingRepository;
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
            catch (Exception e)
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
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return false;
            }
        }

        public async Task<List<Agent>> GetAgentsByCompany(Guid companyId)
        {
            List<Agent> agents = new List<Agent>();

            try
            {
                var company = await dbContext.Companies.FindAsync(companyId);

                if (company is null)
                {
                    throw new ArgumentException($"No companies with id: {companyId}");
                }

                var temp_agents = await dbContext.Agents.Where(a => a.CompanyId == companyId).ToListAsync();

                foreach (var agent in temp_agents)
                {
                    agent.Company = company;
                    agents.Add(agent);
                }
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
            }

            return agents;
        }

        public async Task<List<Agent>> GetAll()
        {
            List<Agent> agents = new List<Agent>();

            try
            {
                agents = await dbContext.Agents.ToListAsync();

                foreach (var agent in agents)
                {
                    var raiting = await calculateRaiting(agent.Id);
                    agent.Raiting = raiting;

                    var company = await dbContext.Companies.FindAsync(agent.CompanyId);

                    if (company is not null)
                    {
                        agent.Company = company;
                    }
                }
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
            }

            return agents;
        }

        public async Task<Agent?> GetById(Guid id)
        {
            try
            {
                var agent = await dbContext.Agents.FindAsync(id);

                if (agent is null)
                {
                    throw new Exception($"Can't get Agent with id {id}");
                }

                var raiting = await calculateRaiting(id);
                agent.Raiting = raiting;

                var company = await dbContext.Companies.FindAsync(agent.CompanyId);

                if (company is not null)
                {
                    agent.Company = company;
                }

                return agent;
            }
            catch (Exception e)
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
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return null;
            }
        }

        private async Task<double> calculateRaiting(Guid agentId)
        {
            var raiting = await agentRaitingRepository.GetAgentRaiting(agentId);
            if (raiting.Count == 0)
            {
                return 0;
            }
            else
            {
                return raiting.Average(ar => ar.SingleRaiting);
            }
        }
    }
}
