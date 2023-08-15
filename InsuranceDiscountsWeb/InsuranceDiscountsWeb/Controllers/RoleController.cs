using Core.Models;
using DAL;
using InsuranceDiscountsWeb.ViewModels;
using InsuranceDiscountsWeb.ViewModels.UpdateViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InsuranceDiscountsWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly InsuranceDiscountsDbContext dbContext;
        private readonly UserManager<AppUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly ILogger<RoleController> logger;

        public RoleController(
            InsuranceDiscountsDbContext dbContext,
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ILogger<RoleController> logger
            )
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.logger = logger;
        }

        [HttpGet("GetAll")]
        //[Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var roles = await dbContext.Roles.ToListAsync();
                return Ok(roles);
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpGet("GetByName")]
        //[Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> GetByName(string roleName)
        {
            try
            {
                var roles = await dbContext.Roles.Where(r => r.Name == roleName).ToListAsync();
                return Ok(roles);
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }


        [HttpPost("Create")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(RoleViewModel roleViewModel)
        {
            if (!ModelState.IsValid)
            {
                logger.LogError("Model state is Not Valid");
                return BadRequest("Model state is Not Valid");
            }

            try
            {

                var isExist = await roleManager.RoleExistsAsync(roleViewModel.Name);

                if (isExist)
                {
                    logger.LogError($"Role with name {roleViewModel.Name} already exist");
                    return BadRequest($"Role with name {roleViewModel.Name} already exist");
                }

                await roleManager.CreateAsync(convert(roleViewModel));
                await dbContext.SaveChangesAsync();
                return Ok(convert(roleViewModel));
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpPost("Update")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(RoleUpdateViewModel roleViewModel)
        {
            if (!ModelState.IsValid)
            {
                logger.LogError("Model state is Not Valid");
                return BadRequest("Model state is Not Valid");
            }

            try
            {
                var dbRole = await dbContext.Roles.FirstOrDefaultAsync(r => r.Id == roleViewModel.Id);

                if (dbRole is null)
                {
                    logger.LogError($"Role with name {roleViewModel.Name} does not exist");
                    return BadRequest($"Role with name {roleViewModel.Name} does not exist");
                }

                dbRole.Name = roleViewModel.Name;
                dbRole.NormalizedName = roleViewModel.Name.ToUpper();

                await roleManager.UpdateAsync(dbRole);
                await dbContext.SaveChangesAsync();
                return Ok(dbRole);
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("Delete")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var dbRole = await dbContext.Roles.FirstOrDefaultAsync(r => r.Id == id);

                if (dbRole is null)
                {
                    logger.LogError("role doesnt exist");
                    return NotFound("role doesnt exist");
                }

                var usersInRole = dbContext.UserRoles.Where(u => u.RoleId == id).Count();
                if (usersInRole > 0)
                {
                    logger.LogError("Can't delete role while users with such role exists");
                    return BadRequest("Can't delete role while users with such role exists");
                }

                await roleManager.DeleteAsync(dbRole);
                await dbContext.SaveChangesAsync();
                return Ok();

            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        private IdentityRole convert(RoleViewModel roleViewModel)
        {
            return new IdentityRole
            {
                Name = roleViewModel.Name,
            };
        }
    }
}
