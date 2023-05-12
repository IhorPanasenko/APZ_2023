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
        private readonly ICompanyRepository companyRepository;

        public AgentService(
            ILogger<AgentService> logger,
            IAgentRepository agentRepository,
            ICompanyRepository companyRepository
            )
        {
            this.logger = logger;
            this.agentRepository = agentRepository;
            this.companyRepository = companyRepository;
        }

        public async Task<bool> Create(Agent agent)
        {
            try
            {
                var resultAgent =await agentRepository.Create(agent);
                
                if(resultAgent is null)
                {
                    return false;
                }

                return true;
            }
            catch(Exception e)
            {
                logger.LogError(e.Message);
                return false;
            }
        }

        public async Task<bool> Delete(Guid id)
        {
            try
            {
                var result = await agentRepository.Delete(id);
                return result;
            }
            catch(Exception e)
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
                agents = await agentRepository.GetAll();
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
                agents = await agentRepository.GetAll();
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
                var agent = await agentRepository.GetById(id);

                if(agent == null)
                {
                    throw new Exception($"Can't find agent with id: {id}\nSee console logging for more information");
                }

                return agent;
            }
            catch(Exception e)
            {
                logger.LogError(e.Message);
                return null;
            }
        }

        public async Task<Agent?> Update(UpdateAgentModel updateAgentModel)
        {
            try
            {
                var oldAgent = await agentRepository.GetById(updateAgentModel.Id);

                if(oldAgent is null)
                {
                    throw new ArgumentException($"No agents with id {updateAgentModel.Id} was sfound for update");
                }

                await update(oldAgent, updateAgentModel);
                var result = await agentRepository.Update(oldAgent);
                return result;
            }
            catch(Exception e)
            {
                logger.LogError(e.Message);
                return null;
            }
        }

        private async Task update(Agent agent, UpdateAgentModel updateAgentModel)
        {
            agent.FirstName = String.IsNullOrEmpty(updateAgentModel.FirstName) ? agent.FirstName : updateAgentModel.FirstName;
            agent.SecondName = String.IsNullOrEmpty(updateAgentModel.SecondName) ? agent.SecondName : updateAgentModel.SecondName;
            agent.PhoneNumber = String.IsNullOrEmpty(updateAgentModel.PhoneNumber) ? agent.PhoneNumber : updateAgentModel.PhoneNumber;
            agent.EmailAddress = String.IsNullOrEmpty(updateAgentModel.EmailAddress) ? agent.EmailAddress : updateAgentModel.EmailAddress;
            agent.Raiting = updateAgentModel.Raiting < 0 ? agent.Raiting : updateAgentModel.Raiting;

            if (updateAgentModel.CompanyId != null)
            {
                agent.CompanyId = (Guid)updateAgentModel.CompanyId;
                agent.Company = await companyRepository.GetById(updateAgentModel.Id!) ;
            }
        }
    }
}
