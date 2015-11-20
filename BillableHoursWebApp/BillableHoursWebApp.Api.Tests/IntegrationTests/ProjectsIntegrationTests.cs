namespace BillableHoursWebApp.Api.Tests.IntegrationTests
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using Common;
    using Controllers;
    using Data;
    using DataTransferModels.Project;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Mocks;
    using MyTested.WebApi;

    [TestClass]
    public class ProjectsIntegrationTests
    {
        [Ignore]
        [TestInitialize]
        public void TestInit()
        {
            MyWebApi.Server().Starts<Startup>();
            MyWebApi.Controller<ProjectsController>()
                .WithResolvedDependencyFor(new BillableHoursWebAppData())
                .WithResolvedDependencyFor(MocksFactory.PubnubBroadcaster)
                .WithResolvedDependencyFor(MocksFactory.DropboxHelper);
        }

        [Ignore]
        [TestMethod]
        public void GetProjectShouldReturnCorrectResponse()
        {
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
