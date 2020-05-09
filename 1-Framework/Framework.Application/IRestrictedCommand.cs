namespace Framework.Application
{
    public interface IRestrictedCommand
    {
        string Roles { get; }
        string Users { get; }
    }
}