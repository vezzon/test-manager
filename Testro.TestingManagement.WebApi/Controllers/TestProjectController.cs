using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Testro.TestingManagement.WebApi.Models;
using Testro.TestingManagement.WebApi.Services;

namespace Testro.TestingManagement.WebApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class TestProjectController : ControllerBase
    {
        private readonly TestProjectService _service;

        public TestProjectController(TestProjectService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var projects = await _service.GetAsync();
            return Ok(projects);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var project = await _service.GetAsync(id);
            if (project is null)
                return NotFound();
            return Ok(project);
        }
        
        [HttpGet("{name}")]
        public async Task<IActionResult> Get(string name)
        {
            var project = await _service.GetAsync(name);
            if (project is null)
                return NotFound();
            return Ok(project);
        }
    }
}