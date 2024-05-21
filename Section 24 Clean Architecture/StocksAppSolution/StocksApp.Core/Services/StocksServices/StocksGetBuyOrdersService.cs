using RepositoryContracts;
using ServiceContracts.DTO;
using ServiceContracts.StocksServiceContracts;

namespace Services.StocksServices
{
    public class StocksGetBuyOrdersService : IStocksGetBuyOrdersService
    {
        private readonly IStocksRepository _stocksRepository;

        public StocksGetBuyOrdersService(IStocksRepository stocksRepository)
        {
            _stocksRepository = stocksRepository;
        }

        public async Task<List<BuyOrderResponse>> GetBuyOrders()
        {
            return (await _stocksRepository.GetBuyOrders()).Select(temp => temp.ToBuyOrderResponse()).ToList();
        }
    }
}
