using System.Threading.Tasks;
using Testro.TestingManagement.WebApi;
using Testro.TestingManagement.WebApi.Models;
using Xunit;

namespace Testro.TestingManagement.WebApiIntegrationTests.Controllers
{
    public class TestProjectsControllerTests : BaseControllerTests<TestProject>
    {
        public TestProjectsControllerTests(CustomWebApplicationFactory<Startup> factory) : base(factory)
        {
        }

        protected override TestProject UpdateEntity { get; } = Fixtures.Projects.GetProjectForUpdate();

        [Theory]
        [InlineData("/api/v1/TestProjects")]
        public async Task GetAllProjects(string url)
            => await GetAllEntities_WithAuthorization_ReturnOk(url);
    }
}