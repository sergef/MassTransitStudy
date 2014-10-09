namespace MassTransitStudy.Purser.Consumers
{
    using log4net;

    using MassTransit;

    using MassTransitStudy.Messages;

    using StringFormat;

    public class GetSampleMessagesListConsumer : Consumes<GetSampleMessagesList>.All
    {
        public ILog Log { get; set; }

        public IServiceBus ServiceBus { get; set; }

        public IApiClient ApiClient { get; set; }

        #region All Members

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

            this.Log.Info(
                TokenStringFormat.Format(
                    "GetSampleMessagesList: {CorrelationId}.",
                    message));
        }

        #endregion
    }
}
