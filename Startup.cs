﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NetWorkApi.Models;
using NetWorkApi.Repositories;
using MongoDB.Driver;
using System;

namespace NetWorkApi
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();

            services.AddSwaggerGen();

            var mongoUri = Environment.GetEnvironmentVariable("MONGO_URI")
                                ?? "mongodb://localhost:27017";

            Console.WriteLine("Connect to server: " + mongoUri);

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

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

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
