using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ServiceContracts;
using ServiceContracts.DTOs;
using StocksApp.Options;
using StocksApp.ViewModels;

namespace StocksApp.Controllers
{
    [Route("[controller]")]
    public class TradeController : Controller
    {
        private readonly IOptions<TradingOptions> _tradingOptions;
        private readonly IFinnhubService _finnhubService;
        private readonly IConfiguration _configuration;
        private readonly IStocksService _stocksService;

        public TradeController(IOptions<TradingOptions> tradingOptions, IFinnhubService finnhubService, IConfiguration configuration, IStocksService stocksService)
        {
            _tradingOptions = tradingOptions;
            _finnhubService = finnhubService;
            _configuration = configuration;
            _stocksService = stocksService;
        }

        [Route("/")]
        [Route("[action]")]
        [Route("~/[controller]")]
        public async Task<IActionResult> Index()
        {
            if (_tradingOptions.Value.DefaultStockSymbol == null)
            {
                _tradingOptions.Value.DefaultStockSymbol = "MSFT";
            }
            if (_tradingOptions.Value.DefaultOrderQuantity == 0)
            {
                _tradingOptions.Value.DefaultOrderQuantity = 100;
            }
            Dictionary<string, object>? companyProfile = await _finnhubService.GetCompanyProfile(_tradingOptions.Value.DefaultStockSymbol);
            Dictionary<string, object>? stockPriceQuote = await _finnhubService.GetStockPriceQuote(_tradingOptions.Value.DefaultStockSymbol);
            StockTrade stockTrade = new StockTrade()
            {
                StockSymbol = companyProfile?["ticker"].ToString() ?? null,
                StockName = companyProfile?["name"].ToString() ?? null,
                Price = Convert.ToDouble(stockPriceQuote?["c"].ToString()),
                Quantity = _tradingOptions.Value.DefaultOrderQuantity
            };
            ViewBag.FinnhubToken = _configuration["FinnhubToken"];
            return View(stockTrade);
        }

        [Route("[action]")]
        public IActionResult Orders()
        {
            List<BuyOrderResponse> buyOrderResponses = _stocksService.GetBuyOrders();
            List<SellOrderResponse> sellOrderResponses = _stocksService.GetSellOrders();
            Orders orders = new Orders()
            {
                BuyOrders = buyOrderResponses,
                SellOrders = sellOrderResponses
            };
            return View(orders);
        }

        [Route("[action]")]
        [HttpPost]
        public IActionResult BuyOrder(BuyOrderRequest buyOrderRequest)
        {
            buyOrderRequest.DateAndTimeOfOrder = DateTime.Now;
            ModelState.Clear();
            TryValidateModel(buyOrderRequest);
            if (!ModelState.IsValid)
            {
                ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                StockTrade stockTrade = new StockTrade() { StockName = buyOrderRequest.StockName, Quantity = buyOrderRequest.Quantity, StockSymbol = buyOrderRequest.StockSymbol, Price = buyOrderRequest.Price };
                ViewBag.FinnhubToken = _configuration["FinnhubToken"];
                return View("Index", stockTrade);
            }
            BuyOrderResponse buyOrderResponse = _stocksService.CreateBuyOrder(buyOrderRequest);
            return RedirectToAction(nameof(Orders));
        }

        [Route("[action]")]
        [HttpPost]
        public IActionResult SellOrder(SellOrderRequest sellOrderRequest)
        {
            sellOrderRequest.DateAndTimeOfOrder = DateTime.Now;
            ModelState.Clear();
            TryValidateModel(sellOrderRequest);
            if (!ModelState.IsValid)
            {
                ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                StockTrade stockTrade = new StockTrade() { StockName = sellOrderRequest.StockName, Quantity = sellOrderRequest.Quantity, StockSymbol = sellOrderRequest.StockSymbol, Price = sellOrderRequest.Price };
                ViewBag.FinnhubToken = _configuration["FinnhubToken"];
                return View("Index", stockTrade);
            }
            SellOrderResponse sellOrderResponse = _stocksService.CreateSellOrder(sellOrderRequest);
            return RedirectToAction(nameof(Orders));
        }
    }
}
