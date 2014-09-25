using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassTransit;

namespace MassTransitStudy.Messages
{
    [Serializable]
    public class SampleMessage
    {
        public Guid Id { get; set; }

        public String Data { get; set; }

        public DateTime Timestamp { get; set; }
    }
}
