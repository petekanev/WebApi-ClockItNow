namespace SourceControlSystem.Api.Tests.RouteTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MyTested.WebApi;
    using BillableHoursWebApp.Api.Controllers;
    using BillableHoursWebApp.Api.Models;
    using System.Net.Http;
    using BillableHoursWebApp.DataTransferModels.Project;

    [TestClass]
    public class ProjectsControllerTests
    {
        [TestMethod]
        public void GetShouldMapCorrectly()
        {
            MyWebApi
                .Routes()
                .ShouldMap("api/projects")
                .To<ProjectsController>(c => c.Get());
        }

        [TestMethod]
        public void GetWithIdShouldMapCorrectly()
        {
            MyWebApi
                .Routes()
                .ShouldMap("api/projects/5")
                .To<ProjectsController>(c => c.Get(5));
        }

        [TestMethod]
        public void GetWithCategoryIdShouldMapCorrectly()
        {
            MyWebApi
                .Routes()
                .ShouldMap("api/projects/category/5")
                .To<ProjectsController>(c => c.GetByCategory(5));
        }

        [TestMethod]
        public void PostShouldMapCorrectly()
        {
            MyWebApi
                .Routes()
                .ShouldMap("api/projects")
                .WithHttpMethod(HttpMethod.Post)
                .WithJsonContent(@"{ ""Name"": ""Test"", ""Description"": ""TestDescription"" }")
                .To<ProjectsController>(c => c.Post(new ProjectRequestModel
                {
                    Name = "Test",
                    Description = "TestDescription"
                }));
        }

        [TestMethod]
        public void PutWithCompletedProjectIdShouldMapCorrectly()
        {
            MyWebApi
                .Routes()
                .ShouldMap("api/projects/complete/5")
                .WithHttpMethod(HttpMethod.Put)
                .To<ProjectsController>(c => c.FinalizeProject(5));
        }

        [TestMethod]
        public void GetWithCompletedProjectIdShouldMapCorrectly()
        {
            MyWebApi
                .Routes()
                .ShouldMap("api/projects/complete/5")
                .To<ProjectsController>(c => c.GetInvoiceFromFinalizedProject(5));
        }

        [TestMethod]
        public void PostWithSessionIdShouldMapCorrectly()
        {
            MyWebApi
                .Routes()
                .ShouldMap("api/projects/session/5")
                .WithHttpMethod(HttpMethod.Post)
                .WithJsonContent(@"{  ""ShortDescription"": ""TestDescription"" }")
                .To<ProjectsController>(c => c.BeginWorkLogSession(5, new ProjectWorkLogRequestModel
                {
                    ShortDescription = "TestDescription"
                }));
        }

        [TestMethod]
        public void PutWithSessionIdShouldMapCorrectly()
        {
            MyWebApi
                .Routes()
                .ShouldMap("api/projects/session/5")
                .WithHttpMethod(HttpMethod.Put)
                .To<ProjectsController>(c => c.EndWorkLogSession(5));
        }
    }
}