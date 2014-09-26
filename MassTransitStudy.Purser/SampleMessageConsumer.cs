﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cassandra;
using log4net;
using MassTransit;
using MassTransitStudy.Messages;
using StringFormat;

namespace MassTransitStudy.Purser
{
    public class SampleMessageConsumer :
        Consumes<SampleMessage>.All,
        Consumes<GetSampleMessagesList>.All
    {
        public ILog Log
        {
            get;
            set;
        }

        public IServiceBus ServiceBus
        {
            get;
            set;
        }

        public ICluster Cluster
        {
            get;
            set;
        }

        public IPurseRepository Repository
        {
            get;
            set;
        }

        #region All Members

        public void Consume(SampleMessage message)
        {
            this.Repository.SaveSampleMessage(message);

            Log.Info(
                TokenStringFormat.Format(
                    "SampleMessage: {Id}, {Timestamp}, {Data}.",
                    message));
        }

        public void Consume(GetSampleMessagesList message)
        {
            ServiceBus.Publish(new GetSampleMessagesListResult
                {
                    CorrelationId = message.CorrelationId,
                    StartIndex = message.StartIndex,
                    NumberOfItems = message.NumberOfItems,
                    Result = this.Repository.GetSampleMessagesList(message.StartIndex, message.NumberOfItems)
                });

            Log.Info(
                TokenStringFormat.Format(
                    "GetSampleMessagesList: {CorrelationId}.",
                    message));
        }

        #endregion
    }
}
