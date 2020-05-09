using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Framework.Domain.Aggregate;
using Framework.Validation;

namespace Sale.Domain.OrderAgg
{
    public class Order : AggregateRoot<long>
    {
        protected Order()
        {

        }
        //public Order(long customerId, List<OrderLine> orderLines)
        //{
        //    orderLines.MustBeNotEmpty("orderline can't be empty");
        //    this.customerId = customerId;
        //    orderLines.ForEach(this.orderLines.Add);
        //}
        public Order(long customerId)
        {
            this.customerId = customerId;
            SetState(new NewOrderState(this));
            Publish(new OrderCreated(this));
        }
        public virtual void SetOrderLines(List<OrderLine> orderLines)
        {
            orderLines.MustBeNotEmpty("orderline can't be empty");
            orderLines.ForEach(this.orderLines.Add);
        }
        private long id;
        public override long Id
        {
            get { return id; }
        }

        private long customerId;
        public virtual long CustomerId
        {
            get { return customerId; }
        }

        private OrderState orderState;
        public virtual OrderState OrderState
        {
            get { return orderState; }
        }
        private byte[] rowVersion;

        protected internal virtual void SetState(OrderState newState)
        {
            orderState = newState;
        }

        public virtual void Pay()
        {
            orderState.Pay();
        }
        public virtual void Cancel()
        {
            orderState.Cancel();
        }
        public virtual void Approve()
        {
            orderState.Approve();
        }
        public virtual byte[] RowVersion
        {
            get { return rowVersion; }
        }
        private IList<OrderLine> orderLines = new List<OrderLine>();
        public virtual IReadOnlyCollection<OrderLine> OrderLines
        {
            get { return new ReadOnlyCollection<OrderLine>(orderLines); }
        }
        private IList<CouponCode> coupons = new List<CouponCode>();
        public virtual IReadOnlyCollection<CouponCode> Coupons
        {
            get { return new ReadOnlyCollection<CouponCode>(coupons); }
        }
        public virtual void AddCoupon(CouponCode coupon)
        {
            //rule - only one cupon per target type
            if (coupons.Any(c => c.Target == coupon.Target)) return;
            coupons.Add(coupon);
        }
        public virtual void ChangeCustomer(int i)
        {
            customerId = i;
        }
    }

    public class OrderCreated : IDomainEvent
    {
        public OrderCreated(){}
        public OrderCreated(Order order)
        {
            Order = order;
        }
        public string AggregateRootId { get; set; }
        public DateTime CreateDate { get; set; }
        public string UserName { get; set; }
        public string ApplicationSource { get; set; }
        public Order Order { get; set; }
    }

    public class OrderApprovedEvent : IDomainEvent
    {
        public string AggregateRootId { get; set; }
        public DateTime CreateDate { get; set; }
        public string UserName { get; set; }
        public string ApplicationSource { get; set; }
        public long OrderId { get; set; }
    }
}