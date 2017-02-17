using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using DiplomaManager.BLL.Infrastructure;
using DiplomaManager.BLL.Interfaces;
using DiplomaManager.BLL.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

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
            Configuration = builder.Build();
        }

        public IContainer ApplicationContainer { get; private set; }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddElm();
            services.AddElm(options => {
                options.Path = new PathString("/elm");
                options.Filter = (name, level) => level >= LogLevel.Error;
            });

            // Add framework services.
            services.AddMvc();

            // Create the Autofac container builder.
            var builder = new ContainerBuilder();

            // Add any Autofac modules or registrations.
            RegisterServices(builder);

            // Populate the services.
            builder.Populate(services);

            // Build the container.
            ApplicationContainer = builder.Build();

            // Create and return the service provider.
            return new AutofacServiceProvider(ApplicationContainer);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory
            , IApplicationLifetime appLifetime)
        {
            app.Map("/HelloMVC6", map =>
            {
                app.UseElmPage();
                app.UseElmCapture();
                app.UseDeveloperExceptionPage();
                app.UseMvcWithDefaultRoute();
            });

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationScheme = "Admin",
                LoginPath = new Microsoft.AspNetCore.Http.PathString("/Admin/Account/Login"),
                AutomaticAuthenticate = true,
                AutomaticChallenge = true
            });

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

            // вызываем инициализатор
            SampleData.Initialize(ApplicationContainer);

            // If you want to dispose of resources that have been resolved in the
            // application container, register for the "ApplicationStopped" event.
            appLifetime.ApplicationStopped.Register(() => ApplicationContainer.Dispose());
        }

        private static void RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterModule(
                new ServiceModule("Server=(local);Database=diplomamanagerdb;Trusted_Connection=True;"));

            builder.RegisterType<RequestService>().As<IRequestService>();
            builder.RegisterType<UserService>().As<IUserService>();
            builder.RegisterType<TeacherService>().As<ITeacherService>();
        }
    }
}
