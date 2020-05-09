namespace Sale.Domain.OrderAgg
{
    public class CanceledOrderState : OrderState
    {
        public CanceledOrderState(Order order)
            :base(order)
        {
        }
    }
}