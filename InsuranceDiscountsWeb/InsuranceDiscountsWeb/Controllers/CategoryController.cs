using BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace InsuranceDiscountsWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ILogger<CategoryController> logger;
        private readonly ICategoryService categoryService;

        public CategoryController(
            ILogger<CategoryController> logger,
            ICategoryService categoryService
            
            )
        {
            this.logger = logger;
            this.categoryService = categoryService;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create()
        {

        }

        [HttpPost("Delete")]

        public async Task<IActionResult> Delete(Guid id)
        {

        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try{
                var categories = await categoryService.GetAll();
                return Ok(categories);
            }
            catch(Exception e){
                logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var category = await categoryService.GetById(id);
                return Ok(category);
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return BadRequest(e.Message);
            }

        }


    }
}
