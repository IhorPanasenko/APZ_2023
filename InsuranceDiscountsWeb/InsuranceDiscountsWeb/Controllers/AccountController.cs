using Core.Models;
using InsuranceDiscountsWeb.Managers;
using InsuranceDiscountsWeb.ViewModels;
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

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpPost]
        public async Task<IActionResult> Register (RegisterViewModel registerViewModel)
        {
            if (registerViewModel == null)
            {
                throw new ArgumentNullException("RegisterModel is null");
            }

            if (registerViewModel.Password != registerViewModel.ConfirmPassword)
            {
                return BadRequest("Confirm password doesn't match the password");
            }

            var identityUser = new IdentityUser
            {
                Email = registerViewModel.Email,
                UserName = registerViewModel.Email
            };

            
            var result = await userManager.CreateAsync(identityUser, registerViewModel.Password);

            if (!result.Succeeded)
            {
                return BadRequest("User didn't created");
            }

            await signInManager.SignInAsync(identityUser, isPersistent: false);
            return Ok("User Created successfully");
        }

    }
}
