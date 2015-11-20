using Microsoft.Owin;

[assembly: OwinStartup(typeof(BillableHoursWebApp.Api.Startup))]

namespace BillableHoursWebApp.Api
{
    using System.Reflection;
    using System.Web.Http;
    using App_Start;
    using AutoMapper;
    using Common;
    using Data.Models;
    using DataTransferModels;
    using DataTransferModels.Project;
    using Microsoft.Owin.Cors;
    using Owin;

    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            AutoMapperConfig.RegisterMappings(Assembly.Load(Constants.DataTransferModelsAssembly));
            Mapper.CreateMap<AttachmentRequestModel, Attachment>();
            Mapper.CreateMap<ProjectRequestModel, Project>();
            Mapper.CreateMap<CategoryRequestModel, Category>();
            Mapper.CreateMap<ProjectWorkLogRequestModel, WorkLog>();

            DatabaseConfig.Initialize();

            app.UseCors(CorsOptions.AllowAll);

            ConfigureAuth(app);

            var config = new HttpConfiguration();
            WebApiConfig.Register(config);
            app.UseWebApi(config);
        }
    }
}
