namespace Sale.Domain.OrderAgg
{
    public class ApprovedOrderState : OrderState
    {
        public ApprovedOrderState(Order order)
            :base(order)
        {
        }
    }
}