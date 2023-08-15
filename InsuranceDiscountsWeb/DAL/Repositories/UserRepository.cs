using Core.Models;
using DAL.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;


namespace DAL.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<AppUser> userManager;
        private readonly IConfiguration configuration;
        private readonly ILogger<UserRepository> logger;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly InsuranceDiscountsDbContext dbContext;

        public UserRepository(
            UserManager<AppUser> userManager,
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

        public async Task<bool> DeleteUser(AppUser user)
        {
            try
            {
                var habits = await dbContext.UserBadHabits.Where(ubh => ubh.UserId.ToString() == user.Id).ToListAsync();
                var policies = await dbContext.UserPolicies.Where(up => up.UserId.ToString() == user.Id).ToListAsync();

                dbContext.UserBadHabits.RemoveRange(habits);
                dbContext.UserPolicies.RemoveRange(policies);
                dbContext.AppUsers.Remove(user);

                await dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return false;
            }
        }

        public async Task<List<AppUser>> GetAllUsers()
        {
            var users = await dbContext.AppUsers.ToListAsync();
            var userRoles = await dbContext.UserRoles.ToListAsync();
            var roles = await dbContext.Roles.ToListAsync();

            for (int i = 0; i < users.Count; i++)
            {
                var oneUserRoles = userRoles.Where(x => x.UserId == users[i].Id).ToList();

                if (oneUserRoles.Count == 0)
                {
                    users[i].UserRoles.Add("None");
                }
                else
                {
                    foreach (var oneRole in oneUserRoles)
                    {
                        var roleName = roles.FirstOrDefault(x => x.Id == oneRole.RoleId)!.Name;
                        users[i].UserRoles.Add(roleName!);
                    }
                }
            }

            return users;
        }

        public async Task<AppUser?> GetUserByEmail(string email)
        {
            try
            {
                var user = await userManager.FindByEmailAsync(email);

                if (user is null)
                {
                    throw new Exception($"No user with email {email}");
                }

                var roles = await userManager.GetRolesAsync(user);
                user.UserRoles.AddRange(roles);

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
                var user = await userManager.FindByIdAsync(userId.ToString());

                if (user is null)
                {
                    throw new Exception($"No user with Id {userId}");
                }

                var roles = await userManager.GetRolesAsync(user);
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
                var result = await userManager.UpdateAsync(user);

                if (!result.Succeeded)
                {
                    throw new Exception("error in server update" + result.Errors.ToList()[0].ToString());
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
    }
}
