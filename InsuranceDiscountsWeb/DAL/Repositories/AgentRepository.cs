using Core.Models;
using Core.Models.UpdateModels;
using DAL.Interfaces;
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

        public Task<Agent> Create(Agent agent)
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

        public Task<Agent> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Agent> Update(UpdateAgentModel agent)
        {
            throw new NotImplementedException();
        }
    }
}
