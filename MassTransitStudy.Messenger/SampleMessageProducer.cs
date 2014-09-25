using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using Magnum;
using MassTransit;
using MassTransitStudy.Messages;
using MassTransitStudy.Messenger.Properties;
using Topshelf;
using Topshelf.Runtime;

namespace MassTransitStudy.Messenger
{
    public class SampleMessageProducer : ServiceControl
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
                    Id = CombGuid.Generate(),
                    Data = "Something",
                    Timestamp = DateTime.UtcNow
                });
        }

        public bool Start(HostControl hostControl)
        {
            this.Timer.Start();
            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            this.Timer.Stop();
            Bus.Shutdown();
            return true;
        }
    }
}
