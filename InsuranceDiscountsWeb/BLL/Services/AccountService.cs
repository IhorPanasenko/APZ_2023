using BLL.Interfaces;
using Core.Models;
using DAL.Interfaces;
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
    public class AccountService: IAccountService
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly ILogger<AccountService> logger;
        private readonly IAccountRepository accountRepository;

        public AccountService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ILogger<AccountService> logger, IAccountRepository accountRepository)
        {
            this.userManager = userManager;
            this.signInManager = signInManager; 
            this.logger = logger;
            this.accountRepository = accountRepository;
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
                throw new ArgumentException("Passwords are not equal");
            }

            var isUserExist = await userManager.Users.FirstOrDefaultAsync(user => user.Email == registerModel.Email);

            if(isUserExist is not null)
            {
                throw new Exception("User with this Email already exist");
            }

            try
            {
               registerResult = await accountRepository.Register(registerModel);
            }
            catch(Exception e)
            {
                logger.LogError(e.Message);
            }

            return registerResult;
           
        }
    }
}
