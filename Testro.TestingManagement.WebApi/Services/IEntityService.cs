using System.Collections.Generic;
using System.Threading.Tasks;

namespace Testro.TestingManagement.WebApi.Services
{
    public interface IEntityService<TEntity> where TEntity : class
    {
        Task<List<TEntity>> GetAsync();
        Task<TEntity> GetAsync(int id);
        Task AddAsync(TEntity entity);
        Task UpdateAsync(TEntity updateEntity);
        Task<bool> DeleteAsync(int id);
    }
}