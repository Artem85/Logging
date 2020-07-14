using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Logging
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddFile(Path.Combine(Directory.GetCurrentDirectory(), "log.txt"));
            var logger = loggerFactory.CreateLogger<FileLogger>();

            //var logFactory = LoggerFactory.Create(builder =>
            //{
            //    builder.AddDebug();
            //    builder.AddFile($"Logs/app-{DateTime.Today.ToShortDateString()}.txt", LogLevel.Information);
            //    builder.SetMinimumLevel(LogLevel.Debug);
            //});
            //ILogger logger = logFactory.CreateLogger<Startup>();

            if (env.IsDevelopment())
            {
                logger.LogInformation($"ApplicationName: {env.ApplicationName}, EnvironmentName: {env.EnvironmentName}");
                app.UseDeveloperExceptionPage();
            }
            else
            {
                logger.LogInformation($"ApplicationName: {env.ApplicationName}, EnvironmentName: {env.EnvironmentName}");
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
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
        }
    }
}
