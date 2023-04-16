using BLL.Interfaces;
using BLL.Services;
using Core.Models;
using InsuranceDiscountsWeb.Managers;
using InsuranceDiscountsWeb.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Configuration;

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
        private readonly ISendGridEmail sendGridEmail;

        public AccountController(
            UserManager<IdentityUser> userManager, 
            SignInManager<IdentityUser> signInManager, 
            ILogger<AccountController> logger, 
            IAccountService accountService, 
            ISendGridEmail sendGridEmail
            )
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.logger = logger;
            this.accountService = accountService;
            this.sendGridEmail = sendGridEmail;
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

            var registerModel = convertModel(registerViewModel);

            try
            {
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

            var loginModel = convertModel(loginViewModel);

            try
            {
                var result =await accountService.LogIn(loginModel);
                return Ok(result);
            }
            catch(Exception e)
            {
                logger.LogError(e.Message);
                return BadRequest("Some Unexcpected errors were found^\n" + e.Message);
            }
        }

        [Authorize]
        [HttpPost("LogOut")]
        public async Task<IActionResult> LogOut()
        {
            try
            {
               await accountService.LogOut();
            }catch(Exception e)
            {
                logger.LogError(e.Message);
            }

            return Ok();
        }

        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel forgotPassword)
        {
            if (!ModelState.IsValid)
            {
                logger.LogError("Model state not valid");
                return NotFound("Model state not valid");
            }

            var user = await userManager.FindByEmailAsync(forgotPassword.Email);

            if(user is null)
            {
                logger.LogError($"No user was found with email{forgotPassword.Email}");
                return NotFound($"No user was found with email{forgotPassword.Email}");
            }

            var code = await userManager.GeneratePasswordResetTokenAsync(user);
            //var callBackUrl = Url.Action

            await sendGridEmail.SendEmailAsync(forgotPassword.Email, "Reset Email confirmation", "Please, Reset your email + Link");

            return Ok(code);
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

        private LoginModel convertModel(LoginViewModel loginViewModel){
            return new LoginModel
            {
                Email = loginViewModel.EmailAddress,
                Password = loginViewModel.Password,
            };
        }

    }
}
