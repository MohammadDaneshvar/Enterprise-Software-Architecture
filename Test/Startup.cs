using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SimpleInjector;
using SimpleInjector.Integration.ServiceCollection;
namespace App.Service.AspDotNetDistributor
{
    public class Startup
    {
        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var container = new Container();
            services.AddSimpleInjector(container);
            services
            // Save yourself pain and agony and always use "validateScopes: true"
            .BuildServiceProvider(validateScopes: true)
            // Ensures framework components can be retrieved
            .UseSimpleInjector(container);

            container.Verify();

            services.AddSingleton(typeof(Storage<>));
            //services.
            //    AddMvc(o => o.Conventions.Add(new GenericControllerRouteConvention())).
                //ConfigureApplicationPartManager(m => m.FeatureProviders.Add(new GenericTypeControllerFeatureProvider()));
                //ConfigureApplicationPartManager(m => m.FeatureProviders.Add(new RemoteControllerFeatureProvider()));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseMvc();
        }
    }
}
