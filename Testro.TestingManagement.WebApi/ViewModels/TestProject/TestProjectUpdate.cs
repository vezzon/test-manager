using System.ComponentModel.DataAnnotations;

namespace Testro.TestingManagement.WebApi.ViewModels.TestProject
{
    public class TestProjectUpdate
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Requirements { get; set; }
    }
}