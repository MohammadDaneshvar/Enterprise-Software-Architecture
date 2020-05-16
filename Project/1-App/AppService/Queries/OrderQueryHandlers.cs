using Framework.Application;
using AppService.Contracts.Queries;
using AppService.Queries.Convertors;
using Sale.Domain.Query.Finders;
using System.Threading.Tasks;

namespace AppService.Queries
{
    public class OrderQueryHandlers : ICommandHandler<GetOrderByIdQuery>
    {
        private readonly ISaleFinder _saleFinder;

        public OrderQueryHandlers(ISaleFinder saleFinder)
        {
            _saleFinder = saleFinder;
        }

        public ISaleFinder SaleFinder => _saleFinder;

        public async Task HandleAsync(GetOrderByIdQuery command) => command.Result = SaleFinder.FindById(command.Id);
    }
}