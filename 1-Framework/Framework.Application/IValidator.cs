namespace Framework.Application
{
    public interface ICommandValidator<T>
    {
        void Validate(T command);
    }
}