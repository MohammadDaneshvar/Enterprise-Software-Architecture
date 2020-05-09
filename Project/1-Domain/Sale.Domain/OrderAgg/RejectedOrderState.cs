namespace Sale.Domain.OrderAgg
{
    public class RejectedOrderState : OrderState
    {
        public RejectedOrderState(Order order)
            :base(order)
        {
        }
    }
}