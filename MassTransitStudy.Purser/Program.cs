namespace MassTransitStudy.Purser
{
    using Castle.Windsor;
    using Castle.Windsor.Installer;

    using MassTransit;
    using MassTransit.Log4NetIntegration.Logging;

    using MassTransitStudy.Purser.Properties;

    using Topshelf;

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
