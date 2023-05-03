using BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
            try{

            }
            catch(Exception e)
            {
                logger.LogError(e.Message);
            }
        }
    }
}
