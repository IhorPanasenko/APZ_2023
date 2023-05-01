using BLL.Interfaces;
using Core.Models;
using InsuranceDiscountsWeb.ViewModels;
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
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var agent = await agentService.GetById(id);

                if(agent is null)
                {
                    return NotFound($"No agent with id {id} was found");  
                }

                return Ok(agent);
            }
            catch(Exception e)
            {
                logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var agents = await agentService.GetAll();
                return Ok(agents);
            }
            catch(Exception e)
            {
                logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(AgentViewModel agentViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Model is not valid");
            }

            try
            {
                var agent = convert(agentViewModel);
                var result = agentService.Create(agent);

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

            }
        }
    }
}
