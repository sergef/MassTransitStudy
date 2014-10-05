namespace MassTransitStudy.Api.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;
    using MassTransitStudy.Api.MessageStore;
    using MassTransitStudy.Api.Models;

    using SampleMessageDto = MassTransitStudy.Messages.SampleMessage;

    public class MessagesController : ApiController
    {
        public IMessageStoreRepository MessageStore { get; set; }

        [Route("Api/Messages/StartIndex/{StartIndex}/NumberOfItems/{NumberOfItems}")]
        public IEnumerable<SampleMessageDto> Get(int startIndex, int numberOfItems)
        {
            return this.MessageStore
                .GetSampleMessages(startIndex, numberOfItems)
                .Select(item => item.Map<SampleMessageDto>());
        }

        // GET: api/Messages/5
        public string Get(int id)
        {
            return "value";
        }

        [Route("Api/Messages")]
        public SampleMessageDto Post([FromBody]SampleMessageDto message)
        {
            this.MessageStore.SaveSampleMessage(message.Map<SampleMessage>());
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
