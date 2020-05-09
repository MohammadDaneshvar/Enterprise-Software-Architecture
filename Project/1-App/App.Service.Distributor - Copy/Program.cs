using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using AppService;
using Framework.Application;
using Framework.Application.Config;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Hosting;
using Newtonsoft.Json;
using Owin;
using SimpleInjector;
using SimpleInjector.Lifestyles;

namespace Service.Distributor
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseNancy();
            app.UseCors(CorsOptions.AllowAll);
        }
    }
    class Program
    {
        public static Container container;
        static void Main(string[] args)
        {


        }

        //private static void HostAllService()
        //{
        //    var s = container.GetTypesToRegister(typeof(ICommandHandler<>), new[] { typeof(SaleAppService).Assembly });
        //    var commands =
        //        s.SelectMany(type => type.GetInterfaces())
        //            .Where(type => type.IsGenericType)
        //            .Select(type => type.GetGenericArguments().First());
        //    foreach (var command in commands)
        //    {
        //        var address = new Uri(string.Format("http://{0}:{1}/{2}", "localhost", "228", command.Name));
        //        //  Console.WriteLine("{0} host created", address);
        //        Type serviceType = typeof(CommandHandlerService<>).MakeGenericType(command);
        //        Type serviceContractType = typeof(ICommandHandlerService<>).MakeGenericType(command);
        //        var basicHttpBinding = new BasicHttpBinding { MaxReceivedMessageSize = 2147483647 };
        //        var host = new ServiceHost(serviceType, address);
        //        host.AddServiceEndpoint(serviceContractType, basicHttpBinding, address);
        //        host.Description.Behaviors.Find<ServiceDebugBehavior>().IncludeExceptionDetailInFaults = true;
        //        //host.Description.Behaviors.Add(
        //        //    new ServiceDebugBehavior() {IncludeExceptionDetailInFaults = true});
        //        var smb = new ServiceMetadataBehavior
        //        {
        //            HttpGetEnabled = true,
        //            MetadataExporter = { PolicyVersion = PolicyVersion.Policy15 },
        //        };
        //        host.Description.Behaviors.Add(smb);
        //        host.Open();
        //        Console.WriteLine("{0}", address);
        //    }

        //}
        //public class CommandHandlerService<TCommand> : ICommandHandlerService<TCommand>
        //{
        //    public string Send(TCommand command)
        //    {
        //        var bus = container.GetInstance<ICommandBus>();
        //        using (AsyncScopedLifestyle.BeginScope(container))
        //        {
        //            bus.Dispatch(command);
        //        }
        //        var r = command as IHaveResult;
        //        return r != null ? JsonConvert.SerializeObject(r.Result, new JsonSerializerSettings()
        //        {
        //            TypeNameHandling = TypeNameHandling.All,
        //            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        //        }) : null;
        //    }
        //}
    }

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
}
