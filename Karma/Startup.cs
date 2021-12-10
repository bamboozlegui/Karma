using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Karma.Services;
using Karma.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Karma.Areas.Identity.Data;
using System.Text.Json.Serialization;
using System.Net.Http;
using Newtonsoft.Json;
using Karma.Middleware;

namespace Karma
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
            services.AddRazorPages()
                .AddJsonOptions(o =>
                {
                    o.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
                    o.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                });
            services.AddScoped<IItemRepository, SqlItemRepository>();
            services.AddScoped<IRequestRepository, SqlRequestRepository>();
            services.AddScoped<IMessageRepository, SQLMessageRepository>();
            services.AddScoped<IFulfillmentRepository, SQLFulfillmentRepository>();
            services.AddScoped<HttpClient>();
            services.AddScoped<UserManager<KarmaUser>>();
            services.AddTransient<PictureService>();
            services.AddScoped<NotificationService>();
            services.AddScoped<KarmaPointService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseHttpContext();

            app.UseAuthentication();
            app.UseAuthorization();

            // Log user posts/requests
            app.UseWhen(context => context.Request.Method.Equals("POST"), appBuilder =>
            {
                appBuilder.UseMiddleware<RequestLoggingMiddleware>();

            });

            // Check if any errors occur
            app.UseMiddleware<ErrorLoggingMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
