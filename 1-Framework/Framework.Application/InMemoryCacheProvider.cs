using System.Collections.Generic;

namespace Framework.Application
{
    public class InMemoryCacheProvider : ICacheProvider
    {
        public InMemoryCacheProvider()
        {
            Storage = new Dictionary<string, object>();
        }
        private Dictionary<string, object> Storage { get; set; }
        public void Add(string key, object value)
        {
            Storage[key] = value;
        }

        public object Get(string key)
        {
            return Storage.ContainsKey(key)? Storage[key]:null;
        }
    }
}