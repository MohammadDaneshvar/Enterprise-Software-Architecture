using Framework.Domain.Aggregate;

namespace Sale.Domain.OrderAgg
{
    public class OrderLine : EntityBase<OrderLine, long>
    {
        protected OrderLine()
        {
            
        }
        public OrderLine(long productId, int quantity)
        {
            this.productId = productId;
            this.quantity = quantity;
        }
        private long id;
        public override long Id
        {
            get { return id; }
        }

        private long productId;
        public virtual long ProductId
        {
            get { return productId; }
        }

        private int quantity;
        public virtual int Quantity
        {
            get { return quantity; }
        }
    }
}