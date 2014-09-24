using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassTransitStudy.Messenger.Properties;
using Topshelf;

namespace MassTransitStudy.Messenger
{
    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(config =>
                {
                    config.Service<SampleMessageProducer>(service =>
                        {
                            service.ConstructUsing(name => new SampleMessageProducer());
                            service.WhenStarted(s => s.Start());
                            service.WhenStopped(s => s.Stop());
                            service.WhenShutdown(s => s.Shutdown());
                        });

                    config.RunAsLocalSystem();

                    config.SetDescription(Settings.Default.ServiceDesciption);
                    config.SetServiceName(Settings.Default.ServiceName);
                    config.SetDisplayName(Settings.Default.ServiceDisplayName);
                });
        }
    }
}
