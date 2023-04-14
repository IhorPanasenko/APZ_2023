using BLL.Interfaces;
using Core.Models;
using DAL.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly ILogger<AccountService> logger;
        private readonly IAccountRepository accountRepository;
        private readonly IConfiguration configuration;

        public AccountService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ILogger<AccountService> logger, IAccountRepository accountRepository, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.logger = logger;
            this.accountRepository = accountRepository;
            this.configuration = configuration;
        }

        public async Task<string> LogIn(LoginModel loginModel)
        {
            var user = await userManager.FindByEmailAsync(loginModel.Email);

            if (user is null)
            {
                throw new ArgumentException("User with such doesn't exist");
            }

//            var result = await signInManager.PasswordSignInAsync(user.UserName, loginModel.Password, false, false);
            var result = await userManager.CheckPasswordAsync(user, loginModel.Password);

            //if (!result.Succeeded)
            //{
            //    if (result.IsNotAllowed)
            //    {
            //        throw new ApplicationException("Wrong password");
            //    }

            //    throw new Exception("This user is not allowed to enter");
            //}

            if (!result)
            {
                throw new ApplicationException("Wrong password");
            }

            var tokenString = generateJwtToken(user);

            if (string.IsNullOrEmpty(tokenString))
            {
                throw new Exception("Can't generate token");
            }

            return tokenString;
        }

        public async Task LogOut()
        {
            await signInManager.SignOutAsync();
        }

        public async Task<bool> Register(RegisterModel registerModel)
        {
            bool registerResult = false;

            if (registerModel.Password != registerModel.ConfirmPassword)
            {
                throw new ArgumentException("Passwords are not equal");
            }

            var isUserExist = await userManager.Users.FirstOrDefaultAsync(user => user.Email == registerModel.Email);

            if (isUserExist is not null)
            {
                throw new Exception("User with this Email already exist");
            }

            try
            {
                registerResult = await accountRepository.Register(registerModel);
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
            }

            return registerResult;
        }

        private string generateJwtToken(IdentityUser user)
        {
            var claims = new[]{
                new Claim("Email", user.Email),
                new Claim ("UserName", user.UserName),
                new Claim("UserId", user.Id),
            };

            try
            {
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["AuthSettings:Key"]));
                var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                
                var token = new JwtSecurityToken(
                   issuer: configuration["AuthSettings:Issuer"],
                   audience: configuration["AuthSettings:audience"],
                   claims: claims,
                   expires: DateTime.Now.AddDays(30),
                   signingCredentials: credentials
                   );

                string tokenString = new JwtSecurityTokenHandler().WriteToken(token);
                return tokenString;
            }
            catch(Exception e)
            {
                logger.LogError(e.Message);
                return "";
            }
        }
    }
}
