using BLL.Interfaces;
using Core.Models;
using DAL.Interfaces;
using DAL.Services;
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
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly ILogger<AccountService> logger;
        private readonly IAccountRepository accountRepository;
        private readonly IConfiguration configuration;
        private readonly ISendGridEmail sendGridEmail;
        private readonly IUserRepository userRepository;

        public AccountService(
            UserManager<AppUser> userManager, 
            SignInManager<AppUser> signInManager, 
            ILogger<AccountService> logger, 
            IAccountRepository accountRepository, 
            IConfiguration configuration,
            ISendGridEmail sendGridEmail,
            IUserRepository userRepository)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.logger = logger;
            this.accountRepository = accountRepository;
            this.configuration = configuration;
            this.sendGridEmail = sendGridEmail;
            this.userRepository = userRepository;
        }

        public async Task<string> LogIn(LoginModel loginModel)
        {
            //var user = await userManager.FindByEmailAsync(loginModel.Email);
            var user = await userRepository.GetUserByEmail(loginModel.Email);

            if (user is null)
            {
                throw new ArgumentException($"User with such email {loginModel.Email} doesn't exist");
            }

            var result = await userManager.CheckPasswordAsync(user, loginModel.Password);            

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

            if(String.IsNullOrEmpty(registerModel.Role))
            {
                registerModel.Role = "User";
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

        public async Task<string> ForgotPassword(string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            

            if (user is null)
            {
                throw new Exception($"No user was found with email {email}");
            }

            var code = await userManager.GeneratePasswordResetTokenAsync(user);

            await sendGridEmail.SendEmailAsync(email, "Reset Email confirmation", $"Please, Reset your email by Link: http://localhost:3000/ResetPassword\nYou will need to paste a code: {code}");  //TODO: Add link to page for reset Password

            return code;
        }
        public async Task<bool> ResetPassword(ResetPasswordModel resetPassword)
        {

            var user = await userManager.FindByEmailAsync(resetPassword.Email);

            if (user is null)
            {
                throw new Exception("No user with this email. You can't reset password");
            }

            if (resetPassword.NewPassword != resetPassword.NewPassword)
            {
                 throw new ApplicationException("Password must match confirmation password");
            }

            var result = await userManager.ResetPasswordAsync(user, resetPassword.Code, resetPassword.NewPassword);

            return result.Succeeded;
        }

        private string generateJwtToken(AppUser user)
        {
            var claims = new[]{
                new Claim("Email", user.Email),
                new Claim ("UserName", user.UserName),
                new Claim("UserId", user.Id),
                new Claim("Role", user.UserRoles.ToList()[0])
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
