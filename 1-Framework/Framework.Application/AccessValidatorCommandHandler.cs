using System.Threading.Tasks;

namespace Framework.Application
{
    public class AccessValidatorCommandHandler<T> : ICommandHandler<T> where T : IRestrictedCommand
    {
        private readonly ICommandHandler<T> _decoratee;
        private readonly IValidator validator;

        public AccessValidatorCommandHandler(ICommandHandler<T> decoratee, IValidator validator)
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