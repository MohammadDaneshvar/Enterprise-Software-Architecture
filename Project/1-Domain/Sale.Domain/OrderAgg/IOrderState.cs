namespace Sale.Domain.OrderAgg
{
    public interface IOrderState
    {
        void New();
        void Pay();
        void Cancel();
        void Approve();
        void Reject();
        void Shippment();
    }
}