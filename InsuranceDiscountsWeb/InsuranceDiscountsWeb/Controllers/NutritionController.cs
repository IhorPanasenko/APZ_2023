using BLL.Interfaces;
using Core.Models;
using Core.Models.UpdateModels;
using InsuranceDiscountsWeb.ViewModels;
using InsuranceDiscountsWeb.ViewModels.UpdateViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace InsuranceDiscountsWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NutritionController : ControllerBase
    {
        private readonly INutritionService nutritionService;
        private readonly ILogger<NutritionController> logger;

        public NutritionController(
            INutritionService nutritionService,
            ILogger<NutritionController> logger
            )
        {
            this.nutritionService = nutritionService;
            this.logger = logger;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var nutritions = await nutritionService.GetAll();
                convert(nutritions);
                return Ok(nutritions);
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
                var nutrition = await nutritionService.GetById(id);

                if (nutrition is null)
                {
                    return NotFound($"Can't find nutrition with id {id}");
                }

                convert(nutrition);
                return Ok(nutrition);
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
                var nutritions = await nutritionService.UserNutritions(userId);
                convert(nutritions);
                return Ok(nutritions);
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(NutritionViewModel nutritionViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Model is not valid");
                }

                var nutrition = convert(nutritionViewModel);
                var newNutrition = await nutritionService.Create(nutrition);
                return Ok(newNutrition);

            }
            catch(Exception e)
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
                var result = await nutritionService.Delete(id);

                if (!result)
                {
                    return BadRequest($"Cant delete Nutrition with id {id}");
                }

                return Ok("Nutrition succesfully deleted");
            }
            catch(Exception e)
            {
                logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update(NutritionUpdateViewModel updateViewModel)
        {
            try
            {
                var updateModel = convert(updateViewModel);
                var result = await nutritionService.Update(updateModel);

                if(result is null)
                {
                    return BadRequest("Cant update this nutrition watch logging fo rmore information");
                }

                return Ok(result);
            }
            catch(Exception e)
            {
                logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        private List<NutritionViewModel> convert(List<Nutrition> nutritions)
        {
            List<NutritionViewModel> views = new List<NutritionViewModel>();

            foreach (var nutritionItem in nutritions)
            {
                views.Add(convert(nutritionItem));
            }

            return views;
        }

        private NutritionViewModel convert(Nutrition nutrition)
        {
            return new NutritionViewModel
            {
                Id = nutrition.Id,
                Meal = nutrition.Meal,
                Food = nutrition.Food,
                Calories = nutrition.Calories,
                Fat = nutrition.Fat,
                Protein = nutrition.Protein,
                Cards = nutrition.Cards,
                UserId = nutrition.UserId,
                AppUser = nutrition.AppUser,
            };
        }

        private UpdateNutritionModel convert( NutritionUpdateViewModel nutrition)
        {
            return new UpdateNutritionModel
            {
                Id = nutrition.Id,
                Meal = nutrition.Meal,
                Food = nutrition.Food,
                Calories = nutrition.Calories,
                Fat = nutrition.Fat,
                Protein = nutrition.Protein,
                Cards = nutrition.Cards,
                UserId = nutrition.UserId,
            };
        }

        private Nutrition convert(NutritionViewModel nutrition)
        {
            return new Nutrition
            {
                Id = nutrition.Id,
                Meal = nutrition.Meal,
                Food = nutrition.Food,
                Calories = nutrition.Calories,
                Fat = nutrition.Fat,
                Protein = nutrition.Protein,
                Cards = nutrition.Cards,
                UserId = nutrition.UserId,
                AppUser = nutrition.AppUser,
            };
        }
    }
}
