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
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly IConfiguration configuration;
        private readonly ILogger<UserRepository> logger;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly InsuranceDiscountsDbContext dbContext;

        public UserRepository(
            UserManager<IdentityUser> userManager,
            IConfiguration configuration,
            ILogger<UserRepository> logger,
            RoleManager<IdentityRole> roleManager,
            InsuranceDiscountsDbContext dbContext
            )
        {
            this.userManager = userManager;
            this.configuration = configuration;
            this.logger = logger;
            this.roleManager = roleManager;
            this.dbContext = dbContext;
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
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return false;
            }
        }

        public async Task<List<AppUser>> GetAllUsers()
        {
            var users = await dbContext.Users.ToListAsync();
            var userRoles = await dbContext.UserRoles.ToListAsync();
            var roles = await dbContext.Roles.ToListAsync();
            var appUsers = new List<AppUser>();

            for (int i = 0; i < users.Count; i++)
            {
                var oneUserRoles = userRoles.Where(x => x.UserId == users[i].Id).ToList();
                var appUser = convert(users[i]);

                if (oneUserRoles.Count == 0)
                {
                    appUser.UserRoles.Add("None");
                }
                else
                {
                    foreach (var oneRole in oneUserRoles)
                    {
                        var roleName = roles.FirstOrDefault(x => x.Id == oneRole.RoleId)!.Name;
                        appUser.UserRoles.Add(roleName!);
                    }
                }

                appUsers.Add(appUser);
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

        public async Task<bool> UpdateUser(AppUser user)
        {
            try
            {
                var result = await userManager.UpdateAsync(convert(user));

                if (!result.Succeeded)
                {
                    throw new Exception("error in server update");
                }

                await dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
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
