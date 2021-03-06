﻿namespace MassTransitStudy.Messenger
{
    using MassTransit;

    using MassTransitStudy.Messenger.Properties;

    using Topshelf;

    class Program
    {
        static void Main(string[] args)
        {
            Bus.Initialize(bus =>
            {
                bus.UseRabbitMq();
                bus.ReceiveFrom(Messages.Properties.Settings.Default.QueuePath);
                bus.UseXmlSerializer();
            });

            HostFactory.Run(config =>
                {
                    config.Service<SampleMessageProducer>(service =>
                        {
                            service.ConstructUsing(name => new SampleMessageProducer());
                            service.WhenStarted((serviceControl, hostControl) => serviceControl.Start(hostControl));
                            service.WhenStopped((serviceControl, hostControl) => serviceControl.Stop(hostControl));
                        });

                    config.RunAsLocalSystem();

                    config.SetDescription(Settings.Default.ServiceDescription);
                    config.SetServiceName(Settings.Default.ServiceName);
                    config.SetDisplayName(Settings.Default.ServiceDisplayName);
                });
        }
    }
}
