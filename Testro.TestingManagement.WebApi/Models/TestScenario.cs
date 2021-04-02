using System.Collections.Generic;

namespace Testro.TestingManagement.WebApi.Models
{
    public class TestScenario
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<TestCase> TestCases { get; set; }


        public int TestProjectId { get; set; }
        public virtual TestProject TestProject { get; set; }
    }
}