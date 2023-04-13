using BLL.Interfaces;
using Core.Models;
using DAL.Interfaces;
using InsuranceDiscountsWeb.Managers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class UserService : IUserService
    {
        //private IUserRepository userRepository;
        private ILogger<UserService> logger;
        private UserManager<IdentityUser> userManager;
        private IConfiguration configuration;

        public UserService(IUserRepository userRepository, ILogger<UserService> logger)
        {
            
            this.logger = logger;
        }

        public async Task<UserManagerResponse> LoginUserAsync(LoginModel loginModel)
        {
            var user = await userManager.FindByEmailAsync(loginModel.Email);

            if (user is null)
            {
                return new UserManagerResponse
                {
                    Message = "No user with this email address",
                    IsSuccess = false,
                };
            }

            var result = await userManager.CheckPasswordAsync(user, loginModel.Password);

            if (!result)
            {
                return new UserManagerResponse
                {
                    Message = "Invalid password",
                    IsSuccess = false,
                };
            }

            var claims = new[]
            {
                new Claim("Email", loginModel.Email),
                new Claim("UserId", user.Id),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["AuthSettings:Key"]));

            var token = new JwtSecurityToken(
                issuer: configuration["AuthSettings:Issuer"],
                audience: configuration["AuthSettings:audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(30),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
                );

            string tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return new UserManagerResponse
            {
                Message = tokenString,
                IsSuccess = true,
                ExpireDate = token.ValidTo
            };

        }

        public async Task<UserManagerResponse> RegisterUserAsync(RegisterModel registerModel)
        {
            if (registerModel == null)
            {
                throw new ArgumentNullException("RegisterModel is null");
            }

            if (registerModel.Password != registerModel.ConfirmPassword)
            {
                return new UserManagerResponse
                {
                    Message = "Confirm password doesn't match the password",
                    IsSuccess = false
                };
            }

            var identityUser = new IdentityUser
            {
                Email = registerModel.Email,
                UserName = registerModel.Email
            };

            var result = await userManager.CreateAsync(identityUser);

            if (!result.Succeeded)
            {
                return new UserManagerResponse
                {
                    Message = "User didn't created",
                    IsSuccess = false,
                    Errors = result.Errors.Select(e => e.Description)
                };
            }

            //var confirmEmailToken = await userManager.GenerateEmailConfirmationTokenAsync(identityUser);
            //var encodedMailToken = Encoding.UTF8.GetBytes(confirmEmailToken);
            //var validEmailToken = WebEncoders.Base64UrlEncode(encodedMailToken);
            //string url = $"{configuration["AppUrl"]}/api/auth/confirmemail&userid={identityUser.Id}&token{validEmailToken}";

            return new UserManagerResponse
            {
                Message = "User created Succefully",
                IsSuccess = true
            };


        }
    }
}
