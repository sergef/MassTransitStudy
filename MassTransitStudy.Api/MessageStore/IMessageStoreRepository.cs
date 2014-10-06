namespace MassTransitStudy.Api.MessageStore
{
    using System;
    using System.Collections.Generic;

    using MassTransitStudy.Api.Models;

    public interface IMessageStoreRepository
    {
        void CreateSampleMessageSchemaIfNotExists();

        void AddSampleMessage(SampleMessage message);

        List<SampleMessage> GetSampleMessages(int startIndex, int numberOfItems);

        SampleMessage GetSampleMessage(Guid id);

        void DeleteSampleMessage(Guid id);

        void UpdateSampleMessage(Guid id, SampleMessage message);
    }
}
