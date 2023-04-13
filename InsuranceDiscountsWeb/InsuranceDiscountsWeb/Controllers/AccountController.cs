using BLL.Interfaces;
using BLL.Services;
using Core.Models;
using InsuranceDiscountsWeb.Managers;
using InsuranceDiscountsWeb.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceDiscountsWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly ILogger<AccountController> logger;
        private readonly IAccountService accountService;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ILogger<AccountController> logger, IAccountService accountService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.logger = logger;
            this.accountService = accountService;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("Register")]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            try
            {
                var registerModel = convertModel(registerViewModel);
                var result = await accountService.Register(registerModel);

                if (!result)
                {
                    return NotFound("For detailed information see logging");
                }

                return Ok("User Created successfully");
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            var result = await signInManager.PasswordSignInAsync(loginViewModel.EmailAddress, loginViewModel.Password, false, false);

            if (!result.Succeeded)
            {
                return BadRequest(result);
            }

            return Ok();
        }

        [HttpPost("LogOut")]
        public async Task<IActionResult> LogOut()
        {
            await signInManager.SignOutAsync();
            return Ok();
        }

        private RegisterModel convertModel(RegisterViewModel registerViewModel)
        {
            return new RegisterModel
            {
                Email = registerViewModel.Email,
                UserName = registerViewModel.UserName,
                Password = registerViewModel.Password,
                ConfirmPassword = registerViewModel.ConfirmPassword,
            };
        }

    }
}
