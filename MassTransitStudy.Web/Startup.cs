using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MassTransitStudy.Web.Startup))]
namespace MassTransitStudy.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.MapSignalR();
        }
    }
}
