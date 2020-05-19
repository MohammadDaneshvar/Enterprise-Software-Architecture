using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Data
{
    public interface IDbContext
    {
        Task BeginAsync();
        Task CommitAsync();
        Task<int> SaveChangesAsync();
        Task RollbackAsync();
        Task<TEntity> FindAsync<TEntity, TKey>(TKey id) where TEntity : class;
        void RemoveRange<T>(IEnumerable<T> entities) where T : class;
        void Remove<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        Task AddRangeAsync<T>(IEnumerable<T> items) where T : class;
        Task AddAsync<T>(T entity) where T : class;
        IQueryable<T> Query<T>() where T : class;
    }
}
