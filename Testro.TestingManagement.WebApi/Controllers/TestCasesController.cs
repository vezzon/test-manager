using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Testro.TestingManagement.WebApi.Models;
using Testro.TestingManagement.WebApi.Services;
using Testro.TestingManagement.WebApi.ViewModels.TestCase;

namespace Testro.TestingManagement.WebApi.Controllers
{
    public class TestCasesController : BaseController<TestCase, TestCaseView, TestCaseCreate, TestCaseUpdate>
    {
        public TestCasesController(EntityService<TestCase> service, IMapper mapper) : base(service, mapper)
        {
        }

        [HttpGet]
        public Task<List<TestCaseView>> Get()
            => GetAsync();

        [HttpGet("{id}")]
        public Task<TestCaseView> GetById(int id)
            => GetAsync(id);

        [HttpPost]
        public Task<TestCaseView> Create(TestCaseCreate caseCreate)
            => CreateAsync(caseCreate);
        
        [HttpPatch("{id}")]
        public Task<TestCaseView> Update(int id, TestCaseUpdate caseUpdate)
            => UpdateAsync(id, caseUpdate);

        [HttpDelete("{id}")]
        public Task Delete(int id)
            => DeleteAsync(id);
    }
}