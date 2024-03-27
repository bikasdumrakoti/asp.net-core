using Microsoft.Extensions.Primitives;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Run(async (HttpContext context) =>
{
    string queryString = context.Request.QueryString.ToString();
    Dictionary<string, StringValues> queryDict = Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(queryString);
    if (queryDict.ContainsKey("firstNumber") && queryDict.ContainsKey("secondNumber") && queryDict.ContainsKey("operation"))
    {
        if (queryDict["operation"][0].Equals("add"))
        {
            context.Response.StatusCode = 200;
            double sum = Convert.ToDouble(queryDict["firstNumber"][0]) + Convert.ToDouble(queryDict["secondNumber"][0]);
            await context.Response.WriteAsync(sum.ToString());
        }
        else if (queryDict["operation"][0].Equals("subtract"))
        {
            context.Response.StatusCode = 200;
            double difference = Convert.ToDouble(queryDict["firstNumber"][0]) - Convert.ToDouble(queryDict["secondNumber"][0]);
            await context.Response.WriteAsync(difference.ToString());
        }
        else if (queryDict["operation"][0].Equals("multiply"))
        {
            context.Response.StatusCode = 200;
            double product = Convert.ToDouble(queryDict["firstNumber"][0]) * Convert.ToDouble(queryDict["secondNumber"][0]);
            await context.Response.WriteAsync(product.ToString());
        }
        else if (queryDict["operation"][0].Equals("divide"))
        {
            context.Response.StatusCode = 200;
            double division = Convert.ToDouble(queryDict["firstNumber"][0]) / Convert.ToDouble(queryDict["secondNumber"][0]);
            await context.Response.WriteAsync(division.ToString());
        }
        else if (queryDict["operation"][0].Equals("modulusDivide"))
        {
            context.Response.StatusCode = 200;
            double modularDivision = Convert.ToDouble(queryDict["firstNumber"][0]) % Convert.ToDouble(queryDict["secondNumber"][0]);
            await context.Response.WriteAsync(modularDivision.ToString());
        }
        else
        {
            context.Response.StatusCode = 400;
            await context.Response.WriteAsync("Invalid input for 'operation'");
        }
    }
    else
    {
        context.Response.StatusCode = 400;
    }
    if (!queryDict.ContainsKey("firstNumber"))
    {
        await context.Response.WriteAsync("Invalid input for 'firstNumber'\n");
    }
    if (!queryDict.ContainsKey("secondNumber"))
    {
        await context.Response.WriteAsync("Invalid input for 'secondNumber'\n");
    }
    if (!queryDict.ContainsKey("operation"))
    {
        await context.Response.WriteAsync("Invalid input for 'operation'");
    }
});

app.Run();
