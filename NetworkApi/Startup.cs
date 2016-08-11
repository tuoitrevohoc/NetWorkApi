using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NetWorkApi.Models;
using NetWorkApi.Repositories;
using MongoDB.Driver;
using System;
using NetWorkApi.Core.Exceptions;

namespace NetWorkApi
{

    /// <summary>
    /// Start up program
    /// </summary>
    public class Startup
    {

        /// <summary>
        /// Start up the project - collect settings
        /// </summary>
        /// <param name="env"></param>
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        /// <summary>
        /// the configuration
        /// </summary>
        public IConfigurationRoot Configuration { get; }

        /// <summary>
        /// The services
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();
            services.AddSwaggerGen();

            var mongoUri = Environment.GetEnvironmentVariable("MONGO_URI")
                                ?? "mongodb://127.0.0.1:27017";

            var databaseName = Environment.GetEnvironmentVariable("MONGO_DB") ?? "networkapi";
            var client = new MongoClient(mongoUri);
            var database = client.GetDatabase(databaseName);

            services.AddSingleton(database);

            services.AddSingleton<IRepository<AppUser>>(
                    context => new Repository<AppUser>(
                        database.GetCollection<AppUser>("users")
                    )
                );
        }

        /// <summary>
        /// Configuration
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="loggerFactory"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            //loggerFactory.AddDebug();

            app.UseMiddleware<ExceptionHandlerMiddleware>();
            app.UseMvc();

            app.UseDefaultFiles();
            app.UseStaticFiles();

            // Enable middleware to serve generated Swagger as a JSON endpoint
            app.UseSwagger();

            // Enable middleware to serve swagger-ui assets (HTML, JS, CSS etc.)
            app.UseSwaggerUi();
        }
    }
}
