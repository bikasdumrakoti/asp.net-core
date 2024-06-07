
using MinimalAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace MinimalAPI.EndpointFilters
{
    public class CustomEndpointFilter : IEndpointFilter
    {
        private readonly ILogger<CustomEndpointFilter> _logger;

        public CustomEndpointFilter(ILogger<CustomEndpointFilter> logger)
        {
            _logger = logger;
        }

        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            //Before logic
            _logger.LogInformation("Endpoint filter - before logic");

            var product = context.Arguments.OfType<Product>().FirstOrDefault();

            if (product == null)
            {
                return Results.BadRequest("Product details are not found in the request");
            }

            var validationContext = new ValidationContext(product);
            List<ValidationResult> errors = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(product, validationContext, errors, true);

            if (!isValid)
            {
                return Results.BadRequest(errors.FirstOrDefault()?.ErrorMessage);
            }

            var result = await next(context); //It invokes the subsequent filter or endpoint's request delegate

            //After logic
            _logger.LogInformation("Endpoint filter - after logic");

            return result;
        }
    }
}
