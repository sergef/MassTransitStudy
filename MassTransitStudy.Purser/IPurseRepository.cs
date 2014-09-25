using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cassandra;
using MassTransitStudy.Messages;

namespace MassTransitStudy.Purser
{
    public interface IPurseRepository
    {
        void CreateSchemaIfNotExists();

        void SaveSampleMessage(SampleMessage message);
    }
}
