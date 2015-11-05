using Microsoft.Owin;

[assembly: OwinStartup(typeof(BillableHoursWebApp.Api.Startup))]

namespace BillableHoursWebApp.Api
{
    using Owin;

    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
