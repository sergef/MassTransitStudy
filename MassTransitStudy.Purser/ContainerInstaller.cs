namespace MassTransitStudy.Purser
{
    using Castle.MicroKernel.Registration;
    using Castle.MicroKernel.SubSystems.Configuration;
    using Castle.Windsor;

    using log4net;

    using MassTransit;

    using MassTransitStudy.Purser.Consumers;
    using MassTransitStudy.Purser.Properties;

    using Topshelf;

    public class ContainerInstaller : IWindsorInstaller
    {
        #region IWindsorInstaller Members

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Types
                    .FromThisAssembly()
                    .BasedOn<IConsumer>(),
                Component
                    .For<IApiClient>()
                    .ImplementedBy<ApiClient>()
                    .DependsOn(
                        Dependency.OnValue("apiServiceBaseAddress", Settings.Default.AspNetApiServiceBaseAddress)),
                Component
                    .For<ILog>()
                    .UsingFactoryMethod(() =>
                        LogManager
                            .GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType)),
                Component
                    .For<ServiceControl>()
                    .ImplementedBy<PurserService>()
                    .LifeStyle
                    .Singleton,
                Component
                    .For<IServiceBus>()
                    .UsingFactoryMethod(() => 
                        ServiceBusFactory.New(bus =>
                            {
                                bus.UseRabbitMq();
                                bus.ReceiveFrom(Messages.Properties.Settings.Default.QueuePath);
                                bus.UseXmlSerializer();
                                bus.UseControlBus();

                                bus.Subscribe(s => s.LoadFrom(container));
                            }))
                    .LifeStyle
                    .Singleton);
        }

        #endregion
    }
}
