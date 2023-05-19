using Core.Models;
using DAL.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly ILogger<AccountRepository> logger;
        private readonly RoleManager<IdentityRole> roleManager;

        public AccountRepository(
            ILogger<AccountRepository> logger, 
            SignInManager<AppUser> signInManager, 
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager
            )
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.logger = logger;
            this.roleManager = roleManager;
        }

        public async Task<bool> Register(RegisterModel registerModel)
        {

            var identityUser = new AppUser
            {
                Email = registerModel.Email,
                UserName = registerModel.UserName
            };

            try
            {
                var result = await userManager.CreateAsync(identityUser, registerModel.Password);

                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.ToList()[0].ToString());
                }

                await checkRole(registerModel.Role);

                await userManager.AddToRoleAsync(identityUser, registerModel.Role);

                return true;
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return false;
            }
        }

        private async Task checkRole(string roleName)
        {
          var result = await roleManager.RoleExistsAsync(roleName);

            if (!result)
            {
                await roleManager.CreateAsync(new IdentityRole { Name = roleName});
            }
        }
    }
}
