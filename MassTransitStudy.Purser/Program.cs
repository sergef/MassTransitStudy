using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cassandra;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using MassTransit;
using MassTransit.Saga;
using MassTransitStudy.Purser.Properties;
using MassTransit.Log4NetIntegration.Logging;
using Topshelf;
using Castle.Windsor.Installer;

namespace MassTransitStudy.Purser
{
    class Program
    {
        static void Main(string[] args)
        {
            Log4NetLogger.Use("log4net.config");

            var container = new WindsorContainer();
            container.Install(FromAssembly.This());

            HostFactory.Run(config =>
            {
                config.RunAsNetworkService();

                config.SetDescription(Settings.Default.ServiceDescription);
                config.SetServiceName(Settings.Default.ServiceName);
                config.SetDisplayName(Settings.Default.ServiceDisplayName);

                config.Service<ServiceControl>(service =>
                    {
                        service.ConstructUsing(builder => container.Resolve<PurserService>());

                        service.WhenStarted((serviceControl, hostControl) => serviceControl.Start(hostControl));

                        service.WhenStopped((serviceControl, hostControl) =>
                            {
                                var result = serviceControl.Stop(hostControl);
                                var serviceBus = container.Resolve<IServiceBus>();

                                serviceBus.Dispose();
                                container.Dispose();
                                
                                return result;
                            });
                    });
            });
        }
    }
}
