namespace Sale.Domain.OrderAgg
{
    public class RecievedOrderState : OrderState
    {
        public RecievedOrderState(Order order)
            :base(order)
        {
        }
    }
}