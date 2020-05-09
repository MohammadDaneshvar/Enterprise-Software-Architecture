namespace Framework.Application
{
    public interface ICacheProvider
    {
        void Add(string key, object value);
        object Get(string key);
    }
}