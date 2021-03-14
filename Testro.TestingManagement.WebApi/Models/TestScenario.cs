using System.Collections.Generic;

namespace Testro.TestingManagement.WebApi.Models
{
    public class TestScenario
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<TestCase> TestCases { get; set; }

        public TestScenario(string title)
        {
            Title = title;
        }
    }
}