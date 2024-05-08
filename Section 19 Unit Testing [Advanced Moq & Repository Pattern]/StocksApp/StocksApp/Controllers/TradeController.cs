using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Rotativa.AspNetCore;
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

        [Route("[action]/{stockSymbol?}")]
        public async Task<IActionResult> Index(string? stockSymbol)
        {
            if (stockSymbol is null)
            {
                stockSymbol = "MSFT";
            }
            if (_tradingOptions.Value.DefaultOrderQuantity == 0)
            {
                _tradingOptions.Value.DefaultOrderQuantity = 100;
            }
            Dictionary<string, object>? companyProfile = await _finnhubService.GetCompanyProfile(stockSymbol);
            Dictionary<string, object>? stockPriceQuote = await _finnhubService.GetStockPriceQuote(stockSymbol);
            StockTrade stockTrade = new StockTrade()
            {
                StockSymbol = companyProfile?["ticker"].ToString() ?? null,
                StockName = companyProfile?["name"].ToString() ?? null,
                Price = Math.Round(Convert.ToDouble(stockPriceQuote?["c"].ToString()), 2),
                Quantity = _tradingOptions.Value.DefaultOrderQuantity
            };
            ViewBag.FinnhubToken = _configuration["FinnhubToken"];
            return View(stockTrade);
        }

        [Route("[action]")]
        public async Task<IActionResult> Orders()
        {
            List<BuyOrderResponse> buyOrderResponses = await _stocksService.GetBuyOrders();
            List<SellOrderResponse> sellOrderResponses = await _stocksService.GetSellOrders();
            Orders orders = new Orders()
            {
                BuyOrders = buyOrderResponses,
                SellOrders = sellOrderResponses
            };
            return View(orders);
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> BuyOrder(BuyOrderRequest buyOrderRequest)
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
            BuyOrderResponse buyOrderResponse = await _stocksService.CreateBuyOrder(buyOrderRequest);
            return RedirectToAction(nameof(Orders));
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> SellOrder(SellOrderRequest sellOrderRequest)
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
            SellOrderResponse sellOrderResponse = await _stocksService.CreateSellOrder(sellOrderRequest);
            return RedirectToAction(nameof(Orders));
        }

        [Route("[action]")]
        public async Task<IActionResult> OrdersPDF()
        {
            List<BuyOrderResponse> buyOrderResponses = await _stocksService.GetBuyOrders();
            List<SellOrderResponse> sellOrderResponses = await _stocksService.GetSellOrders();
            Orders orders = new Orders()
            {
                BuyOrders = buyOrderResponses,
                SellOrders = sellOrderResponses
            };
            return new ViewAsPdf("OrdersPDF", orders, ViewData)
            {
                PageMargins = new Rotativa.AspNetCore.Options.Margins() { Top = 20, Right = 20, Bottom = 20, Left = 20 },
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Landscape
            };
        }
    }
}
