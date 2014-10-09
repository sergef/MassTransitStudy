namespace MassTransitStudy.Purser.Consumers
{
    using log4net;

    using MassTransit;

    using MassTransitStudy.Messages;
    using MassTransitStudy.Purser.Properties;

    using StringFormat;

    public class SampleMessageAspNetApiSaveConsumer : Consumes<SampleMessage>.All
    {
        private readonly IApiClient apiClient = new ApiClient(Settings.Default.AspNetApiServiceBaseAddress);

        public ILog Log { get; set; }

        public IServiceBus ServiceBus { get; set; }

        #region All Members

        public void Consume(SampleMessage message)
        {
            this.apiClient.SaveSampleMessage(message);

            this.Log.Info(
                TokenStringFormat.Format(
                    "SampleMessage: {Id}, {Timestamp}, {Data}.",
                    message));
        }

        #endregion
    }
}
