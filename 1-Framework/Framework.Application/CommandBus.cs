using System.Threading.Tasks;

namespace Framework.Application
{
    public class CommandBus : ICommandBus
    {
        private readonly ICommandHandlerFactory _factory;

        public CommandBus(ICommandHandlerFactory factory)
        {
            _factory = factory;
        }
        public async Task DispatchAsync<T>(T command)
        {
            await _factory.CreateHandler<T>().HandleAsync(command);
        }
    }
}