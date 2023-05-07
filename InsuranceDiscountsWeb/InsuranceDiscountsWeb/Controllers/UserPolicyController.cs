using BLL.Interfaces;
using Core.Models;
using InsuranceDiscountsWeb.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceDiscountsWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserPolicyController : ControllerBase
    {
        private readonly IUserPolicyService userPoliciesService;
        private readonly ILogger<UserPolicyController> logger;

        public UserPolicyController(
            IUserPolicyService userPoliciesService,
            ILogger<UserPolicyController> logger
            )
        {
            this.userPoliciesService = userPoliciesService;
            this.logger = logger;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var res = await userPoliciesService.GetAll();
                var view = convert(res);

                return Ok(view);
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpGet("GetByUserId")]
        public async Task<IActionResult> GetByUserId(Guid userId)
        {
            try
            {
                var res = await userPoliciesService.GetByUser(userId);
                var views = convert(res);
                return Ok(views);
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(UserPolicyViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem("Validation problems in data were found");
            }

            try
            {
                var userBad = convert(viewModel);
                var res = await userPoliciesService.Create(userBad);

                if (res is null)
                {
                    return BadRequest("Can't create Bad habit for user now, see logging");
                }

                return Ok(res);
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
                var res = await userPoliciesService.Delete(id);

                if (res == false)
                {
                    return Problem("Can't delete bad habit for user, See logging");
                }

                return Ok("UserPolicies deleted succesfully");
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        private UserPolicies convert(UserPolicyViewModel viewModel)
        {
            return new UserPolicies
            {
                Id = viewModel.Id,
                UserId = viewModel.UserId,
                PolicyId = viewModel.PolicyId,
                AppUser = viewModel.AppUser,
                Policy = viewModel.Policy
            };
        }
        private List<UserPolicyViewModel> convert(List<UserPolicies> res)
        {
            List<UserPolicyViewModel> views = new List<UserPolicyViewModel>();

            foreach (var item in res)
            {
                views.Add(convert(item));
            }

            return views;
        }

        private UserPolicyViewModel convert(UserPolicies userPoliciess)
        {
            return new UserPolicyViewModel
            {
                Id = userPoliciess.Id,
                UserId = userPoliciess.UserId,
                PolicyId = userPoliciess.PolicyId,
                AppUser = userPoliciess.AppUser,
                Policy = userPoliciess.Policy
            };
        }
    }
}
