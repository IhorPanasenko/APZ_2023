using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceDiscountsWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        public ValuesController()
        {

        }

        [Authorize]
        [HttpGet("GetInfo")]
        public IActionResult Get()
        {
            return Ok("you are here");
        }
    }
}
