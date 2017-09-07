using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenShiftDemoApp.Models;

namespace OpenShiftDemoApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {
            if (File.Exists("/etc/data/appsettings.json"))
            {
                var builder = new ConfigurationBuilder()
                    .AddJsonFile("/etc/data/appsettings.json");
                Configuration = builder.Build();
            }
            else
            {
                if (environment.IsDevelopment())
                {
                    Configuration = configuration;
                }
                else
                    throw new Exception("Configuration file is required for Production Env: /etc/data/appsettings.json");
            }
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.Configure<HomeOption>(Configuration);
            services.Configure<HomeOption>(Configuration.GetSection("subsection"));
            
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            Configuration.AsEnumerable().ToDictionary(k => k, v => v);
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
