using BLL.Interfaces;
using Core.Models;
using Core.Models.UpdateModels;
using InsuranceDiscountsWeb.ViewModels;
using InsuranceDiscountsWeb.ViewModels.UpdateViewModels;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InsuranceDiscountsWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService companyService;
        private readonly ILogger<CompanyController> logger;

        public CompanyController(
            ICompanyService companyService,
            ILogger<CompanyController> logger
            )
        {
            this.companyService = companyService;
            this.logger = logger;
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var company = await companyService.GetById(id);

                if (company is null)
                {
                    return NotFound($"Cant find compny with id: {id}");
                }

                var companyView = convert(company);
                return Ok(companyView);
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return NotFound(e.Message);
            }
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var companies = await companyService.GetAll();
                var companyViews = convert(companies);
                return Ok(companyViews);
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpGet("SearchByName")]
        public async Task<IActionResult> SearchByName(string searchString)
        {
            try
            {
                var companies = await companyService.SearchByName(searchString);
                var companyViews = convert(companies);
                return Ok(companyViews);
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(CompanyViewModel companyViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Model is not valid. It has errors: " + ModelState.ErrorCount);
            }

            try
            {
                var company = convert(companyViewModel);
                var res = await companyService.Create(company);

                if (res is null)
                {
                    return BadRequest("Cant create company");
                }

                return Ok(res);
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
                var res = await companyService.Delete(id);

                if (!res)
                {
                    return BadRequest($"Cant delete company with id: {id} see logging for more details");
                }

                return Ok($"company with id {id} successfully deleted");
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update(UpdateCompanyViewModel companyViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Model is not valid. It has errors: " + ModelState.ErrorCount);
            }

            try
            {
                var updateCompany = convert(companyViewModel);
                var res = await companyService.Update(updateCompany);

                if (!res)
                {
                    return BadRequest($"Cant update company with if {companyViewModel.Id}");
                }

                return Ok($"Company with id {companyViewModel.Id} updated successfully");
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        private List<CompanyViewModel> convert(List<Company> companies)
        {
            List<CompanyViewModel> result = new List<CompanyViewModel>();

            foreach (var company in companies)
            {
                result.Add(convert(company));
            }

            return result;
        }

        private Company convert(CompanyViewModel companyViewModel)
        {
            return new Company
            {
                Id = companyViewModel.Id ?? Guid.NewGuid(),
                CompanyName = companyViewModel.CompanyName,
                Description = companyViewModel.Description,
                Address = companyViewModel.Address,
                PhoneNumber = companyViewModel.PhoneNumber,
                EmailAddress = companyViewModel.EmailAddress,
                WebsiteAddress = companyViewModel.WebsiteAddress,
                MaxDiscountPercentage = companyViewModel.MaxDiscountPercentage
            };
        }

        private CompanyViewModel convert(Company company)
        {
            return new CompanyViewModel
            {
                Id = company.Id,
                CompanyName = company.CompanyName,
                Description = company.Description,
                Address = company.Address,
                PhoneNumber = company.PhoneNumber,
                EmailAddress = company.EmailAddress,
                MaxDiscountPercentage = company.MaxDiscountPercentage,
                WebsiteAddress = company.WebsiteAddress
            };
        }

        private UpdateCompanyModel convert(UpdateCompanyViewModel updateCompanyView)
        {
            return new UpdateCompanyModel
            {
                Id = updateCompanyView.Id,
                CompanyName = updateCompanyView.CompanyName,
                Description = updateCompanyView.Description,
                Address = updateCompanyView.Address,
                PhoneNumber = updateCompanyView.PhoneNumber,
                EmailAddress = updateCompanyView.EmailAddress,
                WebsiteAddress = updateCompanyView.WebsiteAddress,
                MaxDiscountPercentage = updateCompanyView.MaxDiscountPercentage
            };
        }
    }
}
