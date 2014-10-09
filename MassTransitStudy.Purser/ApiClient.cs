namespace MassTransitStudy.Purser
{
    using System;
    using System.Collections.Generic;

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

        public SampleMessage SaveSampleMessage(SampleMessage message)
        {
            var request = new RestRequest
                {
                    Resource = "Api/Messages",
                    RequestFormat = DataFormat.Json,
                    Method = Method.POST
                };

            request.AddBody(message);

            return this.SendRequest<SampleMessage>(request);
        }

        public List<SampleMessage> GetSampleMessages(int startIndex, int numberOfItems)
        {
            var request = new RestRequest
            {
                Resource = "Api/Messages/StartIndex/{StartIndex}/NumberOfItems/{NumberOfItems}",
                RequestFormat = DataFormat.Json,
                Method = Method.GET
            };

            request.AddParameter("StartIndex", startIndex);
            request.AddParameter("NumberOfItems", numberOfItems);

            return this.SendRequest<List<SampleMessage>>(request);
        }

        public T SendRequest<T>(IRestRequest request) where T : new()
        {
            var client = new RestClient
                {
                    BaseUrl = this.ApiServiceBaseAddress
                };

            var response = client.Execute<T>(request);

            if (response.ErrorException != null)
            {
                throw new ApplicationException("Exception occured.", response.ErrorException);
            }
            
            return response.Data;
        }

        #endregion
    }
}
