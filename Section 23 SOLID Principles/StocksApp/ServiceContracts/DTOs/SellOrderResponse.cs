using Entities;

namespace ServiceContracts.DTOs
{
    public class SellOrderResponse
    {
        public Guid SellOrderID { get; set; }
        public string? StockSymbol { get; set; }
        public string? StockName { get; set; }
        public DateTime? DateAndTimeOfOrder { get; set; }
        public int? Quantity { get; set; }
        public double? Price { get; set; }
        public double? TradeAmount { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (obj.GetType() != typeof(SellOrderResponse)) return false;
            SellOrderResponse sellOrderResponse = (SellOrderResponse)obj;
            return SellOrderID == sellOrderResponse.SellOrderID && StockSymbol == sellOrderResponse.StockSymbol && StockName == sellOrderResponse.StockName && DateAndTimeOfOrder == sellOrderResponse.DateAndTimeOfOrder && Quantity == sellOrderResponse.Quantity && Price == sellOrderResponse.Price;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return $"Sell Order ID: {SellOrderID}, Stock Symbol: {StockSymbol}, Stock Name: {StockName}, Date and Time of Order: {DateAndTimeOfOrder}, Quantity: {Quantity}, Price: {Price}, Trade Amount: {Quantity * Price}";
        }
    }

    public static class SellOrderExtensions
    {
        public static SellOrderResponse ToSellOrderResponse(this SellOrder sellOrder)
        {
            return new SellOrderResponse()
            {
                SellOrderID = sellOrder.SellOrderID,
                StockSymbol = sellOrder.StockSymbol,
                StockName = sellOrder.StockName,
                DateAndTimeOfOrder = sellOrder.DateAndTimeOfOrder,
                Quantity = sellOrder.Quantity,
                Price = sellOrder.Price,
                TradeAmount = sellOrder.Quantity * sellOrder.Price
            };
        }
    }
}
