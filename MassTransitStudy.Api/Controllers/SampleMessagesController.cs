using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MassTransitStudy.Messages;

namespace MassTransitStudy.Api.Controllers
{
    public class SampleMessagesController : ApiController
    {
        public IEnumerable<SampleMessage> GetAllSampleMessages()
        {
            return new List<SampleMessage>();
        }
    }
}
