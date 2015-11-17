namespace BillableHoursWebApp.Api.Tests.RouteTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MyTested.WebApi;
    using BillableHoursWebApp.Api.Controllers;
    using BillableHoursWebApp.Api.Models;
    using System.Net.Http;
    using BillableHoursWebApp.DataTransferModels.Project;
    using DataTransferModels;

    [TestClass]
    public class CategoriesControllerTests
    {
        [TestMethod]
        public void GetShouldMapCorrectly()
        {
            MyWebApi
                .Routes()
                .ShouldMap("api/categories")
                .To<CategoriesController>(c => c.Get());
        }

        [TestMethod]
        public void GetWithIdShouldMapCorrectly()
        {
            MyWebApi
                .Routes()
                .ShouldMap("api/categories/5")
                .To<CategoriesController>(c => c.Get(5));
        }

        [TestMethod]
        public void PostShouldMapCorrectly()
        {
            MyWebApi
                .Routes()
                .ShouldMap("api/categories")
                .WithHttpMethod(HttpMethod.Post)
                .WithJsonContent(@"{ ""Name"": ""Test"" }")
                .To<CategoriesController>(c => c.Post(new CategoryRequestModel
                {
                    Name = "Test"
                }));
        }

        [TestMethod]
        public void PutWithCompletedProjectIdShouldMapCorrectly()
        {
            MyWebApi
                .Routes()
                .ShouldMap("api/categories/5")
                .WithHttpMethod(HttpMethod.Put)
                 .WithJsonContent(@"{ ""Name"": ""Test"" }")
                .To<CategoriesController>(c => c.Put(5, new CategoryRequestModel
                {
                    Name = "Test"
                }));
        }
    }
}
