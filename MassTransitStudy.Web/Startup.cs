
using Microsoft.Owin;

[assembly: OwinStartup(typeof(MassTransitStudy.Web.Startup))]

namespace MassTransitStudy.Web
{
    using Owin;

    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.MapSignalR();
        }
    }
}
