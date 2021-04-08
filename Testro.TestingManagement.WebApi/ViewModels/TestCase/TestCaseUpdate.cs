using System.ComponentModel.DataAnnotations;
using Testro.TestingManagement.WebApi.Attributes;

namespace Testro.TestingManagement.WebApi.ViewModels.TestCase
{
    public class TestCaseUpdate
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [EntityId(typeof(Models.TestScenario))]
        public int? TestScenarioId { get; set; } 
    }
}