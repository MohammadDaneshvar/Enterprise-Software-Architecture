using System.Collections.Generic;
using System.Threading.Tasks;

namespace Framework.Application
{
    public class InMemoryCacheProvider : ICacheProvider
    {
        public InMemoryCacheProvider()
        {
            Storage = new Dictionary<string, object>();
        }
        private Dictionary<string, object> Storage { get; set; }
        public async Task  AddAsync(string key, object value)
        {
            Storage[key] = value;
        }

        public async Task<object> GetAsync(string key)
        {
            return Storage.ContainsKey(key)? Storage[key]:null;
        }
    }
}