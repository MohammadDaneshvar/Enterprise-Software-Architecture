using FluentValidation;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Framework.Application
{
    public class ValidatorCommandHandler<T> : ICommandHandler<T>
    {
        private readonly ICommandHandler<T> _decoratee;
        private readonly ICommandValidator<T> _commandValidator;

        public ValidatorCommandHandler(ICommandHandler<T> decoratee, ICommandValidator<T> validator)
        {
            _decoratee = decoratee;
            this._commandValidator = validator;
        }
        public async Task HandleAsync(T command)
        {
            _commandValidator.Validate(command);

            await _decoratee.HandleAsync(command);
        }
    }
}