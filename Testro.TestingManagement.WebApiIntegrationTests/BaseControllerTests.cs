using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Testro.TestingManagement.WebApi;
using Testro.TestingManagement.WebApi.Configurations;
using Xunit;

namespace Testro.TestingManagement.WebApiIntegrationTests
{
    public class BaseControllerTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public BaseControllerTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData("/api/v1/TestProjects")]
        public async Task GetAllProjects_WithoutAuthorization_ReturnForbidden(string url)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var result = await client.GetAsync(url);

            // Assert
            result.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }
        
        [Theory]
        [InlineData("/api/v1/TestProjects")]
        public async Task GetAllProjects_WithAuthorization_ReturnOk(string url)
        {
            // Arrange
            var user = new IdentityUser
            {
                UserName = "user2@example.com",
                NormalizedUserName = "USER2@EXAMPLE.COM",
                Email = "user2@example.com",
                NormalizedEmail = "USER2@EXAMPLE.COM"
            };
            
            var client = _factory.CreateClient();
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("bearer", GenerateJwtToken(user));
        
            // Act
            var result = await client.GetAsync(url);
        
            // Assert
            result.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }
        
        private string GenerateJwtToken(IdentityUser user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
        
            var key = Encoding.ASCII.GetBytes("l4Yt5ctUDiYETPEoRUQjzyVvK0JH03dS");
        
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("id", user.Id),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(6),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
        
            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHandler.WriteToken(token);
        
            return jwtToken;
        }
    }
}