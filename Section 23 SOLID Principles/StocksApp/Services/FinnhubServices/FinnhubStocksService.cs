using Exceptions;
using RepositoryContracts;
using ServiceContracts.FinnhubServiceContracts;

namespace Services.FinnhubServices
{
    public class FinnhubStocksService : IFinnhubStocksService
    {
        private readonly IFinnhubRepository _finnhubRepository;

        public FinnhubStocksService(IFinnhubRepository finnhubRepository)
        {
            _finnhubRepository = finnhubRepository;
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
    }
}