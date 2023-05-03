using BLL.Interfaces;
using Core.Models.UpdateModels;
using Core.Models;
using InsuranceDiscountsWeb.ViewModels.UpdateViewModels;
using InsuranceDiscountsWeb.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Activity = Core.Models.Activity;

namespace InsuranceDiscountsWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivityController : ControllerBase
    {
        private readonly IActivityService activityService;
        private readonly ILogger<ActivityController> logger;

        public ActivityController(
            IActivityService activityService,
            ILogger<ActivityController> logger
            )
        {
            this.activityService = activityService;
            this.logger = logger;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var activities = await activityService.GetAll();
                convert(activities);
                return Ok(activities);
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var Activity = await activityService.GetById(id);

                if (Activity is null)
                {
                    return NotFound($"Can't find Activity with id {id}");
                }

                convert(Activity);
                return Ok(Activity);
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpGet("GetByuserId")]
        public async Task<IActionResult> GetByUserId(Guid userId)
        {
            try
            {
                var activities = await activityService.UserActivitys(userId);
                convert(activities);
                return Ok(activities);
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(ActivityViewModel activityViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Model is not valid");
                }

                var Activity = convert(activityViewModel);
                var newActivity = await activityService.Create(Activity);
                return Ok(newActivity);

            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("Deelte")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var result = await activityService.Delete(id);

                if (!result)
                {
                    return BadRequest($"Cant delete Activity with id {id}");
                }

                return Ok("Activity succesfully deleted");
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update(UpdateActivityViewModel updateViewModel)
        {
            try
            {
                var updateModel = convert(updateViewModel);
                var result = await activityService.Update(updateModel);

                if (result is null)
                {
                    return BadRequest("Cant update this Activity watch logging fo rmore information");
                }

                return Ok(result);
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        private List<ActivityViewModel> convert(List<Activity> Activitys)
        {
            List<ActivityViewModel> views = new List<ActivityViewModel>();

            foreach (var ActivityItem in Activitys)
            {
                views.Add(convert(ActivityItem));
            }

            return views;
        }

        private ActivityViewModel convert(Activity activity)
        {
            return new ActivityViewModel
            {
                Id = activity.Id,
                Type = activity.Type,
                Duration = activity.Duration,
                Distance = activity.Distance,
                Calories = activity.Calories,
                UserId  =  activity.UserId,
                AppUser = activity.AppUser
            };
        }

        private UpdateActivityModel convert(UpdateActivityViewModel activity)
        {
            return new UpdateActivityModel
            {
                Id = activity.Id,
                Type = activity.Type,
                Duration = activity.Duration,
                Distance = activity.Distance,
                Calories = activity.Calories,
                UserId = activity.UserId,
            };
        }

        private Activity convert(ActivityViewModel activity)
        {
            return new Activity
            {
                Id = activity.Id,
                Type = activity.Type,
                Duration = activity.Duration,
                Distance = activity.Distance,
                Calories = activity.Calories,
                UserId = activity.UserId,
            };
        }
    }
}
