using System.ComponentModel.DataAnnotations;

namespace Testro.TestingManagement.WebApi.ViewModels.Auth
{
    public class UserLoginRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}