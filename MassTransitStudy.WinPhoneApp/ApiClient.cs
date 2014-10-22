namespace MassTransitStudy.WinPhoneApp
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using MassTransitStudy.Messages;

    using RestSharp;

    public class ApiClient : IApiClient
    {
        private readonly string ApiServiceBaseAddress;

        public ApiClient(string apiServiceBaseAddress)
        {
            this.ApiServiceBaseAddress = apiServiceBaseAddress;
        }

        #region IApiClient Members

        public RestRequestAsyncHandle SaveSampleMessageAsync(SampleMessage message, Action<IRestResponse<SampleMessage>, RestRequestAsyncHandle> callback)
        {
            var request = new RestRequest
            {
                Resource = @"Api/Messages",
                RequestFormat = DataFormat.Json,
                Method = Method.POST
            };

            request.AddBody(message);

            return this.SendRequestAsync(request, callback);
        }

        public RestRequestAsyncHandle GetSampleMessagesAsync(int startIndex, int numberOfItems, Action<IRestResponse<List<SampleMessage>>, RestRequestAsyncHandle> callback)
        {
            var request = new RestRequest
            {
                Resource = string.Format(@"Api/Messages"),
                RequestFormat = DataFormat.Json,
                Method = Method.GET
            };

            request.AddParameter("StartIndex", startIndex);
            request.AddParameter("NumberOfItems", numberOfItems);
            
            return this.SendRequestAsync(request, callback);
        }

        public RestRequestAsyncHandle SendRequestAsync<T>(IRestRequest request, Action<IRestResponse<T>, RestRequestAsyncHandle> callback) where T : new()
        {
            var client = new RestClient
            {
                BaseUrl = this.ApiServiceBaseAddress
            };

            return client.ExecuteAsync(request, callback);
        }

        #endregion
    }
}
