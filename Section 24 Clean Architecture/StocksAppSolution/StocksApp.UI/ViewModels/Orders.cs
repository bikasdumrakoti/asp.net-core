using ServiceContracts.DTO;

namespace StocksApp.ViewModels
{
    public class Orders
    {
        public Orders()
        {
            BuyOrders = new List<BuyOrderResponse>();
            SellOrders = new List<SellOrderResponse>();
        }

        public List<BuyOrderResponse> BuyOrders { get; set; }
        public List<SellOrderResponse> SellOrders { get; set; }
    }
}
