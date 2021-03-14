using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Testro.TestingManagement.WebApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class TestProjectController : ControllerBase
    {
        [HttpGet("Projects")]
        public async Task<IActionResult> Get()
        {
            return Ok("ok");
        }
    }
}