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
    using TestObjects;

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
                .WithResolvedDependencyFor(MocksFactory.PubnubBroadcaster)
                .WithResolvedDependencyFor(MocksFactory.DropboxHelper);
        }

        [TestMethod]
        public void ReturnTwentyOrMoreProjectsGetAction()
        {
            this.controller
                .Calling(c => c.Get())
                .ShouldReturn()
                .Ok()
                .WithResponseModelOfType<List<ProjectResponseModel>>()
                .Passing(c => c.Count >= 20);
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

        [TestMethod]
        public void ReturnOkProjectIdPostActionWithCorrectModelAndAuthorizedUser()
        {
            this.controller
                .Calling(c => c.Post(TestObjectsFactory.ValidProjectRequestModel))
                .ShouldHave()
                .ValidModelState()
                .AndAlso()
                .ShouldHave()
                .ActionAttributes(attr => attr.RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .Ok()
                .WithResponseModelOfType<int>();
        }

        [TestMethod]
        public void ReturnBadRequestPostActionWithInvalidModelAndAuthorizedUser()
        {
            this.controller
                .Calling(c => c.Post(TestObjectsFactory.InvalidProjectRequestModel))
                .ShouldReturn()
                .BadRequest()
                .WithModelStateFor<ProjectRequestModel>()
                .ContainingModelStateErrorFor(p => p.Name)
                .AndAlso()
                .ContainingModelStateErrorFor(p => p.Description)
                .AndAlso()
                .ContainingModelStateErrorFor(p => p.PricePerHour);
        }

        [TestMethod]
        public void ReturnOkFinalizeProjectPutAction()
        {
            this.controller
                .Calling(c => c.FinalizeProject(1))
                .ShouldHave()
                .ActionAttributes(attr => attr.RestrictingForRequestsWithMethod(HttpMethod.Put))
                .AndAlso()
                .ShouldHave()
                .ActionAttributes(attr => attr.ChangingRouteTo("~/api/projects/complete/{id}"))
                .AndAlso()
                .ShouldHave()
                .ActionAttributes(attr => attr.RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .Ok();
        }

        [TestMethod]
        public void ReturnBadRequestWhenProjectIsNotCompleteGetInvoiceFromFinalizedProjectGetAction()
        {
            this.controller
                .Calling(c => c.GetInvoiceFromFinalizedProject(15))
                .ShouldHave()
                .ActionAttributes(attr => attr.ChangingRouteTo("~/api/projects/complete/{id}"))
                .AndAlso()
                .ShouldHave()
                .ActionAttributes(attr => attr.RestrictingForRequestsWithMethod(HttpMethod.Get))
                .AndAlso()
                .ShouldHave()
                .ActionAttributes(attr => attr.RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .BadRequest()
                .WithErrorMessage("Project is not finished yet.");
        }

        [TestMethod]
        public void ReturnWorkLogIdPostActionBeginWorkLogSessionWithCorrectModelAndAuthorizedUser()
        {
            this.controller
                .Calling(c => c.BeginWorkLogSession(1, TestObjectsFactory.ValidProjectWorkLogRequestModel))
                .ShouldHave()
                .ActionAttributes(attr => attr.ChangingRouteTo("~/api/projects/session/{id}"))
                .AndAlso()
                .ShouldHave()
                .ActionAttributes(attr => attr.RestrictingForRequestsWithMethod(HttpMethod.Post))
                .AndAlso()
                .ShouldHave()
                .ActionAttributes(attr => attr.RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .Ok()
                .WithResponseModelOfType<int>();
        }

        [TestMethod]
        public void ReturnOkPutActionEndWorkLogSessionAndAuthorizedUser()
        {
            this.controller
                .Calling(c => c.EndWorkLogSession(1))
                .ShouldHave()
                .ActionAttributes(attr => attr.ChangingRouteTo("~/api/projects/session/{id}"))
                .AndAlso()
                .ShouldHave()
                .ActionAttributes(attr => attr.RestrictingForRequestsWithMethod(HttpMethod.Put))
                .AndAlso()
                .ShouldHave()
                .ActionAttributes(attr => attr.RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .Ok();
        }

        [TestMethod]
        public void ReturnBadRequestPutActionEndWorkLogSessionAndAuthorizedUser()
        {
            this.controller
                .Calling(c => c.EndWorkLogSession(25))
                .ShouldReturn()
                .BadRequest()
                .WithErrorMessage("You cannot edit a recorded session!");
        }

        [TestMethod]
        public void ReturnProjectIdPutActionOnAProjectNotBeingWorkedOn()
        {
            controller
                .Calling(c => c.Put(25))
                .ShouldHave()
                .ActionAttributes(attr => attr.RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .Ok()
                .WithResponseModel(25);
        }

        [TestMethod]
        public void ReturnBadRequestPutActionOnAProjectThatIsBeingWorkedOn()
        {
            controller.
                Calling(c => c.Put(10))
                .ShouldHave()
                .ActionAttributes(attr => attr.RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .BadRequest()
                .WithErrorMessage("The project is already being worked on!");
        }

        [TestMethod]
        public void ReturnBadRequestInvalidProjectIdDeleteActionAndAuthorizedUser()
        {
            controller.
                Calling(c => c.Delete(100))
                .ShouldHave()
                .ActionAttributes(attr => attr.RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .BadRequest()
                .WithErrorMessage("No project with that id is present.");
        }

        [TestMethod]
        public void ReturnOkDeleteActionAndAuthorizedUser()
        {
            controller
                .Calling(c => c.Delete(1))
                .ShouldHave()
                .ActionAttributes(attr => attr.RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .Ok()
                .WithResponseModelOfType<ProjectResponseModel>()
                .Passing(p => p.Id = 1);
        }
    }
}
