using Framework.Data;
using Framework.Data.EF;
using Framework.Domain.Events;
using Framework.Domain.Repository;
using Framework.Messaging.NSB;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Framework.Application.Config
{
    public class FrameworkConfigurator
    {
        public static void WireUp(Container container, bool runAsClient, Assembly application)
        {
            container.Register<IServiceProvider>(() => container, Lifestyle.Singleton);
            container.Register<IEventDispatcher, EventDispatcher>(Lifestyle.Singleton);
            container.Register<ICommandHandlerFactory, CommandHandlerFactory>(Lifestyle.Singleton);
            container.Register<ICommandBus, CommandBus>(Lifestyle.Singleton);
            container.RegisterDecorator(typeof(ICommandHandler<>), typeof(TransactionalCommandHandler<>));
            //container.RegisterDecorator(typeof(ICommandHandler<>), typeof(AccessValidatorCommandHandler<>), t => typeof(IRestrictedCommand).IsAssignableFrom(t.ServiceType.GetGenericArguments().First()));

            //container.RegisterDecorator(typeof(ICommandHandler<>), typeof(ValidatorCommandHandler<>));
            //container.Register<ISession>(() =>
            //                SessionFactoryBuilder.Create("Core", typeof(OrderMapper).Assembly).OpenSession()
            //            , Lifestyle.Scoped);
            //container.Register<IValidator, Validator>();
            container.Register<IEventHandlerFactory, EventHandlerFactory>(Lifestyle.Singleton);
            container.RegisterCollection(typeof(IEventHandler<>), new List<Assembly> { application });
        }
    }
}
