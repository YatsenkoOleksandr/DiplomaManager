using System;
using System.Text;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using DiplomaManager.BLL.Services;
using DiplomaManager.Common.Autofac;
using DiplomaManager.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using Serilog;
using Serilog.Events;
using Serilog.Exceptions;
using Serilog.Formatting.Json;

namespace DiplomaManager
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
            _configuration = builder.Build();

            // Configure the Serilog pipeline
            Log.Logger = new LoggerConfiguration()
                .Enrich.WithExceptionDetails()
                .WriteTo.RollingFile(
                    new JsonFormatter(renderMessage: true),
                    @"Logs\log-{Date}.txt",
                    LogEventLevel.Warning)
                .CreateLogger(); 
        }

        public IContainer ApplicationContainer { get; private set; }

        private readonly IConfigurationRoot _configuration;

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(ConfigureMvcOptions)
                .AddJsonOptions(ConfigureJsonOptions);

            ApplicationContainer = services.AddAutofac(_configuration);
           
            return new AutofacServiceProvider(ApplicationContainer);
        }

        private static void ConfigureMvcOptions(MvcOptions options)
        {
            options.Filters.Add(new AuthorizeAreaFilterAttribute("Admin", new[] { "Администраторы" }));
            options.Filters.Add(new AuthorizeAreaFilterAttribute("Teacher", new[] { "Администраторы", "Преподаватели" }));
        }

        private static void ConfigureJsonOptions(MvcJsonOptions options)
        {
            options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory
            , IApplicationLifetime appLifetime)
        {
            loggerFactory.AddSerilog();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "areaRoute",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "spa-fallback-area",
                    template: "{area:exists}/{*url}",
                    defaults: new { controller = "Home", action = "Index" });

                routes.MapRoute(
                    name: "spa-fallback",
                    template: "{*url}",
                    defaults: new {controller = "Home", action = "Index"});
            });

            SampleData.Initialize(ApplicationContainer);

            appLifetime.ApplicationStopped.Register(() => ApplicationContainer.Dispose());
        }
    }
}
