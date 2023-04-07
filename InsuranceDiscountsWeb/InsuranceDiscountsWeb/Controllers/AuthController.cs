using Core.Models;
using DAL.Interfaces;
using DAL.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace InsuranceDiscountsWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IUserRepository _userRepository;
        private IMalRepository mailRepository;   

        public AuthController(IUserRepository userRepository, IMalRepository mailRepository)
        {
            _userRepository = userRepository;
            this.mailRepository = mailRepository;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync([FromBody]RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Some model properties are not valid");
            }

            var result = await _userRepository.RegisterUserAsync(model);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result);  
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Some properties are not valid");
            }

            var result = await _userRepository.LoginUserAsync(model);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            await mailRepository.SendEmailAsync(model.Email, "New Login", "<h1>New login to account</h1><p>LOgin at time" + DateTime.Now + "</p>");
            return Ok(result);
        }

    }
}
