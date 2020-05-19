using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Framework.Domain.Repository
{
    public interface IRepository<T> where T : class
    {
        Task AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> items);
        bool Update(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
        Task<TEntity> FindAsync<TEntity, TKey>(TKey id) where TEntity : class;
        IQueryable<T> FilterBy(Expression<Func<T, bool>> expression);
        IQueryable<T> FilterBy(ISpecification<T> expression);
        IQueryable<T> Query();
    }
}