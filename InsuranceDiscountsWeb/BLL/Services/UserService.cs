using BLL.Interfaces;
using Core.Models;
using Core.Models.UpdateModels;
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
        private readonly UserManager<AppUser> userManager;

        public UserService(
            ILogger<UserService> logger,
            IConfiguration configuration,
            IUserRepository userRepository,
            UserManager<AppUser> userManager
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

        public async Task<AppUser?> GetUserById(Guid userId)
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

        public async Task<bool> UpdateUser(UpdateAppUserModel updateUser)
        {
            try
            {
                var oldUser = await userRepository.GetUserById(updateUser.Id);

                if(oldUser == null)
                {
                    throw new Exception($"Can't find user with Id {updateUser.Id}");
                }

                update(oldUser, updateUser);
                return await userRepository.UpdateUser(oldUser);
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return false;
            }
        }

        private void update(AppUser oldUser, UpdateAppUserModel updateUser)
        {
            oldUser.UserName = String.IsNullOrEmpty(updateUser.UserName) ? oldUser.UserName : updateUser.UserName;
            oldUser.FirstName = String.IsNullOrEmpty(updateUser.FirstName) ? oldUser.FirstName : updateUser.FirstName;
            oldUser.LastName = String.IsNullOrEmpty(updateUser.LastName) ? oldUser.LastName : updateUser.LastName;
            oldUser.Email = String.IsNullOrEmpty(updateUser.Email) ? oldUser.Email :updateUser.Email;
            oldUser.Address = String.IsNullOrEmpty(updateUser.Address) ? oldUser.Address : updateUser.Address;
            oldUser.PhoneNumber = String.IsNullOrEmpty(updateUser.PhoneNumber) ? oldUser.PhoneNumber : updateUser.PhoneNumber;
        }
    }
}
