using Entities;
using ServiceContracts;
using Services;
using StocksApp.Options;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;
using Repositories;
using Serilog;
using StocksApp.Middlewares;

var builder = WebApplication.CreateBuilder(args);

//Serilog
builder.Host.UseSerilog((HostBuilderContext context, IServiceProvider services, LoggerConfiguration loggerConfiguration) =>
{
    loggerConfiguration
    .ReadFrom.Configuration(context.Configuration)
    .ReadFrom.Services(services);
});

//Services
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();
builder.Services.AddScoped<IFinnhubService, FinnhubService>();
builder.Services.AddScoped<IStocksService, StocksService>();
builder.Services.AddScoped<IFinnhubRepository, FinnhubRepository>();
builder.Services.AddScoped<IStocksRepository, StocksRepository>();
builder.Services.Configure<TradingOptions>(builder.Configuration.GetSection("TradingOptions"));

builder.Services.AddDbContext<StockMarketDbContext>
    (options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    });

builder.Services.AddHttpLogging(options =>
{
    options.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.RequestProperties | Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.ResponsePropertiesAndHeaders;
});

var app = builder.Build();

//Default exception page for development environment
if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseExceptionHandlingMiddleware();
}

if (builder.Environment.IsEnvironment("Test") == false)
{
    Rotativa.AspNetCore.RotativaConfiguration.Setup("wwwroot", wkhtmltopdfRelativePath: "Rotativa");
}

app.UseSerilogRequestLogging();

app.UseHttpLogging();

//Middlewares
app.UseStaticFiles();
app.UseRouting();
app.MapControllers();

app.Run();

public partial class Program { } //make the auto-generated Program accessible programmatically