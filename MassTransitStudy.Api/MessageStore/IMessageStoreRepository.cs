﻿namespace MassTransitStudy.Api.MessageStore
{
    using System.Collections.Generic;

    using MassTransitStudy.Messages;

    public interface IMessageStoreRepository
    {
        void CreateSchemaIfNotExists();

        void SaveSampleMessage(SampleMessage message);

        List<SampleMessage> GetSampleMessagesList(int startIndex, int numberOfItems);
    }
}