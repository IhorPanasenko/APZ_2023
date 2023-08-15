using BLL.Interfaces;
using Core.Models;
using InsuranceDiscountsWeb.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceDiscountsWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserBadHabitController : ControllerBase
    {
        private readonly IUserBadHabitService userBadHabitService;
        private readonly ILogger<UserBadHabitController> logger;

        public UserBadHabitController(
            IUserBadHabitService userBadHabitService,
            ILogger<UserBadHabitController> logger
            )
        {
            this.userBadHabitService = userBadHabitService;
            this.logger = logger;
        }

        [HttpGet("GetAll")]
        //[Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var res = await userBadHabitService.GetAll();
                var view = convert(res);

                return Ok(view);
            }
            catch(Exception e)
            {
                logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpGet("GetByUserId")]
        //[Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> GetByUserId(Guid userId)
        {
            try
            {
                var res = await userBadHabitService.GetByUser(userId);
                var views = convert(res);
                return Ok(views);
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpPost("Create")]
        //[Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> Create(UserBadHabitViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem("Validation problems in data were found");
            }

            try
            {
                var userBad = convert(viewModel);
                var res =  await userBadHabitService.Create(userBad);

                if(res is null)
                {
                    return BadRequest("Can't create Bad habit for user now, see logging");
                }

                return Ok(res);
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("Delete")]
        //[Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var res = await userBadHabitService.Delete(id);

                if(res == false)
                {
                    return Problem("Can't delete bad habit for user, See logging");
                }

                return Ok("UserBadHabit deleted succesfully");
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        private UserBadHabits convert(UserBadHabitViewModel viewModel)
        {
            return new UserBadHabits
            {
                Id = viewModel.Id,
                UserId = viewModel.UserId,
                BadHabitId = viewModel.BadHabitId,
                AppUser = viewModel.AppUser,
                BadHabit = viewModel.BadHabit
            };
        }
        private List<UserBadHabitViewModel> convert(List<UserBadHabits> res)
        {
            List<UserBadHabitViewModel> views = new List<UserBadHabitViewModel>();

            foreach (var item in res)
            {
                views.Add(convert(item));
            }

            return views;
        }

        private UserBadHabitViewModel convert(UserBadHabits userBadHabits)
        {
            return new UserBadHabitViewModel
            {
                Id = userBadHabits.Id,
                UserId = userBadHabits.UserId,
                BadHabitId = userBadHabits.BadHabitId,
                AppUser = userBadHabits.AppUser,
                BadHabit = userBadHabits.BadHabit
            };
        }
    }
}
