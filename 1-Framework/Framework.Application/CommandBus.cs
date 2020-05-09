namespace Framework.Application
{
    public class CommandBus : ICommandBus
    {
        private readonly ICommandHandlerFactory _factory;

        public CommandBus(ICommandHandlerFactory factory)
        {
            _factory = factory;
        }
        public void Dispatch<T>(T command)
        {
            _factory.CreateHandler<T>().Handle(command);
        }
    }
}