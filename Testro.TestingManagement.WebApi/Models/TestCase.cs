namespace Testro.TestingManagement.WebApi.Models
{
    public class TestCase
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool Result { get; set; }
    }
}