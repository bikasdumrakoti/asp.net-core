using Exceptions;
using RepositoryContracts;
using ServiceContracts;

namespace Services
{
    public class FinnhubService : IFinnhubService
    {
        private readonly IFinnhubRepository _finnhubRepository;

        public FinnhubService(IFinnhubRepository finnhubRepository)
        {
            _finnhubRepository = finnhubRepository;
        }

        public async Task<Dictionary<string, object>?> GetCompanyProfile(string stockSymbol)
        {
            try
            {
                return await _finnhubRepository.GetCompanyProfile(stockSymbol);
            }
            catch (Exception ex)
            {
                throw new FinnhubException("Unable to connect to Finnhub server.", ex);
            }
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

        public async Task<List<Dictionary<string, string>>?> GetStocks()
        {
            try
            {
                return await _finnhubRepository.GetStocks();
            }
            catch (Exception ex)
            {
                throw new FinnhubException("Unable to connect to Finnhub server.", ex);
            }
        }

        public async Task<Dictionary<string, object>?> SearchStocks(string stockSymbolToSearch)
        {
            try
            {
                return await _finnhubRepository.SearchStocks(stockSymbolToSearch);
            }
            catch (Exception ex)
            {
                throw new FinnhubException("Unable to connect to Finnhub server.", ex);
            }
        }
    }
}