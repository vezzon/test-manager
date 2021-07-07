using Microsoft.AspNetCore.Identity;

namespace Testro.TestingManagement.WebApiIntegrationTests.Fixtures
{
    public static class Users
    {
        public static IdentityUser GetUser()
        {
            return new IdentityUser
            {
                UserName = "user2@example.com",
                Email = "user2@example.com",
            };
        }
    }
}