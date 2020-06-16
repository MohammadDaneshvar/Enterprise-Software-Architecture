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
namespace DynamicAndGenericControllersSample
{
    public class Startup
    {
        private  Container _serviceProvider;

        public Startup(IConfiguration configuration)
        {

            //container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();
            _configuration = configuration;




        }

        private IConfiguration _configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            _serviceProvider = new Container();
            services.AddSimpleInjector(_serviceProvider);
            services.BuildServiceProvider(validateScopes: true)
                .UseSimpleInjector(_serviceProvider);
            
            services.AddSingleton(typeof(Storage<>));
            services.
                AddMvc(o =>
                {
                    o.Conventions.Add(new GenericControllerRouteConvention());
                    o.EnableEndpointRouting = false;
                }
                    ).
                //ConfigureApplicationPartManager(m => m.FeatureProviders.Add(new GenericTypeControllerFeatureProvider()));
                ConfigureApplicationPartManager(m => m.FeatureProviders.Add(new RemoteControllerFeatureProvider(_serviceProvider)));


            services.AddInfrastructure(_configuration, true);
            services.AddServiceQuery(_configuration, true);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseSimpleInjector(_serviceProvider);
            FrameworkConfigurator.WireUp(_serviceProvider, false, typeof(LoanAppService).Assembly, typeof(CreateLoanCommand).Assembly);
            AppServiceConfigurator.WireUp(_serviceProvider);
            _serviceProvider.Verify();
            app.UseMvc();
        }
    }
}
