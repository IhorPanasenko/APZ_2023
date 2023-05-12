using BLL.Interfaces;
using Core.Models;
using Core.Models.UpdateModels;
using InsuranceDiscountsWeb.ViewModels;
using InsuranceDiscountsWeb.ViewModels.UpdateViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceDiscountsWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgentController : ControllerBase
    {
        private readonly ILogger<AgentController> logger;
        private readonly IAgentService agentService;

        public AgentController(
            ILogger<AgentController> logger,
            IAgentService agentService
            )
        {
            this.logger = logger;
            this.agentService = agentService;
        }

        [HttpGet("GetById")]
        [Authorize]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var agent = await agentService.GetById(id);

                if (agent is null)
                {
                    return NotFound($"No agent with id {id} was found");
                }

                var agentView = convert(agent);
                return Ok(agentView);
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpGet("GetAll")]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var agents = await agentService.GetAll();
                var agentViews = convert(agents);
                return Ok(agentViews);
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpPost("Create")]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> Create(AgentViewModel agentViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Model is not valid");
            }

            try
            {
                var agent = convert(agentViewModel);
                var result = await agentService.Create(agent);

                if (!result)
                {
                    return BadRequest("Can't create this agent");
                }

                return Ok("Agent created Successfully");
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var result = await agentService.Delete(id);

                if (!result)
                {
                    return BadRequest($"Can't delete agent with id {id}");
                }

                return Ok($"Agent with id {id} succesfully deleted");
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpPut("Update")]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> Update(UpdateAgentViewModel updateAgentViewModel)
        {
            try
            {
                var updateModel = convert(updateAgentViewModel);
                var res = await agentService.Update(updateModel);

                if(res is null)
                {
                    return BadRequest($"Can't update agent with Id {updateAgentViewModel.Id}");
                }

                return Ok(res);
            }
            catch(Exception e)
            {
                logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        private Agent convert(AgentViewModel agentViewModel)
        {
            return new Agent
            {
                Id = agentViewModel.Id ?? Guid.NewGuid(),
                FirstName = agentViewModel.FirstName,
                SecondName = agentViewModel.SecondName,
                PhoneNumber = agentViewModel.PhoneNumber,
                EmailAddress = agentViewModel.EmailAddress,
                Raiting = agentViewModel.Raiting,
                CompanyId = agentViewModel.CompanyId
            };
        }

        private object convert(List<Agent> agents)
        {
            List<AgentViewModel> agentViews = new List<AgentViewModel>();

            foreach (var agent in agents)
            {
                agentViews.Add(convert(agent));
            }

            return agentViews;
        }

        private AgentViewModel convert(Agent agent)
        {
            return new AgentViewModel
            {
                Id = agent.Id,
                FirstName = agent.FirstName,
                SecondName = agent.SecondName,
                PhoneNumber = agent.PhoneNumber,
                EmailAddress = agent.EmailAddress,
                Raiting = agent.Raiting,
                CompanyId = agent.CompanyId,
                Company = agent.Company
            };
        }

        private UpdateAgentModel convert(UpdateAgentViewModel agentViewModel)
        {
            return new UpdateAgentModel
            {
                Id = agentViewModel.Id,
                FirstName = agentViewModel.FirstName,
                SecondName = agentViewModel.SecondName,
                PhoneNumber = agentViewModel.PhoneNumber,
                EmailAddress = agentViewModel.EmailAddress,
                Raiting = agentViewModel.Raiting,
                CompanyId = agentViewModel.CompanyId
            };
        }
    }
}
