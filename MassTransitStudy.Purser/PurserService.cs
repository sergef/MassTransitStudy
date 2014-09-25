using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cassandra;
using MassTransit;
using MassTransitStudy.Messages;
using Topshelf;

namespace MassTransitStudy.Purser
{
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
