using ServiceContracts.DTOs;

namespace ServiceContracts.StocksServiceContracts
{
    public interface IStocksCreateSellOrderService
    {
        Task<SellOrderResponse> CreateSellOrder(SellOrderRequest? sellOrderRequest);
    }
}
