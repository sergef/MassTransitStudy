using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using MassTransit;
using MassTransitStudy.Messages;

namespace MassTransitStudy.Web.Hubs
{
    public class SampleMessagesHub : Hub
    {
        public void Send(string name, string message)
        {
            Clients.All.addNewMessageToPage(name, message);
        }
    }
}