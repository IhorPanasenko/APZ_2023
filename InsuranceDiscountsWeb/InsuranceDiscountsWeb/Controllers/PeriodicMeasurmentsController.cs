using BLL.Interfaces;
using Core.Models;
using Core.Models.UpdateModels;
using InsuranceDiscountsWeb.ViewModels;
using InsuranceDiscountsWeb.ViewModels.UpdateViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceDiscountsWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeriodicMeasurmentsController : ControllerBase
    {
        private readonly IPeriodicMeasurmentsService periodicMeasurmentsService;
        private readonly ILogger<PeriodicMeasurmentsController> logger;

        public PeriodicMeasurmentsController(
            IPeriodicMeasurmentsService periodicMeasurmentsService,
            ILogger<PeriodicMeasurmentsController> logger
            )
        {
            this.periodicMeasurmentsService = periodicMeasurmentsService;
            this.logger = logger;
        }

        [HttpGet("GetAll")]
        //[Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var periodicMeasurments = await periodicMeasurmentsService.GetAll();
                convert(periodicMeasurments);
                return Ok(periodicMeasurments);
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
                var periodicMeasurments = await periodicMeasurmentsService.GetById(id);

                if (periodicMeasurments is null)
                {
                    return NotFound($"Can't find periodicMeasurments with id {id}");
                }

                convert(periodicMeasurments);
                return Ok(periodicMeasurments);
            }
            catch (Exception e)
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
                var periodicMeasurments = await periodicMeasurmentsService.UserPeriodicMeasurmentss(userId);
                convert(periodicMeasurments);
                return Ok(periodicMeasurments);
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(PeriodicMeasurmentsViewModel periodicMeasurmentsViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Model is not valid");
                }

                var periodicMeasurments = convert(periodicMeasurmentsViewModel);
                var newPeriodicMeasurments = await periodicMeasurmentsService.Create(periodicMeasurments);
                return Ok(newPeriodicMeasurments);

            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var result = await periodicMeasurmentsService.Delete(id);

                if (!result)
                {
                    return BadRequest($"Cant delete PeriodicMeasurments with id {id}");
                }

                return Ok("PeriodicMeasurments succesfully deleted");
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update(UpdatePeriodicMeasurmentsViewModel updateViewModel)
        {
            try
            {
                var updateModel = convert(updateViewModel);
                var result = await periodicMeasurmentsService.Update(updateModel);

                if (result is null)
                {
                    return BadRequest("Cant update this periodicMeasurments watch logging fo rmore information");
                }

                return Ok(result);
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        private List<PeriodicMeasurmentsViewModel> convert(List<PeriodicMeasurments> periodicMeasurmentss)
        {
            List<PeriodicMeasurmentsViewModel> views = new List<PeriodicMeasurmentsViewModel>();

            foreach (var periodicMeasurmentsItem in periodicMeasurmentss)
            {
                views.Add(convert(periodicMeasurmentsItem));
            }

            return views;
        }

        private PeriodicMeasurmentsViewModel convert(PeriodicMeasurments periodicMeasurments)
        {
            return new PeriodicMeasurmentsViewModel
            {
                Id = periodicMeasurments.Id,
                Pulse = periodicMeasurments.Pulse,
                Glucose = periodicMeasurments.Glucose,
                Cholesterol = periodicMeasurments.Cholesterol,
                BloodPreasure = periodicMeasurments.BloodPreasure,
                MesurmentDate = periodicMeasurments.MesurmentDate,
                UserId = periodicMeasurments.UserId,
                AppUser = periodicMeasurments.AppUser,
            };
        }

        private UpdatePeriodicMeasurmentsModel convert(UpdatePeriodicMeasurmentsViewModel periodicMeasurments)
        {
            return new UpdatePeriodicMeasurmentsModel
            {
                Id = periodicMeasurments.Id,
                MesurmentDate = periodicMeasurments.MesurmentDate,
                Pulse = periodicMeasurments.Pulse,
                Glucose = periodicMeasurments.Glucose,
                Cholesterol = periodicMeasurments.Pulse,
                BloodPreasure = periodicMeasurments.BloodPreasure,
            };
        }

        private PeriodicMeasurments convert(PeriodicMeasurmentsViewModel periodicMeasurments)
        {
            return new PeriodicMeasurments
            {
                Id = periodicMeasurments.Id,
                Pulse = periodicMeasurments.Pulse,
                Glucose = periodicMeasurments.Glucose,
                Cholesterol = periodicMeasurments.Cholesterol,
                BloodPreasure = periodicMeasurments.BloodPreasure,
                MesurmentDate = periodicMeasurments.MesurmentDate,
                UserId = periodicMeasurments.UserId,
                AppUser = periodicMeasurments.AppUser,
            };
        }
    }
}
