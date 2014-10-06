namespace MassTransitStudy.Api.MessageStore
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Cassandra;
    using Cassandra.Data.Linq;

    using MassTransitStudy.Api.Models;
    using MassTransitStudy.Api.Properties;

    public class CassandraMessageStoreRepository : IMessageStoreRepository
    {
        public ICluster Cluster { get; set; }

        #region IMessageStoreRepository Members

        public void CreateSampleMessageSchemaIfNotExists()
        {
            using (var session = this.Cluster.Connect())
            {
                session.CreateKeyspaceIfNotExists(Settings.Default.CassandraMessageStoreKeyspace);
                session.ChangeKeyspace(Settings.Default.CassandraMessageStoreKeyspace);

                var sampleMessagesTable = session.GetTable<SampleMessage>();
                sampleMessagesTable.CreateIfNotExists();
            }
        }

        public void AddSampleMessage(SampleMessage message)
        {
            using (var session = this.Cluster.Connect(Settings.Default.CassandraMessageStoreKeyspace))
            {
                var sampleMessagesTable = session.GetTable<SampleMessage>();

                var batch = session.CreateBatch();
                
                batch.Append(sampleMessagesTable.Insert(message));
                batch.Execute();
            }
        }

        public List<SampleMessage> GetSampleMessages(int startIndex, int numberOfItems)
        {
            using (var session = this.Cluster.Connect(Settings.Default.CassandraMessageStoreKeyspace))
            {
                var sampleMessagesTable = session.GetTable<SampleMessage>();

                return (from item in sampleMessagesTable select item)
                    .Execute()
                    .ToList();
            }
        }

        public SampleMessage GetSampleMessage(Guid id)
        {
            using (var session = this.Cluster.Connect(Settings.Default.CassandraMessageStoreKeyspace))
            {
                var sampleMessagesTable = session.GetTable<SampleMessage>();

                return sampleMessagesTable
                    .FirstOrDefault(item => item.Id == id)
                    .Execute();
            }
        }

        public void DeleteSampleMessage(Guid id)
        {
            using (var session = this.Cluster.Connect(Settings.Default.CassandraMessageStoreKeyspace))
            {
                var sampleMessagesTable = session.GetTable<SampleMessage>();

                sampleMessagesTable
                    .Where(item => item.Id == id)
                    .Delete()
                    .Execute();
            }
        }

        public void UpdateSampleMessage(Guid id, SampleMessage message)
        {
            using (var session = this.Cluster.Connect(Settings.Default.CassandraMessageStoreKeyspace))
            {
                var sampleMessagesTable = session.GetTable<SampleMessage>();

                sampleMessagesTable
                    .Where(item => item.Id == id)
                    .Select(item => new SampleMessage
                        {
                            Data = message.Data
                        })
                    .Update()
                    .Execute();
            }
        }

        #endregion
    }
}
