using System.ComponentModel.DataAnnotations;
using Testro.TestingManagement.WebApi.Attributes;

namespace Testro.TestingManagement.WebApi.ViewModels.TestScenario
{
    public class TestScenarioCreate
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [EntityId(typeof(Models.TestScenario))]
        public int? TestProjectId { get; set; }
    }
}