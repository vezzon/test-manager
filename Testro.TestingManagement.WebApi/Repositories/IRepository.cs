using System.Collections.Generic;
using System.Threading.Tasks;

namespace Testro.TestingManagement.WebApi.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<List<TEntity>> GetAsync();
        Task<TEntity> GetAsync(int id);
        Task AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
    }
}