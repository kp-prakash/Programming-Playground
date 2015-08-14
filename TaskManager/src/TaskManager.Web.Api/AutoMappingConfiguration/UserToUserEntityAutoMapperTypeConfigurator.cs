namespace TaskManager.Web.Api.AutoMappingConfiguration
{
    using AutoMapper;
    using TaskManager.Common.TypeMapping;
    using TaskManager.Web.Api.Models;

    public class UserToUserEntityAutoMapperTypeConfigurator : IAutoMapperTypeConfigurator
    {
        public void Configure()
        {
            Mapper.CreateMap<User, Data.Entities.User>()
                .ForMember(opt => opt.Version, x => x.Ignore());
        }
    }
}