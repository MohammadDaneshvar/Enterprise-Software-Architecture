namespace Sale.Domain.OrderAgg
{
    public class PayedOrderState : OrderState
    {
        public PayedOrderState(Order order)
            :base(order)
        {
        }

        public override void Approve()
        {
            Order.SetState(new ApprovedOrderState(Order));
        }

        public override void Cancel()
        {
            Order.SetState(new CanceledOrderState(Order));
        }
    }
}