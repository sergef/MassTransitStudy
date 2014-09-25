using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassTransit.Saga;
using MassTransitStudy.Messages;

namespace MassTransitStudy.Purser
{
    public class PurserSaga :
        ISaga,
        InitiatedBy<SampleMessage>
    {
        #region CorrelatedBy<Guid> Members

        public Guid CorrelationId { get; set; }

        #endregion

        #region ISaga Members

        public MassTransit.IServiceBus Bus
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        #endregion

        #region All Members

        public void Consume(SampleMessage message)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
