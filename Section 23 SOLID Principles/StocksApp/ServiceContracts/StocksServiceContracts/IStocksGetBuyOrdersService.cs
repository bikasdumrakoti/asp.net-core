using ServiceContracts.DTOs;

namespace ServiceContracts.StocksServiceContracts
{
    public interface IStocksGetBuyOrdersService
    {
        Task<List<BuyOrderResponse>> GetBuyOrders();
    }
}
