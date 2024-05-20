using Microsoft.AspNetCore.Mvc.Filters;
using ServiceContracts.DTOs;
using StocksApp.Controllers;
using StocksApp.ViewModels;

namespace StocksApp.Filters.ActionFilters
{
    public class CreateOrderActionFilter : IAsyncActionFilter
    {
        private readonly ILogger<CreateOrderActionFilter> _logger;
        private readonly IConfiguration _configuration;

        public CreateOrderActionFilter(ILogger<CreateOrderActionFilter> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            //before logic
            _logger.LogInformation("Before logic of {FilterName}.{MethodName} method.", nameof(CreateOrderActionFilter), nameof(OnActionExecutionAsync));

            if (context.Controller is TradeController tradeController)
            {
                var orderRequest = context.ActionArguments["orderRequest"] as IOrderRequest;
                if (orderRequest is not null)
                {
                    orderRequest.DateAndTimeOfOrder = DateTime.Now;
                    tradeController.ModelState.Clear();
                    tradeController.TryValidateModel(orderRequest);
                    if (!tradeController.ModelState.IsValid)
                    {
                        tradeController.ViewBag.Errors = tradeController.ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                        StockTrade stockTrade = new StockTrade() { StockName = orderRequest.StockName, Quantity = orderRequest.Quantity, StockSymbol = orderRequest.StockSymbol, Price = orderRequest.Price };
                        tradeController.ViewBag.FinnhubToken = _configuration["FinnhubToken"];
                        context.Result = tradeController.View(nameof(TradeController.Index), stockTrade);
                    }
                    else
                    {
                        await next();
                    }
                }
                else
                {
                    await next();
                }
            }
            else
            {
                await next();
            }

            //after logic
            _logger.LogInformation("After logic of {FilterName}.{MethodName} method.", nameof(CreateOrderActionFilter), nameof(OnActionExecutionAsync));
        }
    }
}
