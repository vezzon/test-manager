using System.Collections.Generic;
using Testro.TestingManagement.WebApi.ViewModels.TestScenario;

namespace Testro.TestingManagement.WebApi.ViewModels.TestProject
{
    public class TestProjectView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Requirements { get; set; }
        public List<TestScenarioView> TestScenarios { get; set; }
    }
}