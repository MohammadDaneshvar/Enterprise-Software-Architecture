namespace Framework.Application
{
    public interface IValidator
    {
        void Validate(object command);
    }
}