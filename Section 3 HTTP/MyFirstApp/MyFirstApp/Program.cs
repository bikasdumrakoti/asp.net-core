using Microsoft.Extensions.Primitives;
using System.IO;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Run(async (HttpContext context) =>
{
    StreamReader reader = new StreamReader(context.Request.Body);
    string body = await reader.ReadToEndAsync();

    Dictionary<string, StringValues> queryDict = Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(body);
    if (queryDict.ContainsKey("age"))
    {
        foreach (var age in queryDict["age"])
        {
            await context.Response.WriteAsync(age + "\n");
        }
    }
});

app.Run();