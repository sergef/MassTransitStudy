namespace MassTransitStudy.Api.Models
{
    using System;

    using Cassandra.Data.Linq;

    [AllowFiltering]
    [Table("sampleMessage")]
    public class SampleMessage
    {
        [PartitionKey]
        [Column("id")]
        public Guid Id { get; set; }

        [Column("data")]
        public String Data { get; set; }

        [Column("timeStamp")]
        public DateTime Timestamp { get; set; }
    }
}
