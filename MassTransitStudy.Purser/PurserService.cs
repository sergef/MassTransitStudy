namespace MassTransitStudy.Purser
{
    using MassTransit;

    using Topshelf;

    public class PurserService : ServiceControl
    {
        public IServiceBus ServiceBus { get; set; }

        public bool Start(HostControl hostControl)
        {
            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            return true;
        }
    }
}
