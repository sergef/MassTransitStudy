namespace MassTransitStudy.Purser
{
    using System.Collections.Generic;

    using MassTransitStudy.Messages;

    using RestSharp;

    public interface IApiClient
    {
        SampleMessage SaveSampleMessage(SampleMessage message);

        List<SampleMessage> GetSampleMessages(int startIndex, int numberOfItems);

        T SendRequest<T>(IRestRequest request) where T : new();
    }
}
