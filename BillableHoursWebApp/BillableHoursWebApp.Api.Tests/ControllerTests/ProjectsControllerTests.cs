namespace BillableHoursWebApp.Api.Tests.ControllerTests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using Controllers;
    using DataTransferModels.Project;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Mocks;
    using MyTested.WebApi;

    [TestClass]
    public class ProjectsControllerTests
    {

        private IControllerBuilder<ProjectsController> controller;

        [TestInitialize]
        public void TestInit()
        {
            this.controller = MyWebApi
                .Controller<ProjectsController>()
                .WithResolvedDependencyFor(MocksFactory.BillableHoursWebAppData)
                .WithResolvedDependencyFor(MocksFactory.PubnubBroadcaster);
        }

        [TestMethod]
        public void ReturnTwentyProjectsGetAction()
        {
            this.controller
                .Calling(c => c.Get())
                .ShouldReturn()
                .Ok()
                .WithResponseModelOfType<List<ProjectResponseModel>>()
                .Passing(c => c.Count == 20);
        }

        [TestMethod]
        public void ReturnOneProjectGetActionWithIdParameter()
        {
            this.controller
                .Calling(c => c.Get(2))
                .ShouldReturn()
                .Ok()
                .WithResponseModelOfType<ProjectResponseModel>()
                .Passing(p => p.Name == "Mock Project version2");
        }

        [TestMethod]
        public void ReturnBadRequestWhenProjectWithThatIdIsNotFoundGetActionWithIdParameter()
        {
            this.controller
                .Calling(c => c.Get(100))
                .ShouldReturn()
                .BadRequest()
                .WithErrorMessage();
        }

        [TestMethod]
        public void ReturnProjectsGetActionWithCategoryIdParameter()
        {
            this.controller
                .Calling(c => c.GetByCategory(2))
                .ShouldHave()
                .ActionAttributes(attr => attr.RestrictingForRequestsWithMethod(HttpMethod.Get))
                .AndAlso()
                .ShouldHave()
                .ActionAttributes(attr => attr.ChangingRouteTo("~/api/projects/category/{id}"))
                .AndAlso()
                .ShouldReturn()
                .Ok()
                .WithResponseModelOfType<List<ProjectResponseModel>>()
                .Passing(p =>
                {
                    Assert.IsTrue(p.Count > 1);
                    Assert.IsTrue(p.First().Category.Id == 2);
                });
        }
    }
}
