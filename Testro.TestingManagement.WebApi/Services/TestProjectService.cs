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
        
        public async Task<TestProject> GetAsync(string name)
        {
            return await _projectRepository.GetAsync(name);
        }
        
        public async Task Add(TestProject project)
        {
            await _projectRepository.AddAsync(project);
        }
        
        public async Task AddTestScenario(string projectName, TestScenario scenario)
        {
            var project = await _projectRepository.GetAsync(projectName);
            if (project is not null)
            {
                if (!ScenarioNameInProject(project, scenario.Name))
                {
                    project.TestScenarios.Add(scenario);
                }
            }
        }

        public async Task AddTestCaseIntoTestScenario(string projectName, string scenarioName, TestCase testCase)
        {
            var project = await _projectRepository.GetAsync(projectName);
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
        
        public async Task DeleteProject(string projectName)
        {
            await _projectRepository.DeleteAsync(projectName);
        }

        private bool ScenarioNameInProject(TestProject project, string name)
        {
            return project.TestScenarios.Any(x => x.Name == name);
        }

    }
}