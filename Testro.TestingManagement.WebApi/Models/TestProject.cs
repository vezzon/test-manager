
using System.Collections.Generic;

namespace Testro.TestingManagement.WebApi.Models
{
    public class TestProject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Requirements { get; set; }
        public List<TestScenario> TestScenarios { get; set; }
    }
}