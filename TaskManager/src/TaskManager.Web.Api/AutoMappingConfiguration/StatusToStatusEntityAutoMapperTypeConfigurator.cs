namespace TaskManager.Web.Api.AutoMappingConfiguration
{
    using AutoMapper;
    using TaskManager.Common.TypeMapping;
    using TaskManager.Web.Api.Models;

    public class StatusToStatusEntityAutoMapperTypeConfigurator : IAutoMapperTypeConfigurator
    {
        public void Configure()
        {
            Mapper.CreateMap<Status, Data.Entities.Status>()
                .ForMember(opt => opt.Version, x => x.Ignore());
        }
    }
}