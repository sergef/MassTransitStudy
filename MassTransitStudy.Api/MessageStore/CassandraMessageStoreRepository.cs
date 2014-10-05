namespace MassTransitStudy.Api.MessageStore
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Cassandra;
    using Cassandra.Data.Linq;

    using MassTransitStudy.Api.Models;
    using MassTransitStudy.Api.Properties;

    using StringFormat;

    public class CassandraMessageStoreRepository : IMessageStoreRepository
    {
        public ICluster Cluster { get; set; }

        #region IPurseRepository Members

        public void CreateSchemaIfNotExists()
        {
            using (var session = this.Cluster.Connect())
            {
                session.CreateKeyspaceIfNotExists(Settings.Default.CassandraMessageStoreKeyspace);
                session.ChangeKeyspace(Settings.Default.CassandraMessageStoreKeyspace);

                var sampleMessagesTable = session.GetTable<SampleMessage>();
                sampleMessagesTable.CreateIfNotExists();
            }
        }

        public void SaveSampleMessage(SampleMessage message)
        {
            using (var session = this.Cluster.Connect())
            {
                session.ChangeKeyspace(Settings.Default.CassandraMessageStoreKeyspace);
                var sampleMessagesTable = session.GetTable<SampleMessage>();

                var batch = session.CreateBatch();
                
                batch.Append(sampleMessagesTable.Insert(message));
                batch.Execute();
            }
        }

        public List<SampleMessage> GetSampleMessages(int startIndex, int numberOfItems)
        {
            using (var session = this.Cluster.Connect())
            {
                session.ChangeKeyspace(Settings.Default.CassandraMessageStoreKeyspace);
                var sampleMessagesTable = session.GetTable<SampleMessage>();

                return (from item in sampleMessagesTable select item)
                    .Execute()
                    .ToList();
            }
        }

        #endregion
    }
}
