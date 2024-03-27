using ServiceContracts;
using Services;
using StocksAppAssignment.Options;

var builder = WebApplication.CreateBuilder(args);

//services
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();
builder.Services.AddScoped<IFinnhubService, FinnhubService>();
builder.Services.Configure<TradingOptions>(builder.Configuration.GetSection("TradingOptions"));

var app = builder.Build();

//middlewares
app.UseStaticFiles();
app.UseRouting();
app.MapControllers();

app.Run();