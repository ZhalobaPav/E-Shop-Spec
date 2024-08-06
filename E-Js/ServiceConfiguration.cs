using ApplicationCore.Enities;
using ApplicationCore.Interfaces;
using ApplicationCore.Services;
using Infrastructure.Data;
using Infrastructure.Data.Querries;
using Infrastructure.Identity;
using Infrastructure.Logging;

namespace E_Js
{
    public static class ServiceConfiguration
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<CatalogSettings>(configuration);
            var catalogSettings = configuration.Get<CatalogSettings>() ?? new CatalogSettings();
            services.AddSingleton<IUriComposer>(new UriComposer(catalogSettings));

            services.AddScoped(typeof(IRepository<>), typeof(EFRepository<>));
            services.AddScoped(typeof(IReadRepository<>), typeof(EFRepository<>));
            services.AddScoped(typeof(ICustomLogger<>), typeof(CustomLogger<>));

            services.AddScoped<ITokenClaimService, IdentityTokenClaimsService>();
            services.AddScoped<IBasketService, BasketService>(); 
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<UserService>();
            services.AddScoped<IBasketQueryService, BasketQueryService>();
            services.AddScoped<IBasketViewModelService, BasketViewModelService>();
            return services;
        }
    }
}
