using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Sale.Domain.Query.Model;

namespace Sale.Domain.Query.Finders
{
    public interface ISaleFinder
    {
        Order FindById(long id);
        List<Order> GetAll(Expression<Func<Order, bool>> predicate);
    }

    public class SaleFinder : ISaleFinder
    {
        private readonly SaleEntities _entities;

        public SaleFinder(SaleEntities entities)
        {
            _entities = entities;
            //_entities.Configuration.
        }
        public Order FindById(long id)
        {
            return _entities.Orders.Find(id);
        }

        public List<Order> GetAll(Expression<Func<Order, bool>> predicate)
        {
            return _entities.Orders.Where(predicate).ToList();
        }
    }
}