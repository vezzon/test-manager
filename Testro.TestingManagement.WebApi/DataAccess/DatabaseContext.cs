using Microsoft.EntityFrameworkCore;
using Testro.TestingManagement.WebApi.Models;

namespace Testro.TestingManagement.WebApi.DataAccess
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }
        
        public DbSet<TestProject> TestProjects { get; set; }
        public DbSet<TestScenario> TestScenarios { get; set; }
        public DbSet<TestCase> TestCases { get; set; }
    }
}