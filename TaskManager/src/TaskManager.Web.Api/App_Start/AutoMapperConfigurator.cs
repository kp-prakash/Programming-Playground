namespace TaskManager.Web.Api
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using TaskManager.Common.TypeMapping;

    public class AutoMapperConfigurator
    {
        public void Configure(IEnumerable<IAutoMapperTypeConfigurator> autoMapperTypeConfigurators)
        {
            autoMapperTypeConfigurators.ToList().ForEach(x => x.Configure());
            Mapper.AssertConfigurationIsValid();
        }
    }
}