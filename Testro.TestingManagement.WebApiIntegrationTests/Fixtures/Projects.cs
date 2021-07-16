using System.Collections.Generic;
using Testro.TestingManagement.WebApi.Models;

namespace Testro.TestingManagement.WebApiIntegrationTests.Fixtures
{
    public static class Projects
    {
        public static TestProject GetProject()
        {
            return new TestProject
            {
                Id = 1,
                Name = "Integration test project fixture",
                Requirements = "Windows 11",
                TestScenarios = new List<TestScenario>
                {
                    GetScenario()
                }
            };
        }

        public static TestScenario GetScenario()
        {
            return new TestScenario
            {
                Id = 1,
                Name = "Integration test scenario fixture",
                TestCases = new List<TestCase>
                {
                    GetCase()
                },
                TestProjectId = 1
            };
        }

        public static TestCase GetCase()
        {
            return new TestCase
            {
                Id = 1,
                Title = "Integration test case fixture",
                Description = "Test description",
                TestScenarioId = 1,
            };
        }

        public static TestProject GetCreateProject()
        {
            return new TestProject
            {
                Id = 2,
                Name = "Create project",
                Requirements = "Windows 11",
                TestScenarios = new List<TestScenario>
                {
                    new TestScenario
                    {
                        Id = 2,
                        Name = "Create scenario",
                        TestCases = new List<TestCase>
                        {
                            new TestCase
                            {
                                Id = 2,
                                Title = "Create case",
                                Description = "Test description",
                                TestScenarioId = 2,
                            }
                        },
                        TestProjectId = 2
                    }
                }
            };
        }
        
        public static TestProject GetCreateEmptyProject()
        {
            return new TestProject
            {
                Id = 2,
                Name = "Create empty project",
                Requirements = "Windows 11",
                TestScenarios = null
            };
        }
        
        public static TestProject GetProjectForUpdate()
        {
            return new TestProject
            {
                Id = 3,
                Name = "Project for update",
                Requirements = "Please update",
                TestScenarios = null
            };
        }

        public static TestProject GetProjectForDelete()
        {
            return new TestProject
            {
                Id = 4,
                Name = "Project for delete",
                Requirements = "Please delete",
                TestScenarios = null
            };
        }
    }
}