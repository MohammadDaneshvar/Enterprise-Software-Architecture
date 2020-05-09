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
        public void Handle(T command)
        {
            validator.Validate(command);
            _decoratee.Handle(command);
        }
    }
}