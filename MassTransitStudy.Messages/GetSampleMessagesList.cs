using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassTransitStudy.Messages
{
    [Serializable]
    public class GetSampleMessagesList
    {
        public Guid CorrelationId
        {
            get;
            set;
        }

        public int StartIndex
        {
            get;
            set;
        }

        public int NumberOfItems
        {
            get;
            set;
        }
    }
}
