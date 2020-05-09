using System;
using System.Linq;
using AppService;
using Framework.Application;
using Nancy;
using Nancy.ModelBinding;
using Service.Distributor;
using SimpleInjector.Lifestyles;

namespace App.Distributor
{
    public class WelcomeModule : NancyModule
    {
        public WelcomeModule()
        {
            After.AddItemToEndOfPipeline((context) =>
                context.Response
                    .WithHeader("Access-Control-Allow-Origin", "*")
                    .WithHeader("Access-Control-Allow-Methods", "POST,GET")
                    .WithHeader("Access-Control-Allow-Headers", "Accept, Origin, Content-type")
            );

            var s = Program.container.GetTypesToRegister(typeof(ICommandHandler<>),
                new[] { typeof(SaleAppService).Assembly });
            var commands =
                s.SelectMany(type => type.GetInterfaces())
                    .Where(type => type.IsGenericType)
                    .Select(type => type.GetGenericArguments().First());
            foreach (var command in commands)
            {
                var address = $"/{command.Name}";
                var makeHandler = this.GetType().GetMethod("MakeHandler", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance)
                    .MakeGenericMethod(command);
                Get(address, x => (Func<object, object>)makeHandler.Invoke(this, new object[0]));
                Post(address, x => (Func<object, object>)makeHandler.Invoke(this, new object[0]));

            }
        }

        public Func<object, object> MakeHandler<T>()
        {
            return _ =>
            {
                var bus = Program.container.GetInstance<ICommandBus>();
                var command = this.Bind<T>();
                //bindTo.MakeGenericMethod(commandType).Invoke(null, new object[] {this, command});
                using (AsyncScopedLifestyle.BeginScope(Program.container))
                    bus.Dispatch(command);
                if (command is IHaveResult) return ((IHaveResult)command).Result;
                return "ok";
            };
        }
    }

}