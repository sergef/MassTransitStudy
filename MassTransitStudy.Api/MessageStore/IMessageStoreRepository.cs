namespace MassTransitStudy.Api.MessageStore
{
    using System.Collections.Generic;

    using MassTransitStudy.Api.Models;

    public interface IMessageStoreRepository
    {
        void CreateSchemaIfNotExists();

        void SaveSampleMessage(SampleMessage message);

        List<SampleMessage> GetSampleMessages(int startIndex, int numberOfItems);
    }
}
