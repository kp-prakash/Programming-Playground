using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ASPPatterns.Chap2.Service
{
    public interface ICacheStorage
    {
        void Store(string key, object obj);
        T Retrieve<T>(string key);
        void Remove(string key);
    }
}
