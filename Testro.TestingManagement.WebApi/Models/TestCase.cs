namespace Testro.TestingManagement.WebApi.Models
{
    public class TestCase
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public int TestScenarioId { get; set; }
        public virtual TestScenario TestScenario { get; set; }
    }
}