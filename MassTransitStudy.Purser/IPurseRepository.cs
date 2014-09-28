namespace MassTransitStudy.Purser
{
    using System.Collections.Generic;

    using MassTransitStudy.Messages;

    public interface IPurseRepository
    {
        void CreateSchemaIfNotExists();

        void SaveSampleMessage(SampleMessage message);

        List<SampleMessage> GetSampleMessagesList(int startIndex, int numberOfItems);
    }
}
