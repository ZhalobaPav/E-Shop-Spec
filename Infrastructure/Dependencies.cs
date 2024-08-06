using Infrastructure.Data;
using Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static class Dependencies
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            bool useOnlyInMemoryDatabase = false;
            if (configuration["UseOnlyInMemoryDatabase"] != null)
            {
                useOnlyInMemoryDatabase = bool.Parse(configuration["UseOnlyInMemoryDatabase"]!);
            }
            if (useOnlyInMemoryDatabase)
            {
                services.AddDbContext<ProdContext>(c =>
                   c.UseInMemoryDatabase("Catalog"));
                services.AddDbContext<AppIdentityDbContext>(options =>
                    options.UseInMemoryDatabase("Identity"));
            }
            else
            {
                services.AddDbContext<ProdContext>(c =>
                    c.UseSqlServer(configuration.GetConnectionString("defaultConnection"), b=>b.MigrationsAssembly("E-Js")));

                services.AddDbContext<AppIdentityDbContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("IdentityConnection"), b => b.MigrationsAssembly("E-Js")));
            }
        }
    }
}
