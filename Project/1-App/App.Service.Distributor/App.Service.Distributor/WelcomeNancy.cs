using System;
using System.Linq;
using System.Threading.Tasks;
using AppService;
using AppService.Contracts;
using Framework.Application;
using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.AspNetCore.SignalR;
using Nancy;
using Nancy.Json;
using Nancy.ModelBinding;
using Nancy.Security;
using Service.Distributor;
using SimpleInjector.Lifestyles;

namespace App.Distributor
{
    public class WelcomeModule : NancyModule
    {
        private readonly IServiceProvider serviceProvider;

        public WelcomeModule()
        {
            this.RequiresAuthentication();
            


            After.AddItemToEndOfPipeline((context) =>
                context.Response
                    .WithHeader("Access-Control-Allow-Origin", "*")
                    .WithHeader("Access-Control-Allow-Methods", "POST,GET")
                    .WithHeader("Access-Control-Allow-Headers", "Accept, Origin, Content-type")
            );
            //var s=this.Context.CurrentUser.Identity.IsAuthenticated;
            var commandHandlers = Program.container.GetTypesToRegister(typeof(ICommandHandler<>),
                new[] { typeof(LoanAppService).Assembly });
            var commands =
                commandHandlers.SelectMany(type => type.GetInterfaces())
                    .Where(type => type.IsGenericType)
                    .Select(type => type.GetGenericArguments().First());
            foreach (var command in commands)
            {

                var serviceName = command.Namespace.Split('.').LastOrDefault();
                var address = $"/{serviceName}/{command.Name}";
                var makeHandler = this.GetType().GetMethod("MakeHandler", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance)
                    .MakeGenericMethod(command);
                //    //     Get["/"] = parameters => "Hello World!";

                //    Get(address+"t", async (args, ct) =>
                //    {
                //        var result = await MakeHandler1(this);
                //        return result;
                //    });

                //    Post(address + "t", async (args, ct) =>
                //    {

                //    var result = await MakeHandler(this);
                //    return result;
                //});



                Get(address, async x => await (Task<object>)makeHandler.Invoke(this, new object[0]));
                Post(address, async x => await (Task<object>)makeHandler.Invoke(this, new object[0]));


                //Get(address,  x => (Func<object, object>)makeHandler.Invoke(this, new object[0]));
                //Post(address,  x => (Func<object, object>)makeHandler.Invoke(this, new object[0]));
            }

            this.serviceProvider = serviceProvider;
        }


        public async Task<object> MakeHandler<T>()
        {
            try
            {

                var isAuthenticated = false;
                Before.AddItemToEndOfPipeline(ctx =>
                {
                    isAuthenticated = ctx.CurrentUser.IsAuthenticated();
                    return null;
                });
                var bus = Program.container.GetInstance<ICommandBus>();
                var command = this.Bind<T>();
                //bindTo.MakeGenericMethod(commandType).Invoke(null, new object[] {this, command});
                using (AsyncScopedLifestyle.BeginScope(Program.container))
                    await bus.DispatchAsync(command);
                if (command is IHaveResult) return ((IHaveResult)command).Result;
            }
            catch (Exception e)
            {
                var s = e;
            }

            return  new Response
            {
                StatusCode = HttpStatusCode.OK,
            };
        }


        //public Func<object, object> MakeHandler<T>()
        //{
        //    return _ =>
        //    {
        //        var bus = Program.container.GetInstance<ICommandBus>();
        //        var command = this.Bind<T>();
        //        //bindTo.MakeGenericMethod(commandType).Invoke(null, new object[] {this, command});
        //        using (AsyncScopedLifestyle.BeginScope(Program.container))
        //           await  bus.DispatchAsync(command);
        //        if (command is IHaveResult) return ((IHaveResult)command).Result;
        //        return "ok";
        //    };
        //}
    }

}