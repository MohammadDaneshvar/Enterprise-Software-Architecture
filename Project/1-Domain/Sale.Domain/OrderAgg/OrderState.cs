using System;

namespace Sale.Domain.OrderAgg
{
    public abstract class OrderState : IOrderState
    {
        protected Order Order;

        protected OrderState(Order order)
        {
            this.Order = order;
        }

        public virtual void New()
        {
            throw new NotImplementedException();
        }

        public virtual void Pay()
        {
            throw new System.NotImplementedException();
        }

        public virtual void Cancel()
        {
            throw new System.NotImplementedException();
        }

        public virtual void Approve()
        {
            throw new System.NotImplementedException();
        }

        public virtual void Reject()
        {
            throw new System.NotImplementedException();
        }

        public virtual void Shippment()
        {
            throw new System.NotImplementedException();
        }
    }
}