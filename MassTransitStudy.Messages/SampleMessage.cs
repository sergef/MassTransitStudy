using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassTransit;

namespace MassTransitStudy.Messages
{
    [Serializable]
    public class SampleMessage : CorrelatedBy<Guid>
    {
        public Guid CorrelationId { get; set; }

        public String Data { get; set; }

        public DateTime Timestamp { get; set; }
    }
}
