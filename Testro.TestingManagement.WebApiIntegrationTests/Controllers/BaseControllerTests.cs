using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Newtonsoft.Json;
using Testro.TestingManagement.WebApi;
using Testro.TestingManagement.WebApi.Models;
using Xunit;

namespace Testro.TestingManagement.WebApiIntegrationTests.Controllers
{
    public abstract class BaseControllerTests<TEntity> : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;
        protected virtual TEntity UpdateEntity { get; }

        public BaseControllerTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData("/api/v1/TestProjects")]
        public async Task GetAllEntities_WithoutAuthorization_ReturnUnauthorized(string url)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var result = await client.GetAsync(url);

            // Assert
            result.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }
        
        public async Task GetAllEntities_WithAuthorization_ReturnOk(string url)
        {
            // Arrange
            var user = Fixtures.Users.GetUser();
            var client = _factory.CreateClient();
            client.Authorize(user);
        
            // Act
            var result = await client.GetAsync(url);
        
            // Assert
            result.EnsureSuccessStatusCode();
        }

        [Theory]
        [InlineData("/api/v1/TestProjects/1")]
        public async Task GetEntityById_ReturnsEntityWithId(string url)
        {
            // Arrange
            var user = Fixtures.Users.GetUser();
            
            var client = _factory.CreateClient();
            client.Authorize(user);
        
            // Act
            var result = await client.GetAsync(url);
            var entity = await result.Content.ReadAsAsync<TEntity>();
        
            // Assert
            result.EnsureSuccessStatusCode();
            entity.Should().NotBeNull();
        }
        
        [Theory]
        [InlineData("/api/v1/TestProjects/-1")]
        public async Task GetEntityByInvalidId_ReturnsNotFound(string url)
        {
            // Arrange
            var user = Fixtures.Users.GetUser();
            
            var client = _factory.CreateClient();
            client.Authorize(user);
        
            // Act
            var result = await client.GetAsync(url);
        
            // Assert
            result.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Theory]
        [InlineData("/api/v1/TestProjects")]
        public async Task CreateEntity_WithValidEntity_ReturnsSuccess(string url)
        {
            // Arrange
            var user = Fixtures.Users.GetUser();
            var entity = Fixtures.Projects.GetCreateEmptyProject();
            var jsonEntity = JsonConvert.SerializeObject(entity);
            var client = _factory.CreateClient();
            client.Authorize(user);
            var content = new StringContent(jsonEntity, Encoding.UTF8, "application/json");

            // Act
            var response = await client.PostAsync(url, content);

            // Assert
            response.EnsureSuccessStatusCode();
        }
        
        [Theory]
        [InlineData("/api/v1/TestProjects")]
        public async Task CreateEntity_WithInvalidEntity_ReturnsBadRequest(string url)
        {
            // Arrange
            var user = Fixtures.Users.GetUser();
            var entity =  new object();
            var jsonEntity = JsonConvert.SerializeObject(entity);
            var client = _factory.CreateClient();
            client.Authorize(user);
            var content = new StringContent(jsonEntity, Encoding.UTF8, "application/json");

            // Act
            var response = await client.PostAsync(url, content);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }


        [Theory]
        [InlineData("/api/v1/TestProjects/1")]
        public async Task UpdateEntity_ReturnsOk(string url)
        {
            // Arrange
            var user = Fixtures.Users.GetUser();
            var updateEntity = Fixtures.Projects.GetCreateEmptyProject();
            var jsonEntity = JsonConvert.SerializeObject(updateEntity);
            var content = new StringContent(jsonEntity, Encoding.UTF8, "application/json");
            var client = _factory.CreateClient();
            client.Authorize(user);

            // Act
            var response = await client.PatchAsync(url, content);

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Theory]
        [InlineData("/api/v1/TestProjects/4")]
        public async Task DeleteEntity_WithValidId_RemovesEntity(string url)
        {
            // Arrange
            var user = Fixtures.Users.GetUser();
            var client = _factory.CreateClient();
            client.Authorize(user);

            // Act
            var result = await client.DeleteAsync(url);
            result.EnsureSuccessStatusCode();

            var deletedEntity = await client.GetAsync(url);
            // Assert
            deletedEntity.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}