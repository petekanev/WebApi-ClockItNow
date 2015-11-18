namespace BillableHoursWebApp.Api.Tests.RouteTests
{
    using System.Net.Http;
    using Controllers;
    using DataTransferModels.Project;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MyTested.WebApi;

    [TestClass]
    public class ProjectsControllerTests
    {
        private const string ValidProjectRequestModelName = "TestProjectName";
        private const string ValidProjectRequestModelDescription =
            "TestDescriptionTestDescriptionTestDescription TestDescription TestDescription TestDescription TestDescription TestDescriptionTestDescriptionTestDescription TestDescription";
        private const string ValidProjectRequestModelJson = @"{ ""Name"": """ + ValidProjectRequestModelName + @""", ""Description"": """ + ValidProjectRequestModelDescription + @""" }";

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
                .WithJsonContent(ValidProjectRequestModelJson)
                .To<ProjectsController>(c => c.Post(new ProjectRequestModel
                {
                    Name = ValidProjectRequestModelName,
                    Description = ValidProjectRequestModelDescription
                }))
                .AndAlso()
                .ToValidModelState();
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
                }))
                .AndAlso()
                .ToValidModelState();
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