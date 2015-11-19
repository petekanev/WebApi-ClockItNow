namespace BillableHoursWebApp.Api.Tests.IntegrationTests
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using DataTransferModels.Project;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MyTested.WebApi;

    [TestClass]
    public class ProjectsIntegrationTests
    {
        [TestInitialize]
        public void TestInit()
        {
        }

        [TestMethod]
        public void GetProjectShouldReturnCorrectResponse()
        {
            MyWebApi.Server().Starts<Startup>();

            MyWebApi
                .Server()
                .Working()
                .WithHttpRequestMessage(req => req
                    .WithRequestUri("api/projects/2")
                    .WithMethod(HttpMethod.Get))
                .ShouldReturnHttpResponseMessage()
                .WithStatusCode(HttpStatusCode.OK)
                .WithResponseModelOfType<ProjectResponseModel>();

            MyWebApi.Server().Stops();
        }
    }
}
