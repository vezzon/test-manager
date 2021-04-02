using System.Collections.Generic;
using Testro.TestingManagement.WebApi.Models;
using Testro.TestingManagement.WebApi.ViewModels.TestCase;

namespace Testro.TestingManagement.WebApi.ViewModels.TestScenario
{
    public class TestScenarioView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<TestCaseView> TestCases { get; set; }
    }
}