namespace MassTransitStudy.Messages
{
    using System;
    using System.Collections.Generic;

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
