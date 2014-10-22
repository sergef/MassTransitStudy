namespace MassTransitStudy.WinPhoneApp
{
    using System;
    using System.Collections.Generic;

    using MassTransitStudy.Messages;

    using RestSharp;

    public interface IApiClient
    {
        RestRequestAsyncHandle SaveSampleMessageAsync(
            SampleMessage message,
            Action<IRestResponse<SampleMessage>, RestRequestAsyncHandle> callback);

        RestRequestAsyncHandle GetSampleMessagesAsync(
            int startIndex,
            int numberOfItems,
            Action<IRestResponse<List<SampleMessage>>, RestRequestAsyncHandle> callback);

        RestRequestAsyncHandle SendRequestAsync<T>(
            IRestRequest request,
            Action<IRestResponse<T>, RestRequestAsyncHandle> callback) where T : new();
    }
}
