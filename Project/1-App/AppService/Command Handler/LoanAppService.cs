using System.Linq;
using Framework.Application;
using Framework.Domain.Repository;
using AppService.Contracts;
using System.Threading.Tasks;
using Framework.Data.EF;
using AppService.Contracts.Commands.Loans;
using Domain.Logs;
using Domain.Person;
using System.Threading;

namespace AppService
{
    public class LoanAppService : ICommandHandler<CreateLoanCommand>//, ICommandHandler<EditOrderCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Log> _logRepository;

        public LoanAppService(IUnitOfWork unitOfWork, IRepository<Log> logRepository)
        {
            _unitOfWork = unitOfWork;
            _logRepository = logRepository;
        }
        //public void CreateOrder(long customerId, List<OrderLineDto> lines)
        //{
        //    var order = new Order(customerId);
        //    order.SetOrderLines(lines.Select(dto => new OrderLine(dto.ProductId,dto.Quantity)).ToList());
        //    _orderRepository.Add(order);
        //}
        public async Task HandleAsync(CreateLoanCommand command, CancellationToken cancellationToken )
        {
             await _logRepository.AddAsync(new Log
            {
                MessageType = command.PersonId.ToString()
            }); ;
            //var order = new Order(command.CustomerId);
            //order.SetOrderLines(command.OrderLines.Select(dto => new OrderLine(dto.ProductId, dto.Quantity)).ToList());
            //_orderRepository.Add(order);
        }

        //public async Task HandleAsync(EditOrderCommand command)
        //{

        //}
    }

    //public class OrderEventHandler : IEventHandler<OrderCreated>
    //{
    //    public void Handle(OrderCreated @event)
    //    {
    //        //if (true)
    //        //throw new Exception();
    //        //var s = 3;

    //    }
    //}
}
