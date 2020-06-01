using Framework.Application;
using Framework.Data;
using Framework.Data.EF;
using Framework.Domain.Repository;
using Infra.Persistance.EF;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.ServiceModel.Channels;
//using Domain.Query.Finders;

namespace AppService.Config
{
    public class AppServiceConfigurator
    {
        public class EventHandlerFactory : IEventHandlerFactory
        {
            private readonly Container _container;

            public EventHandlerFactory(Container container)
            {
                _container = container;
            }
            public List<IEventHandler<T>> CreateHandler<T>()
            {
                return _container.GetAllInstances<IEventHandler<T>>().ToList();
            }
        }

        [Obsolete]
        public static void WireUp(Container container,string    connectionString)
        {
            container.Register<IUnitOfWork, EFUnitOfWork>();
            container.Register<IDbContext>(()=> new FRIDbContext(connectionString));
            
            
            container.RegisterDecorator(typeof(ICommandHandler<>), typeof(CacheDecoratorCommandHandler<>));
            container.RegisterDecorator(typeof(ICommandHandler<>), typeof(ValidatorCommandHandler<>));
            //container.Register(typeof(ICommandValidator<>), typeof(CommandValidator<>));
            
            container.Register<ICacheProvider, InMemoryCacheProvider>(Lifestyle.Singleton);
            container.Register<IKeyGenerator, KeyGenerator>();
            container.Register(typeof(IRepository<>), typeof(EFRepository<>));
            //container.RegisterCollection(typeof(ICommandHandler<>), new List<Assembly> { typeof(SaleAppService).Assembly });
            //container.RegisterCollection(typeof(IEventHandler<>), new List<Assembly> { typeof(SaleAppService).Assembly });
            //container.Register<IServiceProvider>(() => container, Lifestyle.Singleton);
            ////container.Register<ISession>(() =>
            ////    SessionFactoryBuilder.Create("Core", typeof(OrderMapper).Assembly).OpenSession()
            //, Lifestyle.Scoped);
            ////    container.Register<ISaleFinder, SaleFinder>();
            //container.Register<IValidator, Validator>();
            //container.Register<IEventHandlerFactory, EventHandlerFactory>(Lifestyle.Singleton);
            ////  container.Register<SaleEntities>(); 
        }
    }
}
