using System;
using System.Collections.Generic;
using Moq;
using Testro.TestingManagement.WebApi.Models;
using Testro.TestingManagement.WebApi.Repositories;
using Testro.TestingManagement.WebApi.Services;
using System.Threading.Tasks;
using Xunit;

namespace Testro.TestingManagement.UnitTests
{
    public class EntityServiceTests
    {
        private readonly EntityService<TestProject> _sut;
        private readonly Mock<IRepository<TestProject>> _projectRepoMock = new Mock<IRepository<TestProject>>();

        public EntityServiceTests()
        {
            _sut = new EntityService<TestProject>(_projectRepoMock.Object);
        }
        
        [Fact]
        public async Task GetByIdAsync_ShouldReturnEntity_WhenEntityExists()
        {
            // Arrange
            var projectId = 1;
            var project = new TestProject
            {
                Id = projectId,
                Name = "Test",
                Requirements = "Test Req",
                TestScenarios = new List<TestScenario>()
            };
            
            _projectRepoMock.Setup(x => x.GetAsync(It.IsAny<int>()))
                .ReturnsAsync(project).Verifiable();

            // Act
            var projectFromMockRepo = await _sut.GetAsync(projectId);

            // Assert
            Assert.Equal(projectId, projectFromMockRepo.Id);
        }
    }
}