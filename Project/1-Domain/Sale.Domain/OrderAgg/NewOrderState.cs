namespace Sale.Domain.OrderAgg
{
    public class NewOrderState : OrderState
    {

        public NewOrderState(Order order) : base(order)
        {
        }
        public override void Pay()
        {
            Order.SetState(new PayedOrderState(Order));
        }

        public override void Cancel()
        {
            Order.SetState(new CanceledOrderState(Order));
        }
    }
}