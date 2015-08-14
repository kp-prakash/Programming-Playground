namespace TaskManager.Web.Api.AutoMappingConfiguration
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using TaskManager.Common.TypeMapping;
    using TaskManager.Data.Entities;
    using TaskManager.Web.Common;

    public class TaskAssigneesResolver : ValueResolver<Task, List<User>>
    {
        public IAutoMapper AutoMapper
        {
            get { return WebContainerManager.Get<IAutoMapper>(); }
        }

        protected override List<User> ResolveCore(Task source)
        {
            return source.Users.Select(x => AutoMapper.Map<User>(x)).ToList();
        }
    }
}