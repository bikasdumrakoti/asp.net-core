var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    Dictionary<int, string> keyValuePairs = new Dictionary<int, string>
    {
        {1, "United States" },
        {2, "Canada" },
        {3, "United Kingdom" },
        {4, "India" },
        {5, "Japan" }
    };

    //route to /countries
    endpoints.MapGet("countries", async context =>
    {
        context.Response.StatusCode = 200;
        foreach (var keyValuePair in keyValuePairs)
        {
            await context.Response.WriteAsync(keyValuePair.Key + ", " + keyValuePair.Value + "\n");
        }
    });

    //route to /countries/{countryID}
    endpoints.MapGet("countries/{countryID:int:range(1,100)}", async context =>
    {
        int countryID = Convert.ToInt32(context.Request.RouteValues["countryID"]);
        if (countryID >= 1 && countryID <= 5)
        {
            string country = keyValuePairs[countryID];
            context.Response.StatusCode = 200;
            await context.Response.WriteAsync(country);
        }
        else
        {
            context.Response.StatusCode = 404;
            await context.Response.WriteAsync("[No Country]");
        }
    });
});

app.Run(async context =>
{
    context.Response.StatusCode = 400;
    await context.Response.WriteAsync("The CountryID should be between 1 and 100");
});

app.Run();
