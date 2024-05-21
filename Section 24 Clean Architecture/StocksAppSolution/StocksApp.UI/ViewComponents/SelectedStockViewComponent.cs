using Microsoft.AspNetCore.Mvc;
using ServiceContracts.FinnhubServiceContracts;

namespace StocksApp.ViewComponents
{
    public class SelectedStockViewComponent : ViewComponent
    {
        private readonly IFinnhubCompanyProfileService _finnhubCompanyProfileService;
        private readonly IFinnhubStockPriceQuoteService _finnhubStockPriceQuoteService;

        private readonly IConfiguration _configuration;

        public SelectedStockViewComponent(IFinnhubCompanyProfileService finnhubCompanyProfileService, IFinnhubStockPriceQuoteService finnhubStockPriceQuoteService, IConfiguration configuration)
        {
            _finnhubCompanyProfileService = finnhubCompanyProfileService;
            _finnhubStockPriceQuoteService = finnhubStockPriceQuoteService;

            _configuration = configuration;
        }

        public async Task<IViewComponentResult> InvokeAsync(string? stockSymbol)
        {
            Dictionary<string, object>? companyProfileKeyValuePairs = null;
            if (stockSymbol is not null)
            {
                companyProfileKeyValuePairs = await _finnhubCompanyProfileService.GetCompanyProfile(stockSymbol);
                Dictionary<string, object>? stockPriceQuoteKeyValuePairs = await _finnhubStockPriceQuoteService.GetStockPriceQuote(stockSymbol);
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
