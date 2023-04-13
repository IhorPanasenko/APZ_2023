using BLL.Interfaces;
using Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly ILogger<AccountService> logger;    

        public AccountService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ILogger<AccountService> logger)
        {
            this.userManager = userManager;
            this.signInManager = signInManager; 
            this.logger = logger;
        }

        public async Task<string> LogIn(LoginModel loginModel)
        {
            throw new NotImplementedException();
        }

        public async Task LogOut()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Register(RegisterModel registerModel)
        {
            bool registerResult = false;

            if (registerModel.Password != registerModel.ConfirmPassword)
            {
                //logger.LogError("Validation Error password are not equal");
                throw new ArgumentException("Passwords are not equal");
            }

            var isUserExist = await userManager.Users.FirstOrDefaultAsync(user => user.Email == registerModel.Email);

            if(isUserExist is not null)
            {
                //logger.LogError("User with this Email already exist");
                throw new Exception("User with this Email already exist");
            }

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

                await signInManager.SignInAsync(identityUser, isPersistent: false);
                registerResult = true; 
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
            }

            return registerResult;
           
        }
    }
}
