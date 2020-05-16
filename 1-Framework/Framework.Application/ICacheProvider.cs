using System.Threading.Tasks;

namespace Framework.Application
{
    public interface ICacheProvider
    {
        Task AddAsync(string key, object value);
        Task<object> GetAsync(string key);
    }
}