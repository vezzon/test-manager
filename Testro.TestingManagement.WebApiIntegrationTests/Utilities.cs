using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Testro.TestingManagement.WebApi.DataAccess;

namespace Testro.TestingManagement.WebApiIntegrationTests
{
    public static class Utilities
    {
        public static void InitializeDbForTests(DatabaseContext db)
        {
            db.TestProjects.Add(Fixtures.Projects.GetProject());
            db.SaveChanges();
        }

        public static void Authorize(this HttpClient client, IdentityUser user)
        {
            client.DefaultRequestHeaders.Authorization = 
                new AuthenticationHeaderValue("Bearer", GenerateJwtToken(user));
        }
        
        private static string GenerateJwtToken(IdentityUser user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
        
            var key = Encoding.ASCII.GetBytes("l4Yt5ctUDiYETPEoRUQjzyVvK0JH03dS"); // TODO hide secret
        
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