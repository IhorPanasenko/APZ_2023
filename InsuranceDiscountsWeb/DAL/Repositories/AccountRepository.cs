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
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly ILogger<AccountRepository> logger;

        public AccountRepository(ILogger<AccountRepository> logger, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.logger = logger;
        }

        public async Task<string> LogIn(LoginModel loginModel)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Register(RegisterModel registerModel)
        {

            var identityUser = new IdentityUser
            {
                Email = registerModel.Email,
                UserName = registerModel.UserName
            };

            try
            {
                var result = await userManager.CreateAsync(identityUser, registerModel.Password);

                if (!result.Succeeded)
                {
                    throw new Exception("Can't create new User");
                }

                return true;
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return false;
            }
        }
    }
}
