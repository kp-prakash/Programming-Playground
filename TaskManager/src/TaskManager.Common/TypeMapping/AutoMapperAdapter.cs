namespace TaskManager.Common.TypeMapping
{
    using AutoMapper;

    public class AutoMapperAdapter : IAutoMapper
    {
        public T Map<T>(object objectToMap)
        {
            return Mapper.Map<T>(objectToMap);
        }
    }
}