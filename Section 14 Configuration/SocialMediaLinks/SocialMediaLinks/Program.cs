using SocialMediaLinks.Options;

var builder = WebApplication.CreateBuilder(args);

//services
builder.Services.AddControllersWithViews();
builder.Services.Configure<SocialMediaLinksOptions>(builder.Configuration.GetSection("SocialMediaLinks"));

var app = builder.Build();

//middlewares
app.UseStaticFiles();
app.UseRouting();
app.MapControllers();

app.Run();
