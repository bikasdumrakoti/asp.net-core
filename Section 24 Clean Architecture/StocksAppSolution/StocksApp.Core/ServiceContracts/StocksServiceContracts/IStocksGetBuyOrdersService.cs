using ServiceContracts.DTO;

namespace ServiceContracts.StocksServiceContracts
{
    public interface IStocksGetBuyOrdersService
    {
        Task<List<BuyOrderResponse>> GetBuyOrders();
    }
}
