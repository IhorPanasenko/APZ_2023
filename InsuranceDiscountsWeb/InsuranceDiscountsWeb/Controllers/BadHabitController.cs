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
    public class BadHabitController : ControllerBase
    {
        private readonly IBadHabitService badHabitService;
        private readonly ILogger<BadHabitController> logger;

        public BadHabitController(
            IBadHabitService badHabitService,
            ILogger<BadHabitController> logger
            )
        {
            this.badHabitService = badHabitService;
            this.logger = logger;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var badHabits = await badHabitService.GetAll();
                var badHabitsView = convert(badHabits);
                return Ok(badHabitsView);
            }
            catch(Exception e)
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
                var badHabit = await badHabitService.GetById(id);

                if(badHabit == null)
                {
                    return NotFound($"Can't find badhabit with id {id}");
                }

                var badHabitView = convert(badHabit);
                return Ok(badHabit);
            }
            catch(Exception e)
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
                var result = await badHabitService.Delete(id);

                if(result == false)
                {
                    return NotFound($"Can't delete badHabit with id {id}. See logging for details");
                }

                return Ok("badHabit deleted");
            }
            catch(Exception e)
            {
                logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(BadHabitViewModel badHabitView)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem("Validation problems were found. Check property of data");
            }

            try
            {
                var badHabit = convert(badHabitView);

                var result = await badHabitService.Create(badHabit);

                if(result is null)
                {
                    return BadRequest("Can't Create BadHabit. See logging for details");
                }

                return Ok(result);
            }
            catch(Exception e)
            {
                logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpPut("Update")]

        public async Task<IActionResult> Update(UpdateBadHabitViewModel updateBadHabitView)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem("validation problems were found. Check data that you are sending");
            }

            try
            {
                var updateBadHabit = convert(updateBadHabitView);
                var result = await badHabitService.Update(updateBadHabit);

                if(result is null)
                {
                    return BadRequest("Some problems happend see logging fir details");
                }

                return Ok(result);
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        private UpdateBadHabitModel convert(UpdateBadHabitViewModel updateBadHabitView)
        {
            return new UpdateBadHabitModel
            {
                Id = updateBadHabitView.Id,
                Name = updateBadHabitView.Name,
                Level = updateBadHabitView.Level
            };
        }

        private BadHabitViewModel convert(BadHabit badHabit)
        {
            return new BadHabitViewModel
            {
                Id = badHabit.Id,
                Name = badHabit.Name,
                Level = badHabit.Level
            };

        }

        private BadHabit convert(BadHabitViewModel badHabitView)
        {
            return new BadHabit
            {
                Id = badHabitView.Id,
                Name = badHabitView.Name,
                Level = badHabitView.Level,
            };
        }

        private List<BadHabitViewModel> convert(List<BadHabit> badHabits)
        {
            List<BadHabitViewModel> badHabitViewModels = new List<BadHabitViewModel>();

            foreach (var badHabit in badHabits)
            {
                badHabitViewModels.Add(convert(badHabit));
            }

            return badHabitViewModels;
        }
    }
}
