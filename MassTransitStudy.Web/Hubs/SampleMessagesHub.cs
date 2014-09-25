using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using MassTransit;
using MassTransitStudy.Messages;
using Magnum;

namespace MassTransitStudy.Web.Hubs
{
    public class SampleMessagesHub : Hub
    {
        public void Send(string message)
        {
            var newMessage = new SampleMessage
                {
                    Id = CombGuid.Generate(),
                    Data = message,
                    Timestamp = DateTime.UtcNow
                };

            Bus.Instance.Publish(newMessage);
        }
    }
}