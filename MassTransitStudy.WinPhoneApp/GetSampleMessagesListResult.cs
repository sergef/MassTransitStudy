namespace MassTransitStudy.WinPhoneApp
{
    using System.Collections.Generic;

    using MassTransitStudy.Messages;

    public class GetSampleMessagesListResult : GetSampleMessagesList
    {
        public List<SampleMessage> Result
        {
            get;
            set;
        }
    }
}
