using Entities;
using Microsoft.EntityFrameworkCore;
using Repositories;
using RepositoryContracts;
using ServiceContracts.FinnhubServiceContracts;
using ServiceContracts.StocksServiceContracts;
using Services.FinnhubServices;
using Services.StocksServices;
using StocksApp.Options;

namespace StocksApp.StartupExtensions
{
    public static class ConfigureServicesExtension
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            //Services
            services.AddControllersWithViews();
            services.AddHttpClient();

            services.AddScoped<IFinnhubCompanyProfileService, FinnhubCompanyProfileService>();
            services.AddScoped<IFinnhubSearchStocksService, FinnhubSearchStocksService>();
            services.AddScoped<IFinnhubStockPriceQuoteService, FinnhubStockPriceQuoteService>();
            services.AddScoped<IFinnhubStocksService, FinnhubStocksService>();

            services.AddScoped<IStocksCreateBuyOrderService, StocksCreateBuyOrderService>();
            services.AddScoped<IStocksCreateSellOrderService, StocksCreateSellOrderService>();
            services.AddScoped<IStocksGetBuyOrdersService, StocksGetBuyOrdersService>();
            services.AddScoped<IStocksGetSellOrdersService, StocksGetSellOrdersService>();

            services.AddScoped<IFinnhubRepository, FinnhubRepository>();
            services.AddScoped<IStocksRepository, StocksRepository>();
            services.Configure<TradingOptions>(configuration.GetSection("TradingOptions"));

            services.AddDbContext<StockMarketDbContext>
                (options =>
                {
                    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
                });

            services.AddHttpLogging(options =>
            {
                options.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.RequestProperties | Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.ResponsePropertiesAndHeaders;
            });

            return services;
        }
    }
}