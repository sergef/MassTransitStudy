namespace MassTransitStudy.Purser
{
    using System;
    using System.Collections.Generic;

    using MassTransitStudy.Messages;
    using MassTransitStudy.Messages.Properties;

    using RestSharp;

    public class ApiClient : IApiClient
    {
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
                    BaseUrl = Settings.Default.ApiServiceBaseAddress
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
