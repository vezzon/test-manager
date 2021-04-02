using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Testro.TestingManagement.WebApi.Models;
using Testro.TestingManagement.WebApi.Services;

namespace Testro.TestingManagement.WebApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class TestProjectsController : ControllerBase
    {
        private readonly TestProjectService _service;

        public TestProjectsController(TestProjectService service)
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
        
        [HttpPost]
        public async Task<IActionResult> Create(TestProject project)
        {
            await _service.AddAsync(project);
            return Ok(project);
        }
        
        [HttpPost("{projectId}/TestScenarios")]
        public async Task<IActionResult> CreateTestScenario(int projectId, TestScenario scenario)
        {
            await _service.AddTestScenarioAsync(projectId, scenario);
            return Ok(scenario);
        }
        
        [HttpPost("{projectId}/TestScenarios/{scenarioId}/TestCases")]
        public async Task<IActionResult> CreateTestCase(int projectId, int scenarioId, TestCase testCase)
        {
            await _service.AddTestCaseIntoTestScenarioAsync(projectId, scenarioId, testCase);
            return Ok(testCase);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteProjectAsync(id);
            return Ok();
        }
        
        // TODO update test scenario
        
        // TODO update test case
        
        // TODO delete test scenario
        
        // TODO delete test case
    }
}