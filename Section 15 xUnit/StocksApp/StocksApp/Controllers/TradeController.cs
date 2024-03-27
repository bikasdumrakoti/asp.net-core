using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ServiceContracts;
using StocksApp.Options;
using StocksApp.ViewModels;

namespace StocksApp.Controllers
{
    public class TradeController : Controller
    {
        private readonly IOptions<TradingOptions> _tradingOptions;
        private readonly IFinnhubService _finnhubService;

        public TradeController(IOptions<TradingOptions> tradingOptions, IFinnhubService finnhubService)
        {
            _tradingOptions = tradingOptions;
            _finnhubService = finnhubService;
        }

        [Route("/")]
        public async Task<IActionResult> Index()
        {
            if (_tradingOptions.Value.DefaultStockSymbol == null)
            {
                _tradingOptions.Value.DefaultStockSymbol = "MSFT";
            }
            Dictionary<string, object>? companyProfile = await _finnhubService.GetCompanyProfile(_tradingOptions.Value.DefaultStockSymbol);
            Dictionary<string, object>? stockPriceQuote = await _finnhubService.GetStockPriceQuote(_tradingOptions.Value.DefaultStockSymbol);
            StockTrade stockTrade = new StockTrade()
            {
                StockSymbol = companyProfile["ticker"].ToString(),
                StockName = companyProfile["name"].ToString(),
                Price = Convert.ToDouble(stockPriceQuote["c"].ToString())
            };
            return View(stockTrade);
        }
    }
}
