using AutoMapper;
using Testro.TestingManagement.WebApi.Models;
using Testro.TestingManagement.WebApi.ViewModels.TestCase;
using Testro.TestingManagement.WebApi.ViewModels.TestProject;
using Testro.TestingManagement.WebApi.ViewModels.TestScenario;

namespace Testro.TestingManagement.WebApi
{
    public class AutoMapperConfiguration : Profile
    {
        public AutoMapperConfiguration()
        {
            CreateViewMap<TestProject, TestProjectView, TestProjectCreate, TestProjectUpdate>();
            CreateViewMap<TestScenario, TestScenarioView, TestScenarioCreate, TestScenarioUpdate>();
            CreateViewMap<TestCase, TestCaseView, TestCaseCreate, TestCaseUpdate>();
        }

        private void CreateViewMap<TEntity, TView, TCreate, TUpdate>()
        {
            CreateMap<TEntity, TView>();
            CreateMap<TView, TEntity>();
            CreateMap<TCreate, TEntity>();
            CreateMap<TUpdate, TEntity>();
        }
    }
}