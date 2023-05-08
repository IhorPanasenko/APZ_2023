using BLL.Interfaces;
using Core.Models;
using Core.Models.UpdateModels;
using InsuranceDiscountsWeb.ViewModels;
using InsuranceDiscountsWeb.ViewModels.UpdateViewModels;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceDiscountsWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly ILogger<UserController> logger;

        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            this.userService = userService;
            this.logger = logger;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var users = await userService.GetAllUsers();
                var userViews = convert(users);
                return Ok(users);
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        
        [HttpGet("GetByEmail")]
        public async Task<IActionResult> GetByEmail(string email)
        {
            try
            {
                var user = await userService.GetUserByEmail(email);

                if(user == null)
                {
                    return NotFound($"Can't find user with Email {email}");
                }

                var userView = convert(user);
                return Ok(user);
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        private AppUserViewModel convert(AppUser user)
        {
            return new AppUserViewModel
            {
                Id = Guid.Parse(user!.Id),
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                FirstName = user.FirstName,
                LastName = user.LastName,
                BirthdayDate = user.BirthdayDate,
                Address = user.Address,
                Roles = user.UserRoles
            };
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(Guid userId)
        {
            try
            {
                var user = await userService.GetUserById(userId);
                return Ok(user);
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteUser(string email)
        {
            try
            {
                return Ok(await userService.DeleteUser(email));
            }
            catch(Exception e)
            {
                logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpPost("Update")]
        public async Task<IActionResult> UpdateUser(UserUpdateViewModel userUpdateViewModel)
        {
            try
            {
                var appUser = convert(userUpdateViewModel);

                if(appUser is null)
                {
                    return BadRequest($"Can't get user with Id {userUpdateViewModel.Id} from Database");
                }

                return Ok(await userService.UpdateUser(appUser));
            }
            catch(Exception e)
            {
                logger.LogError(e.Message);
                return BadRequest(e.Message); 

            }
        }

        private UpdateAppUserModel convert(UserUpdateViewModel userUpdateViewModel)
        {
            return new UpdateAppUserModel
            {
                Id = userUpdateViewModel.Id,
                UserName = userUpdateViewModel.UserName,
                Email = userUpdateViewModel.Email,
                PhoneNumber = userUpdateViewModel.PhoneNumber,
                BirthdayDate = userUpdateViewModel.BirthdayDate,
                FirstName = userUpdateViewModel.FirstName,
                LastName = userUpdateViewModel.LastName,
                Address = userUpdateViewModel.Address
            };
        }

        private List<AppUserViewModel> convert(List<AppUser> users)
        {
            List<AppUserViewModel> result = new List<AppUserViewModel>();

            foreach (var user in users)
            {
                result.Add(convert(user));
            }

            return result;
        }
    }
}
