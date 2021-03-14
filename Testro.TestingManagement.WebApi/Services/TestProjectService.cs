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
        
        // Get all projects
        
        // Get Project by Id
        
        // Get Project by Name
        
        // Create Project
        
        // Update Project
            // Add test scenario
            // Add test case into test scenario
        
        // Delete project

    }
}