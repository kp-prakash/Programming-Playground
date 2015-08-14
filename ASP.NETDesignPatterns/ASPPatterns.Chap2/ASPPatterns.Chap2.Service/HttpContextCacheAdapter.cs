using System.Web;

namespace ASPPatterns.Chap2.Service
{
    public class HttpContextCacheAdapter : ICacheStorage
    {

        public void Store(string key, object obj)
        {
            HttpContext.Current.Cache.Insert(key, obj);
        }

        public T Retrieve<T>(string key)
        {
            var itemStored = (T)HttpContext.Current.Cache.Get(key);
            if (itemStored != null) return itemStored;
            itemStored = default(T);
            return itemStored;
        }

        public void Remove(string key)
        {
            HttpContext.Current.Cache.Remove(key);
        }
    }
}
