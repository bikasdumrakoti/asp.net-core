using ServiceContracts;
using Services;

var builder = WebApplication.CreateBuilder(args);

//services
builder.Services.AddControllersWithViews();

//add services into IoC container
builder.Services.AddSingleton<ICountriesService, CountriesService>();
builder.Services.AddSingleton<IPersonsService, PersonsService>();

var app = builder.Build();

if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

//middlewares
app.UseStaticFiles();
app.UseRouting();
app.MapControllers();

app.Run();
