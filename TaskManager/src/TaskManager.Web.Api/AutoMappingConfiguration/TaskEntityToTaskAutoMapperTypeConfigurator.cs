namespace TaskManager.Web.Api.AutoMappingConfiguration
{
    using AutoMapper;
    using TaskManager.Common.TypeMapping;
    using TaskManager.Data.Entities;

    public class TaskEntityToTaskAutoMapperTypeConfigurator : IAutoMapperTypeConfigurator
    {
        public void Configure()
        {
            Mapper.CreateMap<Task, Models.Task>()
                .ForMember(opt => opt.Links, x => x.Ignore())
                .ForMember(opt => opt.Assignees, x => x.ResolveUsing<TaskAssigneesResolver>());
        }
    }
}