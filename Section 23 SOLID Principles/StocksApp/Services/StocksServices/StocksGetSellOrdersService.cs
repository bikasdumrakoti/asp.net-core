using RepositoryContracts;
using ServiceContracts.DTOs;
using ServiceContracts.StocksServiceContracts;

namespace Services.StocksServices
{
    public class StocksGetSellOrdersService : IStocksGetSellOrdersService
    {
        private readonly IStocksRepository _stocksRepository;

        public StocksGetSellOrdersService(IStocksRepository stocksRepository)
        {
            _stocksRepository = stocksRepository;
        }

        public async Task<List<SellOrderResponse>> GetSellOrders()
        {
            return (await _stocksRepository.GetSellOrders()).Select(temp => temp.ToSellOrderResponse()).ToList();
        }
    }
}
