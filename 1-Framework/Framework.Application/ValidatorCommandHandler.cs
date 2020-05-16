using System.Threading.Tasks;

namespace Framework.Application
{
    public class ValidatorCommandHandler<T> : ICommandHandler<T>
    {
        private readonly ICommandHandler<T> _decoratee;
        private readonly IValidator validator;

        public ValidatorCommandHandler(ICommandHandler<T> decoratee, IValidator validator)
        {
            _decoratee = decoratee;
            this.validator = validator;
        }
        public async Task HandleAsync(T command)
        {
            validator.Validate(command);
            await _decoratee.HandleAsync(command);
        }
    }
}