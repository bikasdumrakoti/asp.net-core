using MiddlewareExample.CustomMiddleware;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<MyCustomMiddleware>();
var app = builder.Build();

//middleware 1
app.Use(async (HttpContext context, RequestDelegate next) =>
{
    await context.Response.WriteAsync("From Middleware 1\n");
    await next(context);
});

//middleware 2
//app.UseMiddleware<MyCustomMiddleware>();
app.UseMyCustomMiddleware();
app.UseHelloCustomMiddleware();

//middleware 3
app.Run(async (HttpContext context) =>
{
    await context.Response.WriteAsync("From Middleware 3\n");
});

app.Run();
