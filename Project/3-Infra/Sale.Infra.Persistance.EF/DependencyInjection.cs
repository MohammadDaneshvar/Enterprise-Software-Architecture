//using Infra.Persistance.EF;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;
//using System.Collections.Generic;
//using System.Security.Claims;

//namespace CleanArchitecture.Infrastructure
//{
//    public static class DependencyInjection
//    {
//        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
//        {
//            services.AddDbContext<FRIDbContext>(options =>
//                options.UseSqlServer(
//                    configuration.GetConnectionString("DefaultConnection"),
//                    b => b.MigrationsAssembly(typeof(FRIDbContext).Assembly.FullName)));

//            //services.AddScoped<IFRIDbContext>(provider => provider.GetService<FRIDbContext>());

//            //services.AddDefaultIdentity<ApplicationUser>()
//            //    .AddEntityFrameworkStores<FRIDbContext>();

//            if (environment.IsEnvironment("Test"))
//            {
//                //services.AddIdentityServer()
//                //    .AddApiAuthorization<ApplicationUser, FRIDbContext>(options =>
//                //    {
//                //        options.Clients.Add(new Client
//                //        {
//                //            ClientId = "CleanArchitecture.IntegrationTests",
//                //            AllowedGrantTypes = { GrantType.ResourceOwnerPassword },
//                //            ClientSecrets = { new Secret("secret".Sha256()) },
//                //            AllowedScopes = { "CleanArchitecture.WebUIAPI", "openid", "profile" }
//                //        });
//                //    }).AddTestUsers(new List<TestUser>
//                //    {
//                //        new TestUser
//                //        {
//                //            SubjectId = "f26da293-02fb-4c90-be75-e4aa51e0bb17",
//                //            Username = "jason@clean-architecture",
//                //            Password = "CleanArchitecture!",
//                //            Claims = new List<Claim>
//                //            {
//                //                new Claim(JwtClaimTypes.Email, "jason@clean-architecture")
//                //            }
//                //        }
//                //    });
//            }
//            else
//            {
//                //services.AddIdentityServer()
//                //    .AddApiAuthorization<ApplicationUser, FRIDbContext>();

//                //services.AddTransient<IDateTime, DateTimeService>();
//                //services.AddTransient<IIdentityService, IdentityService>();
//                //services.AddTransient<ICsvFileBuilder, CsvFileBuilder>();
//            }

//            services.AddAuthentication()
//                .AddIdentityServerJwt();

//            return services;
//        }
//    }
//}
