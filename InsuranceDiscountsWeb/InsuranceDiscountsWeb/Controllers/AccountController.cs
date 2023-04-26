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
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly ILogger<AccountController> logger;
        private readonly IAccountService accountService;
        private readonly ISendGridEmail sendGridEmail;

        public AccountController(
            UserManager<AppUser> userManager, 
            SignInManager<AppUser> signInManager, 
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

            try
            {
                var code = await accountService.ForgotPassword(forgotPassword.Email);
                return Ok(code);
            }
            catch(Exception e)
            {
                logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody]ResetPasswordViewModel resetPasswordViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Data validation problems check proper of your email and password");
            }

            var resetPassword = convertModel(resetPasswordViewModel);

            try
            {
                var result = await accountService.ResetPassword(resetPassword);

                if (!result)
                {
                    return BadRequest("System server errors found try again later");
                }

                return Ok($"Password successfully changed to {resetPassword.NewPassword}");
                
            }
            catch(Exception e)
            {
                logger.LogError(e.Message);
                return NotFound(e.Message);
            }
        }

        [HttpPost("ExternalLogin")]
        [AllowAnonymous]
        public IActionResult ExternalLogin(string provider, string returnUrl)
        {
            var redirect = Url.Action("ExternalLoginCallBack", "Account", new { ReturnUrl = returnUrl });
            var properties = signInManager.ConfigureExternalAuthenticationProperties(provider, redirect);
            return Challenge(properties, provider);
        }

        private RegisterModel convertModel(RegisterViewModel registerViewModel)
        {
            return new RegisterModel
            {
                Email = registerViewModel.Email,
                UserName = registerViewModel.UserName,
                Password = registerViewModel.Password,
                ConfirmPassword = registerViewModel.ConfirmPassword,
                Role = registerViewModel.Role
            };
        }

        private LoginModel convertModel(LoginViewModel loginViewModel){
            return new LoginModel
            {
                Email = loginViewModel.EmailAddress,
                Password = loginViewModel.Password,
            };
        }

        private ResetPasswordModel convertModel (ResetPasswordViewModel resetPasswordViewModel)
        {
            return new ResetPasswordModel
            {
                Email = resetPasswordViewModel.Email,
                NewPassword = resetPasswordViewModel.NewPassword,
                ConfirmationNewPassword = resetPasswordViewModel.ConfirmationNewPassword,
                Code = resetPasswordViewModel.Code
            };
        }

    }
}
