using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LogWebApi.Extensions;
using LogWebApi.Model;
using LogWebApi.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using OpenTelemetry;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace LogWebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddHttpContextAccessor();

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.TryAddTransient<ITraceHelper, TraceHelper>();

            // OpenTelemetry 設定
            var appResourceBuilder = ResourceBuilder.CreateDefault()
                .AddService(serviceName: SysOpenTelemetryCollector.SourceName, serviceVersion: "1.0.0");

            services.AddOpenTelemetry().WithTracing(
                (builder) => builder
                    .AddSource(SysOpenTelemetryCollector.MyActivitySource.Name)
                    .SetResourceBuilder(appResourceBuilder)
                    .AddAspNetCoreInstrumentation()  // 打進來的request記錄
                    .AddEntityFrameworkCoreInstrumentation(option => option.SetDbStatementForText = false)
                    .AddHttpClientInstrumentation()  // 打其它系統API的記錄
                    .AddInMemoryExporter(SysOpenTelemetryCollector.ExportedItems)
            );
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
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseMiddleware<RequestMiddleware>();

            app.ConfigureExceptionHandler();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Default}/{action=Index}");
            });
        }
    }
}
