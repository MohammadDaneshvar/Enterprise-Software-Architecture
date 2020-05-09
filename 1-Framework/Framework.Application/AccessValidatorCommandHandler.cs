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
        public void Handle(T command)
        {
            validator.Validate(command);
            _decoratee.Handle(command);
        }
    }
}