namespace BillableHoursWebApp.Api
{
    using System.Web;
    using System.Web.Http;
    using System.Web.Routing;
    using System.Reflection;

    using App_Start;
    using AutoMapper;
    using Common;
    using Data.Models;
    using DataTransferModels;
    using DataTransferModels.Project;

    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AutoMapperConfig.RegisterMappings(Assembly.Load(Constants.DataTransferModelsAssembly));
            Mapper.CreateMap<AttachmentRequestModel, Attachment>();
            Mapper.CreateMap<ProjectRequestModel, Project>();
            Mapper.CreateMap<ProjectWorkLogRequestModel, WorkLog>();

            DatabaseConfig.Initialize();


            GlobalConfiguration.Configure(WebApiConfig.Register);

            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
