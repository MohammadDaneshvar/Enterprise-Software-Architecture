namespace Framework.Application
{
    public interface IKeyGenerator
    {
        string GenerateKeyForCache(object command);
    }
}