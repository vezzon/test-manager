using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Testro.TestingManagement.WebApi.Models;
using Testro.TestingManagement.WebApi.Services;
using Testro.TestingManagement.WebApi.ViewModels.TestCase;
using Testro.TestingManagement.WebApi.ViewModels.TestProject;
using Testro.TestingManagement.WebApi.ViewModels.TestScenario;

namespace Testro.TestingManagement.WebApi.Controllers
{
    public class TestProjectsController : BaseController<TestProject, TestProjectView, TestProjectCreate, TestProjectUpdate>
    {
        public TestProjectsController(EntityService<TestProject> service, IMapper mapper) : base(service, mapper)
        {
        }

        [HttpGet]
        public Task<List<TestProjectView>> Get()
            => GetAsync();

        [HttpGet("{id}")]
        public Task<TestProjectView> Get(int id)
            => GetAsync(id);

        [HttpPost]
        public Task<TestProjectView> Create(TestProjectCreate projectCreate)
            => CreateAsync(projectCreate);

        [HttpPatch("{id}")]
        public Task<TestProjectView> Update(int id, TestProjectUpdate projectUpdate)
            => UpdateAsync(id, projectUpdate);

        [HttpDelete("{id}")]
        public Task Delete(int id)
            => DeleteAsync(id);
    }
}