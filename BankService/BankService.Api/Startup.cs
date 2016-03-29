using BankService.Data.Contexts;
using BankService.Data.Repositories;
using BankService.Domain.Contracts;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace BankService.Api
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            // Set up configuration sources.
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json");
                

            if (env.IsEnvironment("Development"))
            {
                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                //builder.AddApplicationInsightsSettings(developerMode: true);
            }

            builder.AddEnvironmentVariables();
            // Configuration = builder.Build().ReloadOnChanged("appsettings.json");
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            //services.AddApplicationInsightsTelemetry(Configuration);

            services.AddMvc();

            #region Setup DI
            var mongoDBContext = new MongoDBContext(
                Environment.GetEnvironmentVariable("MONGODB_SERVER"),
                Environment.GetEnvironmentVariable("MONGODB_PORT"),
                Environment.GetEnvironmentVariable("MONGODB_DATABASE")
            );

            var redisContext = new RedisContext(
                Environment.GetEnvironmentVariable("REDIS_SERVER"),
                Environment.GetEnvironmentVariable("REDIS_PORT")
            );

            services.AddInstance<IMongoDBContext>(mongoDBContext);
            services.AddInstance<IRedisContext>(redisContext);
            services.AddSingleton<IAccountHolderRepository, AccountHolderRepository>();
            services.AddSingleton<ICachedAccountHolderRepository, CachedAccountHolderRepository>();
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseIISPlatformHandler();

            //app.UseApplicationInsightsRequestTelemetry();

            //app.UseApplicationInsightsExceptionTelemetry();

            app.UseStaticFiles();

            app.UseMvc();
        }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
