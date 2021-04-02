using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Testro.TestingManagement.WebApi.DataAccess;
using Testro.TestingManagement.WebApi.Exceptions;
using Testro.TestingManagement.WebApi.Models;

namespace Testro.TestingManagement.WebApi.Repositories
{
    public class Repository<TEntity> where TEntity : class
    {
        private readonly DatabaseContext _db;

        public Repository(DatabaseContext db)
        {
            _db = db;
        }

        public async Task<List<TEntity>> GetAsync()
        {
            return await _db.Set<TEntity>().ToListAsync();
        }
        
        public async Task<TEntity> GetAsync(int id)
        {
            return await _db.Set<TEntity>().FindAsync(id);
        }

        public async Task AddAsync(TEntity entity)
        {
            await _db.Set<TEntity>().AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            _db.Set<TEntity>().Update(entity);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(TEntity entity)
        {
            _db.Set<TEntity>().Remove(entity);
            await _db.SaveChangesAsync();
        }
    }
}