using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using nhH60Store.Areas.Identity.Data;
using nhH60Store.Data;

[assembly: HostingStartup(typeof(nhH60Store.Areas.Identity.IdentityHostingStartup))]
namespace nhH60Store.Areas.Identity {
    public class IdentityHostingStartup : IHostingStartup {
        public void Configure(IWebHostBuilder builder) {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<nhH60StoreContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("MyConnection")));
            });
        }
    }
}