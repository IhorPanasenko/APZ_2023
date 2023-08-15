using BLL.Interfaces;
using BLL.Services;
using Core.Models;
using Core.Models.UpdateModels;
using InsuranceDiscountsWeb.ViewModels;
using InsuranceDiscountsWeb.ViewModels.UpdateViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceDiscountsWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PolicyController : ControllerBase
    {
        private readonly ILogger<PolicyController> logger;
        private readonly IPolicyService policyService;

        public PolicyController(
            ILogger<PolicyController> logger,
            IPolicyService policyService
            )
        {
            this.logger = logger;
            this.policyService = policyService;
        }

        [HttpGet("GetById")]
        //[Authorize]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var policy = await policyService.GetById(id);

                if (policy is null)
                {
                    return NotFound($"No agent with id {id} was found");
                }

                var policyView = convert(policy);
                return Ok(policyView);
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpGet("GetAll")]
        ///[Authorize]
        public async Task<IActionResult> GetAll(
            string? searchString = null,
            string? sortParameter = null,
            string? sortDirection = null, 
            Guid? categoryId = null,
            Guid? companyId = null, 
            double? minCoverageAmount = null, 
            double? maxCoverageAmount = null, 
            double? minPrice = null, 
            double? maxPrice = null
            )
        {
            try
            {
                var policies = await policyService.GetAll(searchString, sortParameter, sortDirection, categoryId, companyId, minCoverageAmount, maxCoverageAmount, minPrice, maxPrice);
                var policiesViews = convert(policies);
                return Ok(policiesViews);
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpPost("Create")]
        //[Authorize(Roles = "Admin, Manager, User")]
        public async Task<IActionResult> Create(PolicyViewModel policyViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Model is not valid");
            }

            try
            {
                var policy = convert(policyViewModel);
                var result = await policyService.Create(policy);

                if (result is null)
                {
                    return BadRequest("Can't create this policy");
                }

                return Ok(result);
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("Delete")]
        //[Authorize(Roles = "Admin, Manager, User")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var result = await policyService.Delete(id);

                if (!result)
                {
                    return BadRequest($"Can't delete policy with id {id}");
                }

                return Ok($"Policy with id {id} succesfully deleted");
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpPut("Update")]
        //[Authorize(Roles = "Admin, Manager, User")]
        public async Task<IActionResult> Update(UpdatePolicyViewModel updatePolicyViewModel)
        {
            try
            {
                var updateModel = convert(updatePolicyViewModel);
                var res = await policyService.Update(updateModel);

                if (res is null)
                {
                    return BadRequest($"Can't update agent with Id {updatePolicyViewModel.Id}");
                }

                return Ok(res);
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        private UpdatePolicyModel convert(UpdatePolicyViewModel updatePolicyViewModel)
        {
            return new UpdatePolicyModel
            {
                Id = updatePolicyViewModel.Id,
                Name = updatePolicyViewModel.Name,
                Description = updatePolicyViewModel.Description,
                CoverageAmount = updatePolicyViewModel.CoverageAmount,
                Price = updatePolicyViewModel.Price,
                TimePeriod = updatePolicyViewModel.TimePeriod,
                CompanyId = updatePolicyViewModel.CompanyId,
                CategoryId = updatePolicyViewModel.CategoryId,
            };
        }

        private Policy convert(PolicyViewModel policyViewModel)
        {
            return new Policy
            {
                Id = policyViewModel.Id,
                Name = policyViewModel.Name,
                Description = policyViewModel.Description,
                CoverageAmount = policyViewModel.CoverageAmount,
                Price = policyViewModel.Price,
                TimePeriod = policyViewModel.TimePeriod,
                CompanyId = policyViewModel.CompanyId,
                Company = policyViewModel.Company,
                CategoryId = policyViewModel.CategoryId,
                Category = policyViewModel.Category
            };
        }

        private List<PolicyViewModel> convert(List<Policy> policies)
        {
            List<PolicyViewModel> policyViewModels = new List<PolicyViewModel>();

            foreach (var policy in policies)
            {
                policyViewModels.Add(convert(policy));
            }

            return policyViewModels;
        }

        private PolicyViewModel convert(Policy policy)
        {
            return new PolicyViewModel
            {
                Id = policy.Id,
                Name = policy.Name,
                Description = policy.Description,
                CoverageAmount = policy.CoverageAmount,
                Price = policy.Price,
                TimePeriod = policy.TimePeriod,
                CompanyId = policy.CompanyId,
                Company = policy.Company,
                CategoryId = policy.CategoryId,
                Category = policy.Category
            };
        }
    }
}
