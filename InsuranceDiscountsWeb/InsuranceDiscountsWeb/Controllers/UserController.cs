using BLL.Interfaces;
using Core.Models;
using InsuranceDiscountsWeb.ViewModels.UpdateViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

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
                return Ok(user);
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(string userId)
        {
            try
            {
                var user = await userService.GetUserByEmail(userId);
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
        public async Task<IActionResult> UpdateUser(string userId, UserUpdateViewModel userUpdateViewModel)
        {
            try
            {
                var appUser = convert(userId, userUpdateViewModel);

                if(appUser is null)
                {
                    return BadRequest($"Can't get user with Id {userId} from Database");
                }

                return Ok(await userService.UpdateUser(appUser));
            }
            catch(Exception e)
            {
                logger.LogError(e.Message);
                return BadRequest(e.Message); 

            }
        }



        //private AppUser? convert(string userId, UserUpdateViewModel userUpdateViewModel)
        //{
        //    try
        //    {
        //        var user = userService.GetUserById(userId);

        //        if (user == null)
        //        {
        //            throw new Exception($"Can't get user with Id {userId} from database");
        //        }

        //        return new AppUser
        //        {
        //            Id = userId,
        //            FirstName = userUpdateViewModel.FirstName,
        //            LastName = userUpdateViewModel.LastName,
        //            Email = userUpdateViewModel.Email,
        //            PhoneNumber = userUpdateViewModel.PhoneNumber,
        //            UserName = userUpdateViewModel.UserName
        //        };
        //    }
        //    catch(Exception e)
        //    {
        //        logger.LogError(e.Message);
        //        return null;
        //    }
        //}
    }
}
