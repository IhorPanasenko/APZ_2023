using BLL.Interfaces;
using Core.Models;
using DAL.Interfaces;
using InsuranceDiscountsWeb.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceDiscountsWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgentRaitingController : ControllerBase
    {
        private readonly ILogger<AgentRaitingController> logger;
        private readonly IAgentRaitingService agentRaitingService;

        public AgentRaitingController(
            ILogger<AgentRaitingController> logger,
            IAgentRaitingService agentRaitingService
            )
        {
            this.logger = logger;
            this.agentRaitingService = agentRaitingService;
        }

        [HttpGet("GetAgentRaiting")]
        public async Task<IActionResult> GetAgentRaiting(Guid id)
        {
            try
            {
                var res = await agentRaitingService.GetAgentRaiting(id);
                List<AgentRaitingViewModel> viewModels = new List<AgentRaitingViewModel>();

                foreach (var item in res)
                {
                    viewModels.Add(convert(item));
                }


                return Ok(res);
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        private AgentRaitingViewModel convert(AgentRaiting item)
        {
            return new AgentRaitingViewModel
            {
                Id = item.Id,
                AgentId = item.AgentId,
                SingleRaiting = item.SingleRaiting
            };
        }

        [HttpPost("Create")]
        public async Task<IActionResult> AddAgentRaiting(AgentRaitingViewModel agentRaitingViewModel)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem("Model is not valid");
            }

            try
            {
                var agentRaiting = convert(agentRaitingViewModel);
                await agentRaitingService.AddAgentRaiting(agentRaiting);
                return Ok("Raiting added to Agent");
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        private AgentRaiting convert(AgentRaitingViewModel agentRaitingViewModel)
        {
            return new AgentRaiting
            {
                Id = agentRaitingViewModel.Id,
                AgentId = agentRaitingViewModel.AgentId,
                SingleRaiting = agentRaitingViewModel.SingleRaiting
            };
        }
    }
}
