namespace MassTransitStudy.Purser
{
    using Cassandra;

    using MassTransit;

    using Topshelf;

    public class PurserService : ServiceControl
    {
        public IServiceBus ServiceBus
        {
            get;
            set;
        }

        public IPurseRepository Repository
        {
            get;
            set;
        }

        public ICluster Cluster
        {
            get;
            set;
        }

        public bool Start(HostControl hostControl)
        {
            //this.Repository.CreateSchemaIfNotExists();
            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            return true;
        }
    }
}
