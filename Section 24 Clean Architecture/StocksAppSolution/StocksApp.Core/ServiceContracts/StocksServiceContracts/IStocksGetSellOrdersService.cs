using ServiceContracts.DTO;

namespace ServiceContracts.StocksServiceContracts
{
    public interface IStocksGetSellOrdersService
    {
        Task<List<SellOrderResponse>> GetSellOrders();
    }
}
