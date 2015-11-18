using Microsoft.Owin;

[assembly: OwinStartup(typeof(BillableHoursWebApp.Api.Startup))]

namespace BillableHoursWebApp.Api
{
    using Microsoft.Owin.Cors;
    using Owin;

    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCors(CorsOptions.AllowAll);
            ConfigureAuth(app);
        }
    }
}
