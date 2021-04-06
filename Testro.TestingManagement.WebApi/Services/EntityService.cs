using System.Collections.Generic;
using System.Threading.Tasks;
using Testro.TestingManagement.WebApi.Exceptions;
using Testro.TestingManagement.WebApi.Models;
using Testro.TestingManagement.WebApi.Repositories;

namespace Testro.TestingManagement.WebApi.Services
{
    public class EntityService<TEntity> where TEntity : class
    {
        private readonly Repository<TEntity> _repository;
        
        public EntityService(Repository<TEntity> repository)
        {
            _repository = repository;
        }
        
        public async Task<List<TEntity>> GetAsync()
        {
            return await _repository.GetAsync();
        }
        
        public async Task<TEntity> GetAsync(int id)
        {
            var entity = await _repository.GetAsync(id);
            if (entity is null)
                throw new NotFoundException();
            
            return entity;
        }
        
        public async Task AddAsync(TEntity entity)
        {
            await _repository.AddAsync(entity);
        }

        public async Task UpdateAsync(int id, TEntity updateEntity)
        {
            var entity = await _repository.GetAsync(id);
            if (entity is null)
                throw new NotFoundException();

            await _repository.UpdateAsync(updateEntity);
        }
        
        public async Task DeleteAsync(int id)
        {
            var entity = await GetAsync(id);
            if (entity is null)
                throw new NotFoundException();
            
            await _repository.DeleteAsync(entity);
        }
    }
}