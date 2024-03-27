using RoutingExample.CustomConstraints;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRouting(options =>
{
    options.ConstraintMap.Add("months", typeof(MonthsCustomConstraint));
});
var app = builder.Build();

//enable routing
app.UseRouting();

//creating end points
app.UseEndpoints(endpoints =>
{
    //Eg: files/{filename}.{extension}
    endpoints.Map("files/{filename}.{extension}", async context =>
    {
        string? fileName = Convert.ToString(context.Request.RouteValues["filename"]);
        string? extension = Convert.ToString(context.Request.RouteValues["extension"]);
        await context.Response.WriteAsync($"File name is {fileName}.{extension}");
    });

    //Eg: employees/profile/{employeename}
    endpoints.Map("employees/profile/{employeename:length(3,7):alpha=Bikash}", async context =>
    {
        if (context.Request.RouteValues.ContainsKey("employeename"))
        {
            string? employeeName = Convert.ToString(context.Request.RouteValues["employeename"]);
            await context.Response.WriteAsync($"Showing profile of {employeeName}");
        }
        else
        {
            await context.Response.WriteAsync("Employee name is not supplied");
        }
    });

    //Eg: products/detail/{id}
    endpoints.Map("products/detail/{id:int:range(1,1000)}", async context =>
    {
        if (context.Request.RouteValues.ContainsKey("id"))
        {
            int id = Convert.ToInt32(context.Request.RouteValues["id"]);
            await context.Response.WriteAsync($"Product detail for id {id}");
        }
        else
        {
            await context.Response.WriteAsync("Product id is not supplied");
        }
    });

    //Eg: daily-digest-report/{reportdate}
    endpoints.Map("daily-digest-report/{reportdate:datetime}", async context =>
    {
        DateTime dateTime = Convert.ToDateTime(context.Request.RouteValues["reportdate"]);
        await context.Response.WriteAsync($"Report date is {dateTime.ToShortDateString()}");
    });

    //Eg: cities/{cityid}
    endpoints.Map("cities/{cityid:guid}", async context =>
    {
        Guid cityId = Guid.Parse(Convert.ToString(context.Request.RouteValues["cityid"])!);
        await context.Response.WriteAsync($"City id is {cityId}");
    });

    //Eg: students/{contactnumber}
    endpoints.Map("students/{contactnumber:length(10)}", async context =>
    {
        string? contactNumber = Convert.ToString(context.Request.RouteValues["contactnumber"]);
        await context.Response.WriteAsync($"Student contact number is {contactNumber}");
    });

    //Eg: students/{contactnumber} for endpoint selection order demonstration
    endpoints.Map("students/{contactnumber}", async context =>
    {
        string? contactNumber = Convert.ToString(context.Request.RouteValues["contactnumber"]);
        await context.Response.WriteAsync($"Student contact number is {contactNumber} - it is for demonstration purpose");
    });

    //Eg: sales-report/{year}/{month}
    endpoints.Map("sales-report/{year:int:min(1900)}/{month:months}", async context =>
    {
        int year = Convert.ToInt32(context.Request.RouteValues["year"]);
        string? month = Convert.ToString(context.Request.RouteValues["month"]);
        //if condition to demonstrate parameter validation
        //this if condition therefore voilates the regex route constraint if executed
        if (month == "mar" || month == "jun" || month == "sep" || month == "dec")
        {
            await context.Response.WriteAsync($"Sales report for {month}, {year}");
        }
        else
        {
            await context.Response.WriteAsync($"Sales report for {month}, {year} is not allowed");
        }
    });

    //Eg: sales-report/2023/mar
    endpoints.Map("sales-report/2023/mar", async context =>
    {
        await context.Response.WriteAsync("Sales report exclusively for mar, 2023");
    });
});

app.Run(async context =>
{
    await context.Response.WriteAsync($"No route matched at {context.Request.Path}");
});

app.Run();
