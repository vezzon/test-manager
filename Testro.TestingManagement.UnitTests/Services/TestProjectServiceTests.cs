using System.Collections.Generic;
using System.Threading.Tasks;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Testro.TestingManagement.WebApi.Models;
using Testro.TestingManagement.WebApi.Repositories;
using Testro.TestingManagement.WebApi.Services;
using Xunit;

namespace Testro.TestingManagement.UnitTests.Services
{
    public class TestProjectServiceTests
    {
        private readonly IEntityService<TestProject> _sut;
        private readonly IRepository<TestProject> _repository = Substitute.For<IRepository<TestProject>>();

        public TestProjectServiceTests()
        {
            _sut = new EntityService<TestProject>(_repository);
        }

        [Fact]
        public async Task GetAsync_ShouldReturnProjectById_WhenProjectExists()
        {
            // Arrange
            const int projectId = 1;
            const string projectName = "Name";
            const string projectRequirements = "Requirements";
            var projectScenarios = new List<TestScenario>();
            var projectMock = new TestProject
            {
                Id = projectId,
                Name = projectName,
                Requirements = projectRequirements,
                TestScenarios = projectScenarios
            };
            
            _repository.GetAsync(projectId).Returns(projectMock);

            // Act
            var project = await _sut.GetAsync(projectId);

            // Assert
            Assert.Equal(projectId, project.Id);
            Assert.Equal(projectName, project.Name);
            Assert.Equal(projectRequirements, project.Requirements);
            Assert.Equal(projectScenarios, project.TestScenarios);
        }

        [Fact]
        public async Task GetAsync_ShouldReturnNull_WhenProjectDoesNotExist()
        {
            // Arrange
            _repository.GetAsync(Arg.Any<int>()).ReturnsNull();
            
            // Act
            var project = await _sut.GetAsync(13);

            // Assert
            Assert.Null(project);
        }
        
    }
}