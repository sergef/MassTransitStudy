namespace MassTransitStudy.Api
{
    using Cassandra;

    using Castle.MicroKernel.Registration;
    using Castle.MicroKernel.SubSystems.Configuration;
    using Castle.Windsor;

    using log4net;

    using MassTransitStudy.Api.Controllers;
    using MassTransitStudy.Api.MessageStore;
    using MassTransitStudy.Api.Properties;

    using Topshelf;

    public class ContainerInstaller : IWindsorInstaller
    {
        #region IWindsorInstaller Members

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component
                    .For<IMessageStoreRepository>()
                    .ImplementedBy<CassandraMessageStoreRepository>(),
                Component
                    .For<MessagesController>()
                    .LifeStyle
                    .Transient,
                Component
                    .For<ServiceControl>()
                    .ImplementedBy<ApiService>()
                    .LifeStyle
                    .Singleton,
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
                    .OnDestroy(cluster => cluster.Shutdown()));
        }

        #endregion
    }
}
