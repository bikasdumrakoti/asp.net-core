using ServiceContracts.DTOs;

namespace ServiceContracts.StocksServiceContracts
{
    public interface IStocksGetSellOrdersService
    {
        Task<List<SellOrderResponse>> GetSellOrders();
    }
}
