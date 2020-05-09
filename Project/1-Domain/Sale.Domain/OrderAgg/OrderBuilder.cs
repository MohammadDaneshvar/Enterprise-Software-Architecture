using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sale.Domain.OrderAgg
{
    public class OrderBuilder : IOrderABuilder, IOrderBBuilder, IOrderBuilder
    {
        private Order order;
        private bool orderLinesSet;
        public OrderBuilder() { }
        //public OrderBuilder WithOrderLineInBasket(Basket basket)
        //{
        //    this.order.SetOrderLines(orderLines);
        //    return this;
        //}

        Order IOrderBuilder.Build()
        {
            if (!orderLinesSet)
                throw new Exception();
            return order;
        }

        IOrderBBuilder IOrderABuilder.WithCustomerId(long customerId)
        {
            this.order = new Order(customerId);
            return this;
        }

        IOrderBuilder IOrderBBuilder.WithOrderLine(List<OrderLine> orderLines)
        {
            this.order.SetOrderLines(orderLines);
            orderLinesSet = true;
            return this;
        }

    }
    public interface IOrderABuilder
    {
        IOrderBBuilder WithCustomerId(long customerId);
    }
    public interface IOrderBBuilder
    {
        IOrderBuilder WithOrderLine(List<OrderLine> orderLines);
    }
    public interface IOrderBuilder
    {
        Order Build();
    }
}
