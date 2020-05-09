using AppService.Contracts.Dtos;
using Sale.Domain.OrderAgg;

namespace AppService.Queries.Convertors
{
    public static class OrderConvertor
    {
        public static OrderDto ToDto(this Order order)
        {
            return new OrderDto
            {
                Id = order.Id,
                CustomerId = order.CustomerId
            };
        }
    }
}