namespace BillableHoursWebApp.Api.Tests.ControllerTests
{
    using System;
    using System.Collections.Generic;
    using Controllers;
    using DataTransferModels;
    using DataTransferModels.Project;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Mocks;
    using MyTested.WebApi;
    using TestObjects;

    [TestClass]
    public class CategoriesControllerTests
    {
        private IControllerBuilder<CategoriesController> controller;

        [TestInitialize]
        public void TestInit()
        {
            this.controller = MyWebApi
                .Controller<CategoriesController>()
                .WithResolvedDependencyFor(MocksFactory.BillableHoursWebAppData);
        }

        [TestMethod]
        public void ReturnAnyCategoriesGetAction()
        {
            this.controller
                .Calling(c => c.Get())
                .ShouldReturn()
                .Ok()
                .WithResponseModelOfType<List<CategoryResponseModel>>()
                .Passing(c => c.Count > 1);
        }

        [TestMethod]
        public void ReturnOneCategoryGetActionWithIdParameter()
        {
            this.controller
                .Calling(c => c.Get(1))
                .ShouldReturn()
                .Ok()
                .WithResponseModelOfType<CategoryResponseModel>()
                .Passing(p => p.Id == 1);
        }

        [TestMethod]
        public void ReturnBadRequestWhenCategoryWithThatIdIsNotFoundGetActionWithIdParameter()
        {
            this.controller
                .Calling(c => c.Get(100))
                .ShouldReturn()
                .BadRequest()
                .WithErrorMessage("No category with that id is present.");
        }

        [TestMethod]
        public void ReturnOkPostActionWithCorrectModelAndAuthorizedUser()
        {
            this.controller
                .Calling(c => c.Post(TestObjectsFactory.ValidCategoryRequestModel))
                .ShouldHave()
                .ActionAttributes(attr => attr.RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldHave()
                .ValidModelState()
                .AndAlso()
                .ShouldReturn()
                .Ok()
                .WithResponseModelOfType<int>()
                .Passing(x => x > 1);
        }

        [TestMethod]
        public void ReturnBadRequestPostActionWithInvalidModelState()
        {
            this.controller
                .Calling(c => c.Post(new CategoryRequestModel()))
                .ShouldHave()
                .ActionAttributes(attr => attr.RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .BadRequest()
                .WithModelStateFor<CategoryRequestModel>()
                .ContainingModelStateErrorFor(p => p.Name);
        }

        [TestMethod]
        public void ReturnBadRequestPostActionWithValidModelStateButDuplicateName()
        {
            this.controller
                .Calling(c => c.Post(new CategoryRequestModel { Name = "Mock Category" }))
                .ShouldHave()
                .ActionAttributes(attr => attr.RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .BadRequest()
                .WithErrorMessage("A category with that name exists already!");
        }

        [TestMethod]
        public void ReturnOkDeleteActionWithAuthorizedUser()
        {
            this.controller
                .Calling(c => c.Delete(1))
                .ShouldHave()
                .ActionAttributes(attr => attr.RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .Ok()
                .WithResponseModelOfType<CategoryResponseModel>()
                .Passing(c => c.Id == 1);
        }

        [TestMethod]
        public void ReturnBadRequestInvalidCategoryIdDeleteAction()
        {
            this.controller
                .Calling(c => c.Delete(100))
                .ShouldHave()
                .ActionAttributes(attr => attr.RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .BadRequest()
                .WithErrorMessage("No category with that id is present.");
        }
    }
}
