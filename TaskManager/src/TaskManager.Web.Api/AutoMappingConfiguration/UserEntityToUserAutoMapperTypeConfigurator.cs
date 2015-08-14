namespace TaskManager.Web.Api.AutoMappingConfiguration
{
    using AutoMapper;
    using TaskManager.Common.TypeMapping;
    using TaskManager.Data.Entities;

    public class UserEntityToUserAutoMapperTypeConfigurator : IAutoMapperTypeConfigurator
    {
        public void Configure()
        {
            Mapper.CreateMap<User, Models.User>()
                .ForMember(opt => opt.Links, x => x.Ignore());
        }
    }
}