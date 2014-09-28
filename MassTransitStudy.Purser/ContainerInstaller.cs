namespace MassTransitStudy.Purser
{
    using Cassandra;

    using Castle.MicroKernel.Registration;
    using Castle.MicroKernel.SubSystems.Configuration;
    using Castle.Windsor;

    using log4net;

    using MassTransit;

    using MassTransitStudy.Purser.Properties;

    public class ContainerInstaller : IWindsorInstaller
    {
        #region IWindsorInstaller Members

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                //Component
                //    .For(typeof(ISagaRepository<>))
                //    .ImplementedBy(typeof(InMemorySagaRepository<>))
                //    .LifeStyle.Singleton,
                //Component
                //    .For<PurserSaga>()
                //    .LifeStyle.Singleton,
                Types
                    .FromThisAssembly()
                    .BasedOn<IConsumer>(),
                Component
                    .For<IPurseRepository>()
                    .ImplementedBy<CassanraPurseRepository>(),
                Component
                    .For<ILog>()
                    .UsingFactoryMethod(() =>
                        LogManager
                            .GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType)),
                Component
                    .For<ICluster>()
                    .UsingFactoryMethod(() =>
                        Cluster
                            .Builder()
                            .AddContactPoint(Settings.Default.CassandraNode)
                            .Build())
                    .LifestyleTransient()
                    .OnDestroy(cluster => cluster.Shutdown()),
                Component
                    .For<PurserService>()
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
