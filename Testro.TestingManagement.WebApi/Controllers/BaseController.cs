using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Testro.TestingManagement.WebApi.Exceptions;
using Testro.TestingManagement.WebApi.Services;


namespace Testro.TestingManagement.WebApi.Controllers
{ 
    [ApiController]
    [Route("/api/v1/[controller]")]
    public abstract class BaseController<TEntity, TView, TCreate, TUpdate> : ControllerBase
        where TEntity : class
    {
        private readonly EntityService<TEntity> _service;
        private readonly IMapper _mapper;

        public BaseController(EntityService<TEntity> service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        protected async Task<List<TView>> GetAsync()
        {
            var entities = await _service.GetAsync();
            return _mapper.Map<List<TView>>(entities);
        }
        
        protected async Task<TView> GetAsync(int id)
        {
            var entity = await _service.GetAsync(id);
            if (entity is null)
                throw new NotFoundException();
            
            return _mapper.Map<TView>(entity);
        }
        
        protected async Task<TView> CreateAsync(TCreate entityCreate)
        {
            var entity = _mapper.Map<TEntity>(entityCreate);
            await _service.AddAsync(entity);
            return _mapper.Map<TView>(entity);
        }

        protected async Task<TView> UpdateAsync(int id, TUpdate entityUpdate)
        {
            var entity = await _service.GetAsync(id);
            if (entity is null)
                throw new NotFoundException();
            
            _mapper.Map(entityUpdate, entity);
            await _service.UpdateAsync(entity);
            return _mapper.Map<TView>(entity);
        }

        protected async Task DeleteAsync(int id)
        {
            var isDeleted = await _service.DeleteAsync(id);
            if (!isDeleted)
                throw new NotFoundException();
        }
    }
}