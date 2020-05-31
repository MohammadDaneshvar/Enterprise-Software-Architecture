

using FluentValidation.Results;

namespace Framework.Application
{
    public interface ICommandValidator<T>
    {
        ValidationResult Validate(T command);
    }
}