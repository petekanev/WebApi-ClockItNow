namespace BillableHoursWebApp.Api.Tests
{
    using System.Reflection;
    using System.Web.Http;
    using App_Start;
    using AutoMapper;
    using Common;
    using Data.Models;
    using DataTransferModels;
    using DataTransferModels.Project;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MyTested.WebApi;

    [TestClass]
    public class TestInit
    {
        [AssemblyInitialize]
        public static void AssemblyInit(TestContext context)
        {
            AutoMapperConfig.RegisterMappings(Assembly.Load(Constants.DataTransferModelsAssembly));
            Mapper.CreateMap<AttachmentRequestModel, Attachment>();
            Mapper.CreateMap<ProjectRequestModel, Project>();
            Mapper.CreateMap<ProjectWorkLogRequestModel, WorkLog>();

            var config = new HttpConfiguration();
            WebApiConfig.Register(config);
            MyWebApi.IsUsing(config);
        }
    }
}
