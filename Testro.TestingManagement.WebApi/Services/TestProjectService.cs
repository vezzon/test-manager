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
            // TODO check if project exists 
            await _projectRepository.AddAsync(project);
        }
        
        public async Task AddTestScenario(int id, TestScenario scenario)
        {
            var project = await _projectRepository.GetAsync(id);
            if (project is not null)
            {
                if (!ScenarioNameInProject(project, scenario.Name))
                {
                    project.TestScenarios.Add(scenario);
                }
            }
        }

        public async Task AddTestCaseIntoTestScenario(int id, string scenarioName, TestCase testCase)
        {
            var project = await _projectRepository.GetAsync(id);
            if (project is not null)
            {
                if (ScenarioNameInProject(project, scenarioName))
                {
                    var scenario = project.TestScenarios.FirstOrDefault(
                        x => x.Name == scenarioName);
                    
                    scenario.TestCases.Add(testCase);
                }
            } 
        }
        
        public async Task DeleteProject(int id)
        {
            await _projectRepository.DeleteAsync(id);
        }

        private bool ScenarioNameInProject(TestProject project, string name)
        {
            return project.TestScenarios.Any(x => x.Name == name);
        }

    }
}