using Core.Models;
using DAL.Interfaces;
using DAL.Repositories;
using InsuranceDiscountsWeb.Managers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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

namespace DAL.Services
{
    public class UserRepository: IUserRepository
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly IConfiguration configuration;
        private readonly ILogger<UserRepository> logger;
        private readonly RoleManager<IdentityRole> roleManager;
        public UserRepository(
            UserManager<IdentityUser> userManager, 
            IConfiguration configuration, 
            ILogger<UserRepository> logger,
            RoleManager<IdentityRole> roleManager
            )
        {
            this.userManager = userManager;
            this.configuration = configuration;
            this.logger = logger; 
            this.roleManager = roleManager;
        }

        public async Task<bool> DeleteUser(IdentityUser user)
        {
            try
            {
                var result = await userManager.DeleteAsync(user);

                if (result.Succeeded)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch(Exception e)
            {
                logger.LogError(e.Message);
                return false;
            }
        }

        public async Task<List<AppUser>> GetAllUsers()
        {
            List<AppUser> appUsers = new List<AppUser>();
            var roles = await roleManager.Roles.ToListAsync();

            for (int i = 0; i < roles.Count; ++i)
            {
                var usersInRole = await userManager.GetUsersInRoleAsync(roles[i].Name);
                for(int j =0; j < usersInRole.Count; j++)
                {
                    var user = convert(usersInRole[j]);
                    if (!appUsers.Contains(user))
                    {
                        appUsers.Add(user);
                    }
                    else
                    {
                        appUsers[appUsers.IndexOf(user)].UserRoles.Add(roles[i].Name);
                    }
                }
            }

            return appUsers;
        }

        public async Task<AppUser?> GetUserByEmail(string email)
        {
            try
            {
                var identityUser = await userManager.FindByEmailAsync(email);

                if (identityUser is null)
                {
                    throw new Exception($"No user with email {email}");
                }

                var roles = await userManager.GetRolesAsync(identityUser);
                var user = convert(identityUser);
                user.UserRoles.AddRange(roles);

                return user;
            }
            catch(Exception e)
            {
                logger.LogError(e.Message);
                return null;
            }
        }

        public async Task<AppUser?> GetUserById(string userId)
        {
            try
            {
                var identityUser = await userManager.FindByIdAsync(userId);

                if (identityUser is null)
                {
                    throw new Exception($"No user with Id {userId}");
                }

                var roles = await userManager.GetRolesAsync(identityUser);
                var user = convert(identityUser);
                user.UserRoles.AddRange(roles);

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
               var result = await userManager.UpdateAsync(convert(user));

                if (!result.Succeeded)
                {
                    throw new Exception("error in server update");
                }

                return true;
            }
            catch(Exception e)
            {
                logger.LogError(e.Message);
                return false;
            }
        }

        private AppUser convert(IdentityUser identityUser)
        {
            return new AppUser
            {
                UserName = identityUser.UserName,
                Email = identityUser.Email,
                Id = identityUser.Id,
                PhoneNumber = identityUser.PhoneNumber,
            };
        }

        private IdentityUser convert(AppUser appUser)
        {
            return new IdentityUser
            {
                UserName = appUser.UserName,
                Email = appUser.Email,
                Id = appUser.Id,
                PhoneNumber = appUser.PhoneNumber,
            };
        }
    }
}
