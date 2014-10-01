namespace MassTransitStudy.Api.Controllers
{
    using System.Collections.Generic;
    using System.Web.Http;

    using MassTransitStudy.Api.MessageStore;
    using MassTransitStudy.Messages;

    public class MessagesController : ApiController
    {
        public IMessageStoreRepository MessageStore { get; set; }

        [Route("Api/Messages/StartIndex/{StartIndex}/NumberOfItems/{NumberOfItems}")]
        public IEnumerable<SampleMessage> Get(int startIndex, int numberOfItems)
        {
            return this.MessageStore.GetSampleMessages(startIndex, numberOfItems);
        }

        // GET: api/Messages/5
        public string Get(int id)
        {
            return "value";
        }

        [Route("Api/Messages")]
        public SampleMessage Post([FromBody]SampleMessage message)
        {
            this.MessageStore.SaveSampleMessage(message);
            return message;
        }

        // PUT: api/Messages/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Messages/5
        public void Delete(int id)
        {
        }
    }
}
