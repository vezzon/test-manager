using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Testro.TestingManagement.WebApi.DataAccess;
using Testro.TestingManagement.WebApi.Models;

namespace Testro.TestingManagement.WebApi.Repositories
{
    public class TestProjectRepository
    {
        private readonly DatabaseContext _db;

        public TestProjectRepository(DatabaseContext db)
        {
            _db = db;
        }

        public async Task<List<TestProject>> GetAsync()
        {
            return await _db.TestProjects
                .Include(tp => tp.TestScenarios)
                .ToListAsync();
        }
        
        public async Task<TestProject> GetAsync(int id)
        {
            var project = _db.TestProjects
                .Include(tp => tp.TestScenarios)
                .FirstOrDefaultAsync(p => p.Id == id);
            return await project;
        }

        public async Task<TestProject> GetAsync(string name)
        {
            var project = _db.TestProjects
                .Include(tp => tp.TestScenarios)
                .FirstOrDefaultAsync(p => p.Name == name);
            return await project;
        }

        public async Task AddAsync(TestProject project)
        {
            await _db.TestProjects.AddAsync(project);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(TestProject project)
        {
            _db.TestProjects.Update(project);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(string name)
        {
            var project = _db.TestProjects.FirstOrDefault(p => p.Name == name);
            if (project is not null)
            {
                _db.TestProjects.Remove(project);
                await _db.SaveChangesAsync();
            }
        }
    }
}