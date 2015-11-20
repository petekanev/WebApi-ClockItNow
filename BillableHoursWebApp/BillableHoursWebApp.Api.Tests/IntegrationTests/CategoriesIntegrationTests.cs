namespace BillableHoursWebApp.Api.Tests.IntegrationTests
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using DataTransferModels;
    using DataTransferModels.Project;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MyTested.WebApi;

    [TestClass]
    public class CategoriesIntegrationTests
    {
        [TestInitialize]
        public void TestInit()
        {
            MyWebApi.Server().Starts<Startup>();
        }

        [TestMethod]
        public void GetCategoryShouldReturnCorrectResponse()
        {
            MyWebApi
                .Server()
                .Working()
                .WithHttpRequestMessage(req => req
                    .WithRequestUri("api/categories/2")
                    .WithMethod(HttpMethod.Get))
                .ShouldReturnHttpResponseMessage()
                .WithStatusCode(HttpStatusCode.OK)
                .WithResponseModelOfType<CategoryResponseModel>();

            MyWebApi.Server().Stops();
        }

        [TestMethod]
        public void GetAllCategoriesShouldReturnCorrectResponse()
        {
            MyWebApi
                .Server()
                .Working()
                .WithHttpRequestMessage(req => req
                    .WithRequestUri("api/categories")
                    .WithMethod(HttpMethod.Get))
                .ShouldReturnHttpResponseMessage()
                .WithStatusCode(HttpStatusCode.OK)
                .WithResponseModelOfType<List<CategoryResponseModel>>();

            MyWebApi.Server().Stops();
        }

        [TestMethod]
        public void GetCategoriesShouldReturnCorrectResponse()
        {
            MyWebApi
                .Server()
                .Working()
                .WithHttpRequestMessage(req => req
                    .WithRequestUri("api/categories/2")
                    .WithMethod(HttpMethod.Get))
                .ShouldReturnHttpResponseMessage()
                .WithStatusCode(HttpStatusCode.OK)
                .WithResponseModelOfType<CategoryResponseModel>();

            MyWebApi.Server().Stops();
        }
    }
}
