using Microsoft.AspNetCore.Mvc;
using ServiceContracts;

namespace StocksApp.ViewComponents
{
    public class SelectedStockViewComponent : ViewComponent
    {
        private readonly IFinnhubService _finnhubService;
        private readonly IConfiguration _configuration;

        public SelectedStockViewComponent(IFinnhubService finnhubService, IConfiguration configuration)
        {
            _finnhubService = finnhubService;
            _configuration = configuration;
        }

        public async Task<IViewComponentResult> InvokeAsync(string? stockSymbol)
        {
            Dictionary<string, object>? companyProfileKeyValuePairs = null;
            if (stockSymbol is not null)
            {
                companyProfileKeyValuePairs = await _finnhubService.GetCompanyProfile(stockSymbol);
                Dictionary<string, object>? stockPriceQuoteKeyValuePairs = await _finnhubService.GetStockPriceQuote(stockSymbol);
                if (companyProfileKeyValuePairs is not null && stockPriceQuoteKeyValuePairs is not null)
                {
                    companyProfileKeyValuePairs.Add("price", stockPriceQuoteKeyValuePairs["c"]);
                }
            }
            if (companyProfileKeyValuePairs is not null && companyProfileKeyValuePairs.ContainsKey("logo"))
            {
                ViewBag.FinnhubToken = _configuration["FinnhubToken"];
                return View(companyProfileKeyValuePairs);
            }
            else
            {
                return Content("");
            }
        }
    }
}
