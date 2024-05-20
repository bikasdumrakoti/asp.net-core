using RepositoryContracts;
using ServiceContracts.DTOs;
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
