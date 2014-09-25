using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using MassTransit;
using MassTransitStudy.Messages;
using MassTransitStudy.Web.Hubs;
using Microsoft.AspNet.SignalR;

namespace MassTransitStudy.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Bus.Initialize(bus =>
                {
                    bus.ReceiveFrom(Messages.Properties.Settings.Default.QueuePath);
                    bus.UseRabbitMq();
                    bus.UseXmlSerializer();

                    bus.Subscribe(sub =>
                        {
                            sub.Handler<SampleMessage>((context, message) =>
                                {
                                    var hub = GlobalHost.ConnectionManager.GetHubContext<SampleMessagesHub>();
                                    hub.Clients.All.addNewMessageToPage(message);
                                });
                        });
                });

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
