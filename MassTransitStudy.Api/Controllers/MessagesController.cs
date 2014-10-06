namespace MassTransitStudy.Api.Controllers
{
    using System;
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

        [Route("Api/Messages/{Id}")]
        public SampleMessageDto Get(Guid id)
        {
            return this.MessageStore
                .GetSampleMessage(id)
                .Map<SampleMessageDto>();
        }

        [Route("Api/Messages")]
        public SampleMessageDto Post([FromBody]SampleMessageDto message)
        {
            this.MessageStore.AddSampleMessage(message.Map<SampleMessage>());
            return message;
        }

        [Route("Api/Messages/{Id}")]
        public void Put(Guid id, [FromBody]SampleMessageDto message)
        {
            this.MessageStore.UpdateSampleMessage(id, message.Map<SampleMessage>());
        }

        [Route("Api/Messages/{Id}")]
        public void Delete(Guid id)
        {
            this.MessageStore.DeleteSampleMessage(id);
        }
    }
}
