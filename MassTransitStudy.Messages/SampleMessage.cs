namespace MassTransitStudy.Messages
{
    using System;

    [Serializable]
    public class SampleMessage
    {
        public Guid Id { get; set; }

        public String Data { get; set; }

        public DateTime Timestamp { get; set; }
    }
}
