
namespace ASPPatterns.Chap2.Service
{
    public class NullObjectCache : ICacheStorage
    {
        public void Remove(string key)
        {
            // Do nothing
        }
        public void Store(string key, object data)
        {
            // Do nothing
        }
        public T Retrieve<T>(string storageKey)
        {
            return default(T);
        }
    }
}