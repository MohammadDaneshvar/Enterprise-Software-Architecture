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
using Microsoft.OpenApi.Models;
using Framework.Application.Common.Attributes;
using Nancy.Json;
using Newtonsoft.Json.Serialization;

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
            
            services.AddControllersWithViews();
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


            services.AddSwaggerGen(setupAction =>
            {
                setupAction.SwaggerDoc(
                   name: "LibraryOpenAPISpecification",
                   info: new Microsoft.OpenApi.Models.OpenApiInfo()
                   {
                       Title = "Library API",
                       Version = "1",
                       Description = "Through this API you can access authors and their books.",
                       Contact = new Microsoft.OpenApi.Models.OpenApiContact()
                       {
                           Email = "name@site.com",
                           Name = "DNT",
                           Url = new Uri("http://www.dotnettips.info")
                       },
                       License = new Microsoft.OpenApi.Models.OpenApiLicense()
                       {
                           Name = "MIT License",
                           Url = new Uri("https://opensource.org/licenses/MIT")
                       }
                   });
                setupAction.SchemaFilter<SwaggerIgnoreFilter>();
            });
            services.AddSwaggerGenNewtonsoftSupport();




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
            }, _container);

            _container.AddRegistration<IDbContext>(reg);
            //_container.Register<IDbContext>(() =>
            //{
            //    return new FRIDbContext(new DbContextOptionsBuilder<FRIDbContext>().UseSqlServer(_configuration.GetConnectionString("FRIQuery")).Options);
            //});
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


            app.UseSimpleInjector(_container);


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //app.UseSimpleInjector(_serviceProvider);

            _container.Verify();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/LibraryOpenAPISpecification/swagger.json", "My API V1");
            });

        }
    }
}
