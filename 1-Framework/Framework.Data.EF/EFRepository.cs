using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Framework.Domain;
using Framework.Domain.Aggregate;
using Framework.Domain.Events;
using Framework.Domain.Repository;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Framework.Data.EF
{

    public interface IX<T> where T : class
    {
      
    }
    public class X<T>:IX<T> where T : class
    {

    }

    public class EFRepository<T> : IRepository<T> where T : class /*,IAggregateRoot*/
    {
        private readonly IDbContext _dbContext;
        private IEventDispatcher eventDispatcher;
        private MethodInfo dispatchMethod;
        public EFRepository(IDbContext dbContext, IEventDispatcher eventDispatcher)
        {
            this._dbContext = dbContext;
            this.eventDispatcher = eventDispatcher;
            dispatchMethod = eventDispatcher.GetType().GetMethod("Dispatch");
        }

        public async Task AddAsync(T entity)
        {
            await this._dbContext.AddAsync(entity);
       //     DispatchEvents(entity);
        }
        readonly JsonSerializerSettings setting = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All,
            TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        };
        //private void DispatchEvents(T entity)
        //{
        //    var events = entity.GetUnPublishedEvents().ToList();
        //    entity.ClearEvents();
        //    events.ForEach(@event =>
        //    {
        //        @event.AggregateRootId = entity.Id.ToString();
        //        @event.CreateDate = DateTime.Now;
        //        @event.UserName = Thread.CurrentPrincipal.Identity.Name;
        //        dispatchMethod.MakeGenericMethod(@event.GetType()).Invoke(eventDispatcher, new object[] { @event });
        //    });
        //    foreach (var e in events)
        //    {
        //        //session.Save(new Event(e.GetType().FullName, e.UserName, e.AggregateRootId,
        //        //    JsonConvert.SerializeObject(e, setting), e.GetType().FullName));
        //    }
        //}

        public async Task AddRangeAsync(IEnumerable<T> items)
        {
            await _dbContext.AddRangeAsync(items);
        }

        public bool Update(T entity)
        {
            _dbContext.Update(entity);
          //  DispatchEvents(entity);

            return true;
        }

        public void Remove(T entity)
        {
            _dbContext.Remove(entity);
        //    DispatchEvents(entity);
        }

        public  void RemoveRange(IEnumerable<T> entities)
        {
            _dbContext.RemoveRange(entities);
        }

        public async Task<TEntity> FindAsync<TEntity, TKey>(TKey id) where TEntity : class
        {
            return await _dbContext.FindAsync<TEntity,TKey>(id);
        }

        public IQueryable<T> FilterBy(ISpecification<T> expression)
        {
            return FilterBy(expression.Expression);
        }

        public IQueryable<T> Query()
        {
            return _dbContext.Query<T>();
        }

        public T FindByAsync(Expression<Func<T, bool>> expression)
        {
            return FilterBy(expression).FirstOrDefault();
        }

        public IQueryable<T> FilterBy(Expression<Func<T, bool>> expression)
        {
            return Query().Where(expression);
        }
    }
}