using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using Magnum;
using MassTransit;
using MassTransitStudy.Messages;
using MassTransitStudy.Messenger.Properties;
using Topshelf.Runtime;

namespace MassTransitStudy.Messenger
{
    public class SampleMessageProducer : IHostableService
    {
        private readonly Timer Timer;

        public SampleMessageProducer()
        {
            this.Timer = new Timer()
                {
                    Interval = Settings.Default.MessageProductionInterval,
                    AutoReset = true
                };

            this.Timer.Elapsed += TimerElapsed;
        }

        ~SampleMessageProducer()
        {
            this.Timer.Elapsed -= TimerElapsed;
        }

        void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            Bus.Instance.Publish(new SampleMessage
                {
                    CorrelationId = CombGuid.Generate(),
                    Data = "Something",
                    Timestamp = DateTime.UtcNow
                });
        }

        public void Start()
        {
            Bus.Initialize(bus =>
                {
                    bus.UseRabbitMq();
                    bus.ReceiveFrom(Messages.Properties.Settings.Default.QueuePath);
                    bus.UseXmlSerializer();
                });

            this.Timer.Start();
        }

        public void Stop()
        {
            this.Timer.Stop();
            Bus.Shutdown();
        }

        public void Shutdown()
        {
            this.Timer.Stop();
            Bus.Shutdown();
        }
    }
}
