using Entities;
using RepositoryContracts;
using ServiceContracts.DTOs;
using ServiceContracts.StocksServiceContracts;
using Services.Helpers;

namespace Services.StocksServices
{
    public class StocksCreateBuyOrderService : IStocksCreateBuyOrderService
    {
        private readonly IStocksRepository _stocksRepository;

        public StocksCreateBuyOrderService(IStocksRepository stocksRepository)
        {
            _stocksRepository = stocksRepository;
        }

        public async Task<BuyOrderResponse> CreateBuyOrder(BuyOrderRequest? buyOrderRequest)
        {
            if (buyOrderRequest == null)
                throw new ArgumentNullException(nameof(buyOrderRequest));
            ValidationHelper.ModelValidation(buyOrderRequest);
            BuyOrder buyOrder = buyOrderRequest.ToBuyOrder();
            buyOrder.BuyOrderID = Guid.NewGuid();
            return (await _stocksRepository.CreateBuyOrder(buyOrder)).ToBuyOrderResponse();
        }
    }
}
