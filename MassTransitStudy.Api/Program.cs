namespace MassTransitStudy.Api
{
    using System.Web.Http;
    using System.Web.Http.Dispatcher;
    using Castle.MicroKernel.Registration;
    using Castle.Windsor;
    using Castle.Windsor.Installer;
    using MassTransitStudy.Api.Properties;
    using Topshelf;

    class Program
    {
        static void Main(string[] args)
        {
            var container = new WindsorContainer();
            container.Install(FromAssembly.This());

            container.Register(
                Component.For<HttpConfiguration>().UsingFactoryMethod(
                    () =>
                        {
                            var config = new HttpConfiguration();
                            config.Services.Replace(
                                typeof(IHttpControllerActivator),
                                new ContainerHttpControllerActivator(container));
                            config.MapHttpAttributeRoutes();
                            return config;
                        }));

            HostFactory.Run(config =>
                {
                    config.RunAsNetworkService();

                    config.SetDescription(Settings.Default.ServiceDescription);
                    config.SetServiceName(Settings.Default.ServiceName);
                    config.SetDisplayName(Settings.Default.ServiceDisplayName);

                    config.Service<ServiceControl>(service =>
                        {
                            service.ConstructUsing(builder => container.Resolve<ServiceControl>());
                            service.WhenStarted((serviceControl, hostControl) => serviceControl.Start(hostControl));
                            service.WhenStopped((serviceControl, hostControl) => serviceControl.Stop(hostControl));
                        });
                });
        }
    }
}
