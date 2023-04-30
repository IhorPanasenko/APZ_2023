using BLL.Interfaces;
using Core.Models;
using Core.Models.UpdateModels;
using InsuranceDiscountsWeb.ViewModels;
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
        public async Task<IActionResult> Create(CategoryViewModel categoryViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Model is not valid");
            }

            try
            {
                var category = convert(categoryViewModel);
                var res = await categoryService.Create(category);

                if (!res)
                {
                    return BadRequest("Error's were found. Inspect logging");
                }

                return Ok(categoryViewModel);

            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }



        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var res = await categoryService.Delete(id);

                if (!res)
                {
                    return NotFound("Errors happend. Watch logging");
                }

                return Ok($"category with id {id} was deleted");

            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var categories = await categoryService.GetAll();
                var viewModelsList = convert(categories); 
                return Ok(viewModelsList);
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
                var category = await categoryService.GetById(id);

                if (category is null)
                {
                    return NotFound($"No category with {id} was found");
                }

                var categoryView = convert(category);
                return Ok(categoryView);
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update(UpdateCategoryViewModel categoryViewModel)
        {
            try
            {
                var category = convert(categoryViewModel);
                var res = await categoryService.Update(category);

                if (!res)
                {
                    return NotFound($"Cannot Update category with parameters\nid: {category.Id}\n name: {category.CategoryName}");
                }

                return Ok(res);
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        private IEnumerable<CategoryViewModel> convert(IEnumerable<Category> categories)
        {
            List<CategoryViewModel> viewModelsList = new List<CategoryViewModel>();

            foreach (var category in categories)
            {
                viewModelsList.Add(convert(category));
            }

            return viewModelsList;
        }

        private Category convert(CategoryViewModel categoryViewModel)
        {
            return new Category
            {
                Id = categoryViewModel.Id,
                CategoryName = categoryViewModel.CategoryName,
            };
        }

        private CategoryViewModel convert(Category category)
        {
            return new CategoryViewModel
            {
                Id = category.Id,
                CategoryName = category.CategoryName,
            };
        }

        private UpdateCategoryModel convert (UpdateCategoryViewModel updateCategoryView)
        {
            return new UpdateCategoryModel
            {
                Id = updateCategoryView.Id,
                CategoryName = updateCategoryView.CategoryName
            };
        }
    }
}
