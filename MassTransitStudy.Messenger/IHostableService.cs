using System;
using Topshelf.Runtime;

namespace MassTransitStudy.Messenger
{
    interface IHostableService
    {
        void Shutdown();
        void Start();
        void Stop();
    }
}
