using IdentityServer4.Services;
using IdentityServer4.Validation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace IS4
{
    public static class AppServices
    {
        public static void GetServicesConfiguration(this IServiceCollection services, IConfiguration config)
        {
            services.AddControllersWithViews();
            services.AddDbContext<SQLDbContext>(options =>
                 options.UseSqlServer(config.GetConnectionString("dbconn")));
            //services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            //    .AddEntityFrameworkStores<IDS4SQLDbContext>();

            services.AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;
            })
            .AddDeveloperSigningCredential()
            .AddInMemoryApiResources(ApiResources.Get())
            .AddInMemoryClients(Clients.Get(config))
            .AddInMemoryApiScopes(ApiScopes.Get())
            .AddInMemoryIdentityResources(IdentityResourcesExtensions.GetIdentityResources)
            .AddProfileService<ProfileService>();

            services.AddTransient<IResourceOwnerPasswordValidator, ResourceOwnerPasswordValidator>();
            services.AddTransient<IProfileService, ProfileService>();

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                    .AllowAnyOrigin()
                    //.WithOrigins("http://localhost:5003/")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                );
            });

            services.AddRazorPages();
            services.AddServerSideBlazor();
            //services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();
            //services.AddSingleton(s =>
            //        new StatisticsServiceManager(s.GetRequiredService<IFirebaseServiceManager>()));
            //            var cors = new DefaultCorsPolicyService(ILoggerFactory.CreateLogger<DefaultCorsPolicyService>())
            //                    {
            //                        AllowedOrigins = { "http://localhost:5003/", "https://bar" }
            //                    };
            //services.AddSingleton<ICorsPolicyService>(cors);

            services
               .AddTransient<IFirebaseServiceManager, FirebaseServiceManager>()
               .AddTransient<ILogServiceManager, LogServiceManager>();
            //.AddTransient<IStatisticsServiceManager, StatisticsServiceManager>(s =>
            //     new StatisticsServiceManager(s.GetRequiredService<IFirebaseServiceManager>()));
        }

        public static void GetServicesConfiguration(this IHostBuilder host)
        {
            host.UseSerilog();
            //TODO: Lyuben - Think on smarter approach how to trick scoped service bus consumer
            host.UseDefaultServiceProvider(options => options.ValidateScopes = false);

        }
    }
}
