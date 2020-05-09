using System;
using System.Linq.Expressions;
using Framework.Domain;

namespace Sale.Domain.OrderAgg
{
    public class MoreThan10Item : Specification<Order>
    {
        public MoreThan10Item()
            :base(entity => entity.OrderLines.Count < 10)
        {
            
        }
    }
    public class MoreThanXItem : Specification<Order>
    {
        public MoreThanXItem(int min)
            :base(o => o.OrderLines.Count > min)
        {
        }
    }
}