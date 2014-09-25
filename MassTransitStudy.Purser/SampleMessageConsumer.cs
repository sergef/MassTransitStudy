using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cassandra;
using log4net;
using MassTransit;
using MassTransitStudy.Messages;
using StringFormat;

namespace MassTransitStudy.Purser
{
    public class SampleMessageConsumer : Consumes<SampleMessage>.All
    {
        public ILog Log
        {
            get;
            set;
        }

        public IServiceBus ServiceBus
        {
            get;
            set;
        }

        public ICluster Cluster
        {
            get;
            set;
        }

        public IPurseRepository Repository
        {
            get;
            set;
        }

        #region All Members

        public void Consume(SampleMessage message)
        {
            Log.Info(
                TokenStringFormat.Format(
                    "Consuming Message: {Id}, {Timestamp}, {Data}.",
                    message));

            this.Repository.SaveSampleMessage(message);

            Log.Info(
                TokenStringFormat.Format(
                    "Saved Message: {Id}, {Timestamp}, {Data}.",
                    message));
        }

        #endregion
    }
}
