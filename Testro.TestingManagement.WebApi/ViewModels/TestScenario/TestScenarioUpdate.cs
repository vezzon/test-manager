using System.ComponentModel.DataAnnotations;

namespace Testro.TestingManagement.WebApi.ViewModels.TestScenario
{
    public class TestScenarioUpdate
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int? TestProjectId { get; set; }
    }
}