using System;
using Karma.Areas.Identity.Data;
using Karma.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(Karma.Areas.Identity.IdentityHostingStartup))]
namespace Karma.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<KarmaDbContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("KarmaDbContextConnection")));

                services.AddDefaultIdentity<KarmaUser>(options => {
                    options.SignIn.RequireConfirmedAccount = false;
                    options.Password.RequireDigit = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireNonAlphanumeric = false;                    
                    })
                    .AddEntityFrameworkStores<KarmaDbContext>();
            });
        }
    }
}