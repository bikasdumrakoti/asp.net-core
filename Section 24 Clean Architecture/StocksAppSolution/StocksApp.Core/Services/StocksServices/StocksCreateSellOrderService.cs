using Entities;
using RepositoryContracts;
using ServiceContracts.DTO;
using ServiceContracts.StocksServiceContracts;
using Services.Helpers;

namespace Services.StocksServices
{
    public class StocksCreateSellOrderService : IStocksCreateSellOrderService
    {
        private readonly IStocksRepository _stocksRepository;

        public StocksCreateSellOrderService(IStocksRepository stocksRepository)
        {
            _stocksRepository = stocksRepository;
        }

        public async Task<SellOrderResponse> CreateSellOrder(SellOrderRequest? sellOrderRequest)
        {
            if (sellOrderRequest == null)
                throw new ArgumentNullException(nameof(sellOrderRequest));
            ValidationHelper.ModelValidation(sellOrderRequest);
            SellOrder sellOrder = sellOrderRequest.ToSellOrder();
            sellOrder.SellOrderID = Guid.NewGuid();
            return (await _stocksRepository.CreateSellOrder(sellOrder)).ToSellOrderResponse();
        }
    }
}
