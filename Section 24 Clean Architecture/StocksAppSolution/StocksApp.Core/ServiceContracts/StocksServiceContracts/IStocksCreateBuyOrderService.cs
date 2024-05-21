using ServiceContracts.DTO;

namespace ServiceContracts.StocksServiceContracts
{
    public interface IStocksCreateBuyOrderService
    {
        Task<BuyOrderResponse> CreateBuyOrder(BuyOrderRequest? buyOrderRequest);
    }
}
