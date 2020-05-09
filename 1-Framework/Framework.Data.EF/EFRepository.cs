using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using Framework.Domain;
using Framework.Domain.Aggregate;
using Framework.Domain.Events;
using Framework.Domain.Repository;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Framework.Data.EF
{
    public class EFRepository<T> : IRepository<T> where T : class, IAggregateRoot
    {
        private readonly DbContext _dbContext;
        private IEventDispatcher eventDispatcher;
        private MethodInfo dispatchMethod;
        public EFRepository(DbContext dbContext, IEventDispatcher eventDispatcher)
        {
            this._dbContext = dbContext;
            this.eventDispatcher = eventDispatcher;
            dispatchMethod = eventDispatcher.GetType().GetMethod("Dispatch");
        }

        public bool Add(T entity)
        {
            this._dbContext.Add(entity);
            DispatchEvents(entity);
            return true;
        }
        readonly JsonSerializerSettings setting = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All,
            TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        };
        private void DispatchEvents(T entity)
        {
            var events = entity.GetUnPublishedEvents().ToList();
            entity.ClearEvents();
            events.ForEach(@event =>
            {
                @event.AggregateRootId = entity.Id.ToString();
                @event.CreateDate = DateTime.Now;
                @event.UserName = Thread.CurrentPrincipal.Identity.Name;
                dispatchMethod.MakeGenericMethod(@event.GetType()).Invoke(eventDispatcher, new object[] { @event });
            });
            foreach (var e in events)
            {
                //session.Save(new Event(e.GetType().FullName, e.UserName, e.AggregateRootId,
                //    JsonConvert.SerializeObject(e, setting), e.GetType().FullName));
            }
        }

        public bool Add(IEnumerable<T> items)
        {
            _dbContext.AddRange(items);
            return true;
        }

        public bool Update(T entity)
        {
            _dbContext.Update(entity);
            DispatchEvents(entity);

            return true;
        }

        public bool Delete(T entity)
        {
            _dbContext.Remove(entity);
            DispatchEvents(entity);
            return true;
        }

        public bool Delete(IEnumerable<T> entities)
        {
            _dbContext.RemoveRange(entities);
            return true;
        }

        public T FindBy(object id)
        {
            return _dbContext.Find<T>(id);
        }

        public IQueryable<T> FilterBy(ISpecification<T> expression)
        {
            return FilterBy(expression.Expression);
        }

        public IQueryable<T> All()
        {
            return _dbContext.Query<T>();
        }

        public T FindBy(Expression<Func<T, bool>> expression)
        {
            return FilterBy(expression).FirstOrDefault();
        }

        public IQueryable<T> FilterBy(Expression<Func<T, bool>> expression)
        {
            return All().Where(expression);
        }
    }
}