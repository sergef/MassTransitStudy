namespace MassTransitStudy.Messages
{
    using System;
    using System.Runtime.Serialization;

    [DataContract]
    public class SampleMessage
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public String Data { get; set; }

        [DataMember]
        public DateTime Timestamp { get; set; }
    }
}
