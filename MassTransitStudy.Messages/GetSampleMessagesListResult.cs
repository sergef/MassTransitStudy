using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassTransitStudy.Messages
{
    [Serializable]
    public class GetSampleMessagesListResult : GetSampleMessagesList
    {
        public List<SampleMessage> Result
        {
            get;
            set;
        }
    }
}
