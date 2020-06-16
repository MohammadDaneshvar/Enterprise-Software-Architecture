using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using AppService;
using AppService.Config;
using AppService.Contracts;
using FluentAssertions.Common;
using Framework.Application;
using Framework.Application.Config;
using Infra.Persistance.EF;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Nancy.Owin;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using AppService.Query;
using App.Service.Distributor.Common;
using Microsoft.Extensions.Hosting;

namespace Service.Distributor
{
    public class Startup
    {
        private IConfiguration _configuration { get; }

        private IWebHostEnvironment _environment { get; }
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            _configuration = configuration;
            _environment = environment;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddInfrastructure(_configuration,true);
            services.AddServiceQuery(_configuration, true);
            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.Authority = "http://localhost:5000";
                    options.RequireHttpsMetadata = false;

                    options.Audience = "api1";
                });
            services.AddAuthorization();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            });
            services.AddRouting();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseOwin(x => { x.UseNancy(); });
            app.UseAuthentication();
            app.UseAuthorization();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseCustomExceptionHandler();
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();
            app.UseHealthChecks("/health");
            app.UseHttpsRedirection();
            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }

    class Program
    {
        public static Container container;
        static void Main(string[] args)
        {
            string conStr = ConfigurationManager.ConnectionStrings["FRI"].ToString();
            container = new Container();

            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();
            FrameworkConfigurator.WireUp(container, false, typeof(LoanAppService).Assembly, typeof(CreateLoanCommand).Assembly);
            AppServiceConfigurator.WireUp(container);
            //var mydb = new FRIDbContext(conStr);
            //mydb.Database.Migrate();


            var host = new WebHostBuilder()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseKestrel()
                .UseStartup<Startup>()
                .UseUrls("http://localhost:5005/")
                .Build();


            host.Run();

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
