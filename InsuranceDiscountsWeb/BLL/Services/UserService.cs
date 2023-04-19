using BLL.Interfaces;
using Core.Models;
using DAL.Interfaces;
using DAL.Services;
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
        private readonly ILogger<UserService> logger;
        private readonly IConfiguration configuration;
        private readonly IUserRepository userRepository;
        private readonly UserManager<IdentityUser> userManager;

        public UserService(
            ILogger<UserService> logger,
            IConfiguration configuration,
            IUserRepository userRepository,
            UserManager<IdentityUser> userManager
            )
        {
            this.logger = logger;
            this.configuration = configuration;
            this.userRepository = userRepository;
            this.userManager = userManager;
        }

        public async Task<bool> DeleteUser(string email)
        {
            try
            {
                var user = await userManager.FindByEmailAsync(email);

                if (user is null)
                {
                    throw new Exception($"No user with email {email}");
                }

                var result = await userRepository.DeleteUser(user);
                return result;

            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return false;
            }
        }

        public async Task<List<AppUser>> GetAllUsers()
        {
            var users = new List<AppUser>();

            try
            {
                users = await userRepository.GetAllUsers();
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
            }

            return users;
        }

        public async Task<AppUser?> GetUserByEmail(string email)
        {
            try
            {
                var user = await userRepository.GetUserByEmail(email);
                return user;
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return null;
            }
        }

        public async Task<AppUser?> GetUserById(string userId)
        {
            try
            {
                var user = await userRepository.GetUserById(userId);
                return user;
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return null;
            }
        }

        public async Task<bool> UpdateUser(string userId, AppUser user)
        {
            try
            {
              return await userRepository.UpdateUser(userId, user);    
            }
            catch(Exception e)
            {
                logger.LogError(e.Message);
                return false;
            }
        }
    }
}
