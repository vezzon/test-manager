using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Testro.TestingManagement.WebApi.Models;
using Testro.TestingManagement.WebApi.Repositories;

namespace Testro.TestingManagement.WebApi.Services
{
    public class TestProjectService
    {
        private readonly TestProjectRepository _projectRepository;

        public TestProjectService(TestProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }
        
        public async Task<List<TestProject>> GetAsync()
        {
            return await _projectRepository.GetAsync();
        }
        
        public async Task<TestProject> GetAsync(int id)
        {
            return await _projectRepository.GetAsync(id);
        }
        
        public async Task AddAsync(TestProject project)
        {
            await _projectRepository.AddAsync(project);
        }
        
        public async Task AddTestScenarioAsync(int projectId, TestScenario scenario)
        {
            var project = await _projectRepository.GetAsync(projectId);
            project?.TestScenarios.Add(scenario);
        }

        public async Task AddTestCaseIntoTestScenarioAsync(int id, int scenarioId, TestCase testCase)
        {
            var project = await _projectRepository.GetAsync(id);
            if (project is not null)
            {
                var scenario = project.TestScenarios.FirstOrDefault(
                    x => x.Id == scenarioId);

                scenario?.TestCases.Add(testCase);
            } 
        }
        
        public async Task DeleteProjectAsync(int id)
        {
            await _projectRepository.DeleteAsync(id);
        }
    }
}