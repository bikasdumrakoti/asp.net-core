using Exceptions;
using RepositoryContracts;
using ServiceContracts.FinnhubServiceContracts;

namespace Services.FinnhubServices
{
    public class FinnhubStockPriceQuoteService : IFinnhubStockPriceQuoteService
    {
        private readonly IFinnhubRepository _finnhubRepository;

        public FinnhubStockPriceQuoteService(IFinnhubRepository finnhubRepository)
        {
            _finnhubRepository = finnhubRepository;
        }

        public async Task<Dictionary<string, object>?> GetStockPriceQuote(string stockSymbol)
        {
            try
            {
                return await _finnhubRepository.GetStockPriceQuote(stockSymbol);
            }
            catch (Exception ex)
            {
                throw new FinnhubException("Unable to connect to Finnhub server.", ex);
            }
        }
    }
}