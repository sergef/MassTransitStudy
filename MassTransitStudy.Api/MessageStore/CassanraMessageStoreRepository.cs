namespace MassTransitStudy.Api.MessageStore
{
    using System;
    using System.Collections.Generic;

    using Cassandra;

    using MassTransitStudy.Messages;

    using StringFormat;

    public class CassanraMessageStoreRepository : IMessageStoreRepository
    {
        public ICluster Cluster
        {
            get;
            set;
        }

        #region IPurseRepository Members

        public void CreateSchemaIfNotExists()
        {
            using (var session = this.Cluster.Connect())
            {
                session.Execute(
                    @"CREATE KEYSPACE MassTransitStudy
                    WITH replication = { 'class' : 'SimpleStrategy', 'replication_factor' : 3 };");

                session.Execute(
                    @"CREATE TABLE MassTransitStudy.SampleMessages (
                    Id uuid PRIMARY KEY,
                    MessageTimestamp text,
                    MessageData text);");
            }
        }

        public void SaveSampleMessage(SampleMessage message)
        {
            using (var session = this.Cluster.Connect())
            {
                session.Execute(
                    TokenStringFormat.Format(
                        @"INSERT INTO MassTransitStudy.SampleMessages
                        (Id, MessageTimestamp, MessageData)
                        VALUES ({Id}, '{Timestamp}', '{Data}');",
                        message));
            }
        }

        public List<SampleMessage> GetSampleMessagesList(int startIndex, int numberOfItems)
        {
            var result = new List<SampleMessage>();

            using (var session = this.Cluster.Connect())
            {
                var rowSet = session.Execute(
                    @"SELECT * FROM MassTransitStudy.SampleMessages;");

                foreach (var row in rowSet.GetRows())
                {
                    result.Add(new SampleMessage
                        {
                            Id = row.GetValue<Guid>("id"),
                            Timestamp = DateTime.Parse(row.GetValue<string>("messagetimestamp")),
                            Data = row.GetValue<string>("messagedata")
                        });
                }
            }

            return result;
        }

        #endregion
    }
}
