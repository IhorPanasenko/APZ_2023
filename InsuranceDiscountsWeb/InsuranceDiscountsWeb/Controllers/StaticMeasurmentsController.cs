using BLL.Interfaces;
using Core.Models.UpdateModels;
using Core.Models;
using InsuranceDiscountsWeb.ViewModels.UpdateViewModels;
using InsuranceDiscountsWeb.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace InsuranceDiscountsWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaticMeasurmentsController : ControllerBase
    {
        private readonly IStaticMeasurmentsService staticMeasurmentsService;
        private readonly ILogger<StaticMeasurmentsController> logger;

        public StaticMeasurmentsController(
            IStaticMeasurmentsService staticMeasurmentsService,
            ILogger<StaticMeasurmentsController> logger
            )
        {
            this.staticMeasurmentsService = staticMeasurmentsService;
            this.logger = logger;
        }

        [HttpGet("GetAll")]
        //[Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var staticMeasurmentss = await staticMeasurmentsService.GetAll();
                convert(staticMeasurmentss);
                return Ok(staticMeasurmentss);
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpGet("GetById")]
        //[Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var staticMeasurments = await staticMeasurmentsService.GetById(id);

                if (staticMeasurments is null)
                {
                    return NotFound($"Can't find staticMeasurments with id {id}");
                }

                convert(staticMeasurments);
                return Ok(staticMeasurments);
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpGet("GetByuserId")]
        //[Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> GetByUserId(Guid userId)
        {
            try
            {
                var staticMeasurmentss = await staticMeasurmentsService.UserStaticMeasurmentss(userId);
                convert(staticMeasurmentss);
                return Ok(staticMeasurmentss);
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpPost("Create")]
        //[Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> Create(StaticMeasurmentsViewModel staticMeasurmentsViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Model is not valid");
                }

                var staticMeasurments = convert(staticMeasurmentsViewModel);
                var newStaticMeasurments = await staticMeasurmentsService.Create(staticMeasurments);
                return Ok(newStaticMeasurments);

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
                var result = await staticMeasurmentsService.Delete(id);

                if (!result)
                {
                    return BadRequest($"Cant delete StaticMeasurments with id {id}");
                }

                return Ok("StaticMeasurments succesfully deleted");
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpPut("Update")]
        //[Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> Update(UpdateStaticMeasurmentsViewModel updateViewModel)
        {
            try
            {
                var updateModel = convert(updateViewModel);
                var result = await staticMeasurmentsService.Update(updateModel);

                if (result is null)
                {
                    return BadRequest("Cant update this staticMeasurments watch logging fo rmore information");
                }

                return Ok(result);
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        private List<StaticMeasurmentsViewModel> convert(List<StaticMeasurments> staticMeasurmentss)
        {
            List<StaticMeasurmentsViewModel> views = new List<StaticMeasurmentsViewModel>();

            foreach (var staticMeasurmentsItem in staticMeasurmentss)
            {
                views.Add(convert(staticMeasurmentsItem));
            }

            return views;
        }

        private StaticMeasurmentsViewModel convert(StaticMeasurments staticMeasurments)
        {
            return new StaticMeasurmentsViewModel
            {
                Id = staticMeasurments.Id,
                Height = staticMeasurments.Height,
                Weight = staticMeasurments.Weight,
                Waist = staticMeasurments.Waist,
                UserId = staticMeasurments.UserId,
                AppUser = staticMeasurments.AppUser,
            };
        }

        private UpdateStaticMeasurmentsModel convert(UpdateStaticMeasurmentsViewModel staticMeasurments)
        {
            return new UpdateStaticMeasurmentsModel
            {
                Id = staticMeasurments.Id,
                Height = staticMeasurments.Height,
                Weight = staticMeasurments.Weight,
                Waist = staticMeasurments.Waist,
            };
        }

        private StaticMeasurments convert(StaticMeasurmentsViewModel staticMeasurments)
        {
            return new StaticMeasurments
            {
                Id = staticMeasurments.Id,
                Height = staticMeasurments.Height,
                Weight = staticMeasurments.Weight,
                Waist = staticMeasurments.Waist,
                UserId = staticMeasurments.UserId,
                AppUser = staticMeasurments.AppUser,
            };
        }
    }
}
