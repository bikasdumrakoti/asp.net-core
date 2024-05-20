using Exceptions;
using RepositoryContracts;
using ServiceContracts.FinnhubServiceContracts;

namespace Services.FinnhubServices
{
    public class FinnhubCompanyProfileService : IFinnhubCompanyProfileService
    {
        private readonly IFinnhubRepository _finnhubRepository;

        public FinnhubCompanyProfileService(IFinnhubRepository finnhubRepository)
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
    }
}