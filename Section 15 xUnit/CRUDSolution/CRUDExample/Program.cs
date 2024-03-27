var builder = WebApplication.CreateBuilder(args);

//services
builder.Services.AddControllersWithViews();

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
