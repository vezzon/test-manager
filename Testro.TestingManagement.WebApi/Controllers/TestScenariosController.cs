using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Testro.TestingManagement.WebApi.Models;
using Testro.TestingManagement.WebApi.Services;
using Testro.TestingManagement.WebApi.ViewModels.TestScenario;

namespace Testro.TestingManagement.WebApi.Controllers
{
    public class TestScenariosController : 
        BaseController<TestScenario, TestScenarioView, TestScenarioCreate, TestScenarioUpdate>
    {
        public TestScenariosController(EntityService<TestScenario> service, IMapper mapper) : base(service, mapper)
        {
        }

        [HttpGet]
        public Task<List<TestScenarioView>> Get()
            => GetAsync();

        [HttpGet("{id}")]
        public Task<TestScenarioView> Get(int id)
            => GetAsync(id);

        [HttpPost]
        public Task<TestScenarioView> Create(TestScenarioCreate scenarioCreate)
            => CreateAsync(scenarioCreate);
        
        [HttpPatch("{id}")]
        public Task<TestScenarioView> Update(int id, TestScenarioUpdate scenarioUpdate)
            => UpdateAsync(id, scenarioUpdate);

        [HttpDelete("{id}")]
        public Task Delete(int id)
            => DeleteAsync(id);
    }

}