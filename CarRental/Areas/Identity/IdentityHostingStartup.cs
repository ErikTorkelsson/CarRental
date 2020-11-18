using System;
using CarRental.Areas.Identity.Data;
using CarRental.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(CarRental.Areas.Identity.IdentityHostingStartup))]
namespace CarRental.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<CarRentalContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("CarRentalContextConnection")));

                services.AddDefaultIdentity<CarRentalUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<CarRentalContext>();
            });
        }
    }
}