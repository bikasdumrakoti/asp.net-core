using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ServiceContracts;
using StocksApp.Options;
using StocksApp.ViewModels;

namespace StocksApp.Controllers
{
    [Route("[controller]")]
    public class StocksController : Controller
    {
        private readonly IFinnhubService _finnhubService;
        private readonly IOptions<TradingOptions> _tradingOptions;
        private readonly ILogger<StocksController> _logger;

        public StocksController(IFinnhubService finnhubService, IOptions<TradingOptions> tradingOptions, ILogger<StocksController> logger)
        {
            _finnhubService = finnhubService;
            _tradingOptions = tradingOptions;
            _logger = logger;
        }

        [Route("/")]
        [Route("[action]/{stock?}")]
        public async Task<IActionResult> Explore(string? stock, bool showAllStocks = false)
        {
            _logger.LogInformation("Explore action method of StocksController");
            _logger.LogDebug($"Stock: {stock}, Show all Stocks: {showAllStocks}");

            List<Dictionary<string, string>>? allStocks = await _finnhubService.GetStocks();
            List<Stock> stocks = new List<Stock>();
            if (allStocks is not null)
            {
                if (!showAllStocks && _tradingOptions.Value.Top25PopularStocks is not null)
                {
                    string[]? stockSymbols = _tradingOptions.Value.Top25PopularStocks.Split(",");
                    if (stockSymbols is not null)
                    {
                        allStocks = allStocks.Where(temp => stockSymbols.Contains(temp["symbol"].ToString())).ToList();
                    }
                }
                stocks = allStocks.OrderBy(temp => temp["description"]).Select(temp => new Stock
                {
                    StockSymbol = temp["symbol"].ToString(),
                    StockName = temp["description"].ToString()
                }).ToList();
            }
            ViewBag.Stock = stock;
            return View(stocks);
        }
    }
}
