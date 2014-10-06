namespace MassTransitStudy.Api
{
    using MassTransitStudy.Api.Models;
    using SampleMessageDto = MassTransitStudy.Messages.SampleMessage;

    public static class AutoMapperInstaller
    {
        public static void InstallMappings()
        {
            AutoMapper.Mapper.CreateMap<SampleMessage, SampleMessageDto>();
            AutoMapper.Mapper.CreateMap<SampleMessageDto, SampleMessage>();

            AutoMapper.Mapper.AssertConfigurationIsValid();
        }

        public static TMapTo Map<TMapTo>(this object item)
        {
            return item != null
                ? AutoMapper.Mapper.Map<TMapTo>(item)
                : default(TMapTo);
        }
    }
}
