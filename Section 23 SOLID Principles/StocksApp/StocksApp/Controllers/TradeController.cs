using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Rotativa.AspNetCore;
using ServiceContracts.DTOs;
using ServiceContracts.FinnhubServiceContracts;
using ServiceContracts.StocksServiceContracts;
using StocksApp.Filters.ActionFilters;
using StocksApp.Options;
using StocksApp.ViewModels;

namespace StocksApp.Controllers
{
    [Route("[controller]")]
    public class TradeController : Controller
    {
        private readonly IOptions<TradingOptions> _tradingOptions;

        private readonly IFinnhubCompanyProfileService _finnhubCompanyProfileService;
        private readonly IFinnhubStockPriceQuoteService _finnhubStockPriceQuoteService;

        private readonly IConfiguration _configuration;

        private readonly IStocksCreateBuyOrderService _stocksCreateBuyOrderService;
        private readonly IStocksCreateSellOrderService _stocksCreateSellOrderService;
        private readonly IStocksGetBuyOrdersService _stocksGetBuyOrdersService;
        private readonly IStocksGetSellOrdersService _stocksGetSellOrdersService;

        private readonly ILogger<TradeController> _logger;

        public TradeController(IOptions<TradingOptions> tradingOptions, IFinnhubCompanyProfileService finnhubCompanyProfileService, IFinnhubStockPriceQuoteService finnhubStockPriceQuoteService, IConfiguration configuration, IStocksCreateBuyOrderService stocksCreateBuyOrderService, IStocksCreateSellOrderService stocksCreateSellOrderService, IStocksGetBuyOrdersService stocksGetBuyOrdersService, IStocksGetSellOrdersService stocksGetSellOrdersService, ILogger<TradeController> logger)
        {
            _tradingOptions = tradingOptions;

            _finnhubCompanyProfileService = finnhubCompanyProfileService;
            _finnhubStockPriceQuoteService = finnhubStockPriceQuoteService;

            _configuration = configuration;

            _stocksCreateBuyOrderService = stocksCreateBuyOrderService;
            _stocksCreateSellOrderService = stocksCreateSellOrderService;
            _stocksGetBuyOrdersService = stocksGetBuyOrdersService;
            _stocksGetSellOrdersService = stocksGetSellOrdersService;

            _logger = logger;
            _stocksGetBuyOrdersService = stocksGetBuyOrdersService;
            _stocksGetSellOrdersService = stocksGetSellOrdersService;
        }

        [Route("[action]/{stockSymbol?}")]
        public async Task<IActionResult> Index(string? stockSymbol)
        {
            _logger.LogInformation("Index action method of TradeController.");
            _logger.LogDebug($"Stock symbol: {stockSymbol}");

            if (stockSymbol is null)
            {
                stockSymbol = "MSFT";
            }
            if (_tradingOptions.Value.DefaultOrderQuantity == 0)
            {
                _tradingOptions.Value.DefaultOrderQuantity = 100;
            }
            Dictionary<string, object>? companyProfile = await _finnhubCompanyProfileService.GetCompanyProfile(stockSymbol);
            Dictionary<string, object>? stockPriceQuote = await _finnhubStockPriceQuoteService.GetStockPriceQuote(stockSymbol);
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
            _logger.LogInformation("Orders action method of TradeController.");

            List<BuyOrderResponse> buyOrderResponses = await _stocksGetBuyOrdersService.GetBuyOrders();
            List<SellOrderResponse> sellOrderResponses = await _stocksGetSellOrdersService.GetSellOrders();
            Orders orders = new Orders()
            {
                BuyOrders = buyOrderResponses,
                SellOrders = sellOrderResponses
            };
            return View(orders);
        }

        [Route("[action]")]
        [HttpPost]
        [TypeFilter(typeof(CreateOrderActionFilter))]
        public async Task<IActionResult> BuyOrder(BuyOrderRequest orderRequest)
        {
            BuyOrderResponse buyOrderResponse = await _stocksCreateBuyOrderService.CreateBuyOrder(orderRequest);
            return RedirectToAction(nameof(Orders));
        }

        [Route("[action]")]
        [HttpPost]
        [TypeFilter(typeof(CreateOrderActionFilter))]
        public async Task<IActionResult> SellOrder(SellOrderRequest orderRequest)
        {
            SellOrderResponse sellOrderResponse = await _stocksCreateSellOrderService.CreateSellOrder(orderRequest);
            return RedirectToAction(nameof(Orders));
        }

        [Route("[action]")]
        public async Task<IActionResult> OrdersPDF()
        {
            _logger.LogInformation("OrdersPDF action method of TradeController.");

            List<BuyOrderResponse> buyOrderResponses = await _stocksGetBuyOrdersService.GetBuyOrders();
            List<SellOrderResponse> sellOrderResponses = await _stocksGetSellOrdersService.GetSellOrders();
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
