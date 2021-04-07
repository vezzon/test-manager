using System.ComponentModel.DataAnnotations;

namespace Testro.TestingManagement.WebApi.ViewModels.TestProject
{
    public class TestProjectCreate
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Requirements { get; set; }
    }
}