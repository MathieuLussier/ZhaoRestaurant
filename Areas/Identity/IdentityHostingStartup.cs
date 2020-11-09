using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using tp1_restaurant.Areas.Identity.Data;

[assembly: HostingStartup(typeof(tp1_restaurant.Areas.Identity.IdentityHostingStartup))]
namespace tp1_restaurant.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<IdentityDataContext>(options =>
                    options.UseMySql(
                        context.Configuration.GetConnectionString("IdentityDataContextConnection")));

                services.AddIdentity<IdentityUser, IdentityRole>()
                    .AddDefaultTokenProviders()
                    .AddDefaultUI()
                    .AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<IdentityDataContext>();
            });
        }
    }
}