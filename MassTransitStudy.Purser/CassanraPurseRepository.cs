using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cassandra;
using MassTransitStudy.Messages;
using StringFormat;

namespace MassTransitStudy.Purser
{
    public class CassanraPurseRepository : IPurseRepository
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

        #endregion
    }
}
