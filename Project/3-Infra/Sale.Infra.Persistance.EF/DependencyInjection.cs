using Infra.Persistance.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimpleInjector;
using System.Collections.Generic;
using System.Security.Claims;

namespace Infra.Persistance.EF
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration, bool isTestEnvironment)
        {
    //        var provider =(Container) services.BuildServiceProvider();
    //        var reg = Lifestyle.Scoped.CreateRegistration(() =>
    //        {
    //            var optionsBuilder =
    //                new DbContextOptionsBuilder<FRIDbContext>().UseSqlServer("FRIQuery");
    //            return new FRIDbContext(optionsBuilder.Options);
    //        },
    //provider);


            //services.AddDbContext<FRIDbContext>(options =>
            //    options.UseSqlServer(
            //        configuration.GetConnectionString("FRIQuery"),
            //        b => b.MigrationsAssembly(typeof(FRIDbContext).Assembly.FullName)));

            //services.AddScoped<IFRIDbContext>(provider => provider.GetService<FRIDbContext>());

            //services.AddDefaultIdentity<ApplicationUser>()
            //    .AddEntityFrameworkStores<FRIDbContext>();

            if (isTestEnvironment)
            {
                //services.AddIdentityServer()
                //    .AddApiAuthorization<ApplicationUser, FRIDbContext>(options =>
                //    {
                //        options.Clients.Add(new Client
                //        {
                //            ClientId = "CleanArchitecture.IntegrationTests",
                //            AllowedGrantTypes = { GrantType.ResourceOwnerPassword },
                //            ClientSecrets = { new Secret("secret".Sha256()) },
                //            AllowedScopes = { "CleanArchitecture.WebUIAPI", "openid", "profile" }
                //        });
                //    }).AddTestUsers(new List<TestUser>
                //    {
                //        new TestUser
                //        {
                //            SubjectId = "f26da293-02fb-4c90-be75-e4aa51e0bb17",
                //            Username = "jason@clean-architecture",
                //            Password = "CleanArchitecture!",
                //            Claims = new List<Claim>
                //            {
                //                new Claim(JwtClaimTypes.Email, "jason@clean-architecture")
                //            }
                //        }
                //    });
            }
            else
            {
                //services.AddIdentityServer()
                //    .AddApiAuthorization<ApplicationUser, FRIDbContext>();

                //services.AddTransient<IDateTime, DateTimeService>();
                //services.AddTransient<IIdentityService, IdentityService>();
                //services.AddTransient<ICsvFileBuilder, CsvFileBuilder>();
            }

            //services.AddAuthentication()
            //    .AddIdentityServerJwt();

            return services;
        }
    }
}
