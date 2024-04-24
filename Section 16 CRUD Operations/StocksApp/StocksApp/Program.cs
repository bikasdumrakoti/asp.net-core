using ServiceContracts;
using Services;
using StocksApp.Options;

var builder = WebApplication.CreateBuilder(args);

//Services
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();
builder.Services.AddSingleton<IFinnhubService, FinnhubService>();
builder.Services.AddSingleton<IStocksService, StocksService>();
builder.Services.Configure<TradingOptions>(builder.Configuration.GetSection("TradingOptions"));

var app = builder.Build();

//Default exception page for development environment
if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

//Middlewares
app.UseStaticFiles();
app.UseRouting();
app.MapControllers();

app.Run();
