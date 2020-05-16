using System.Linq;
using Framework.Application;
using Framework.Domain.Repository;
using AppService.Contracts;
using Sale.Domain.Query.Model;
using Sale.Domain.OrderAgg;
using System.Threading.Tasks;

namespace AppService
{
    public class SaleAppService : ICommandHandler<CreateOrderCommand>, ICommandHandler<EditOrderCommand>
    {
        //private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Sale.Domain.OrderAgg.Order> _orderRepository;

        public SaleAppService(/*IUnitOfWork unitOfWork,*/IRepository<Sale.Domain.OrderAgg.Order> orderRepository)
        {
            //_unitOfWork = unitOfWork;
            _orderRepository = orderRepository;
        }
        //public void CreateOrder(long customerId, List<OrderLineDto> lines)
        //{
        //    _unitOfWork.Begin();
        //    var order = new Order(customerId);
        //    order.SetOrderLines(lines.Select(dto => new OrderLine(dto.ProductId,dto.Quantity)).ToList());
        //    _orderRepository.Add(order);
        //    _unitOfWork.Commit();
        //}
        public async Task HandleAsync(CreateOrderCommand command)
        {
            //_unitOfWork.Begin();
            //var order = new Order(command.CustomerId);
            //order.SetOrderLines(command.OrderLines.Select(dto => new OrderLine(dto.ProductId, dto.Quantity)).ToList());
            //_orderRepository.Add(order);
            //_unitOfWork.Commit();
        }

        public async Task HandleAsync(EditOrderCommand command)
        {

        }
    }

    public class OrderEventHandler : IEventHandler<OrderCreated>
    {
        public void Handle(OrderCreated @event)
        {
            //if (true)
            //throw new Exception();
            //var s = 3;

        }
    }
}
