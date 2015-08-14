namespace TaskManager.Web.Api.AutoMappingConfiguration
{
    using AutoMapper;
    using TaskManager.Common.TypeMapping;
    using TaskManager.Data.Entities;

    public class StatusEntityToStatusAutoMapperTypeConfigurator : IAutoMapperTypeConfigurator
    {
        public void Configure()
        {
            Mapper.CreateMap<Status, Models.Status>();
        }
    }
}