using LoginApp.CustomMiddlewares;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseLoginMiddleware();

app.Run(async (HttpContext context) =>
{
    await context.Response.StartAsync();
});

app.Run();