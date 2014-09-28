namespace MassTransitStudy.Web.Hubs
{
    using System;

    using Magnum;

    using MassTransit;

    using MassTransitStudy.Messages;

    using Microsoft.AspNet.SignalR;

    public class SampleMessagesHub : Hub
    {
        public void Send(string message)
        {
            Bus.Instance.Publish(
                new SampleMessage
                    {
                        Id = CombGuid.Generate(),
                        Data = message,
                        Timestamp = DateTime.UtcNow
                    });
        }

        public void GetSampleMessagesList(int startIndex, int numberOfItems)
        {
            Bus.Instance.Publish(
                new GetSampleMessagesList
                    {
                        CorrelationId = CombGuid.Generate(),
                        StartIndex = startIndex,
                        NumberOfItems = numberOfItems
                    });
        }
    }
}