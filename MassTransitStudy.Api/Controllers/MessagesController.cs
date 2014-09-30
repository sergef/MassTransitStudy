namespace MassTransitStudy.Api.Controllers
{
    using System.Collections.Generic;
    using System.Web.Http;

    using MassTransitStudy.Api.MessageStore;
    using MassTransitStudy.Messages;

    public class MessagesController : ApiController
    {
        public IMessageStoreRepository MessageStore { get; set; }

        [Route("api/messages")]
        public IEnumerable<SampleMessage> Get()
        {
            return this.MessageStore.GetSampleMessagesList(0, 100);
        }

        // GET: api/Messages/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Messages
        public void Post([FromBody]string value)
        {
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
