using Framework.Application;
using AppService.Contracts.Queries;
using AppService.Queries.Convertors;
using Sale.Domain.Query.Finders;

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

        public void Handle(GetOrderByIdQuery command) => command.Result = SaleFinder.FindById(command.Id);
    }
}