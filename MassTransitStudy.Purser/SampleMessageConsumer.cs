namespace MassTransitStudy.Purser
{
    using System.Collections.Generic;

    using log4net;

    using MassTransit;

    using MassTransitStudy.Messages;

    using StringFormat;

    public class SampleMessageConsumer :
        Consumes<SampleMessage>.All,
        Consumes<GetSampleMessagesList>.All
    {
        public ILog Log { get; set; }

        public IServiceBus ServiceBus { get; set; }

        public IApiClient ApiClient { get; set; }

        #region All Members

        public void Consume(SampleMessage message)
        {
            this.ApiClient.SaveSampleMessage(message);

            Log.Info(
                TokenStringFormat.Format(
                    "SampleMessage: {Id}, {Timestamp}, {Data}.",
                    message));
        }

        public void Consume(GetSampleMessagesList message)
        {
            var sampleMessages = this.ApiClient.GetSampleMessages(message.StartIndex, message.NumberOfItems);

            this.ServiceBus.Publish(new GetSampleMessagesListResult
                {
                    CorrelationId = message.CorrelationId,
                    StartIndex = message.StartIndex,
                    NumberOfItems = message.NumberOfItems,
                    Result = sampleMessages
                });

            Log.Info(
                TokenStringFormat.Format(
                    "GetSampleMessagesList: {CorrelationId}.",
                    message));
        }

        #endregion
    }
}
