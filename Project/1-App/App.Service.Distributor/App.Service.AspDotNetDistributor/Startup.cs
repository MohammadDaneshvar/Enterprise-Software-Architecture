using System;
using System.Threading.Tasks;
using AppService;
using AppService.Config;
using AppService.Contracts;
using Framework.Application.Config;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using Infra.Persistance.EF;
using AppService.Query;
using SimpleInjector.Integration.ServiceCollection;
using SimpleInjector.Integration.Web.Mvc;
using System.Web.Mvc;
using Framework.Application;
using Framework.Data;
using Microsoft.EntityFrameworkCore;

namespace DynamicAndGenericControllersSample
{
    public class Startup
    {
        public static Container _container;
        public Startup(IConfiguration configuration)
        {
            _container = new SimpleInjector.Container();
            _configuration = configuration;
            // Set to false. This will be the default in v5.x and going forward.
            _container.Options.ResolveUnregisteredConcreteTypes = false;
        }

        private IConfiguration _configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddInfrastructure(_configuration, true);
            services.AddServiceQuery(_configuration, true);
            services.AddSimpleInjector(_container
         //, options =>
         //   {

         //       options.AddAspNetCore()

         //// Ensure activation of a specific framework type to be created by
         //// Simple Injector instead of the built-in configuration system.
         //// All calls are optional. You can enable what you need. For instance,
         //// ViewComponents, PageModels, and TagHelpers are not needed when you
         //// build a Web API.
         //.AddControllerActivation()
         //.AddViewComponentActivation()
         //.AddPageModelActivation()
         //.AddTagHelperActivation();

         //       // Optionally, allow application components to depend on the non-generic
         //       // ILogger (Microsoft.Extensions.Logging) or IStringLocalizer
         //       // (Microsoft.Extensions.Localization) abstractions.
         //       options.AddLogging();
         //       options.AddLocalization();
         //   }
         );
            InitializeContainer();
            services.
                AddMvc(o =>
                {
                    o.Conventions.Add(new GenericControllerRouteConvention());
                    o.EnableEndpointRouting = false;
                }
                    ).
                //ConfigureApplicationPartManager(m => m.FeatureProviders.Add(new GenericTypeControllerFeatureProvider()));
                ConfigureApplicationPartManager(m => m.FeatureProviders.Add(new RemoteControllerFeatureProvider(_container)));


            services.AddInfrastructure(_configuration, true);
            services.AddServiceQuery(_configuration, true);

        }

        private void InitializeContainer()
        {
            // Add application services. For instance:
            //   _container.Register<ICommandBus, CommandBus>(Lifestyle.Singleton);
            FrameworkConfigurator.WireUp(_container, false, typeof(LoanAppService).Assembly, typeof(CreateLoanCommand).Assembly);
            AppServiceConfigurator.WireUp(_container);


            // options.UseSqlServer(
            ////        configuration.GetConnectionString("FRIQuery"),
            ////        b => b.MigrationsAssembly(typeof(FRIDbContext).Assembly.FullName)));

            var reg = Lifestyle.Scoped.CreateRegistration(() =>
            {
                return new FRIDbContext(new DbContextOptionsBuilder<FRIDbContext>().UseSqlServer(_configuration.GetConnectionString("FRIQuery")).Options);
            },  _container);

            _container.AddRegistration<IDbContext>(reg);
            //_container.Register<IDbContext>(() =>
            //{
            //    return new FRIDbContext(new DbContextOptionsBuilder<FRIDbContext>().UseSqlServer(_configuration.GetConnectionString("FRIQuery")).Options);
            //});
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseSimpleInjector(_container);


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseSimpleInjector(_serviceProvider);

            _container.Verify();
            app.UseMvc();
        }
    }
}
