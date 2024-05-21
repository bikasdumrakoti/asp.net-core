using Entities;
using ServiceContracts.CustomValidators;
using System.ComponentModel.DataAnnotations;

namespace ServiceContracts.DTO
{
    public class SellOrderRequest : IOrderRequest
    {
        [Required(ErrorMessage = "{0} is required !")]
        [Display(Name = "Stock Symbol")]
        public string? StockSymbol { get; set; }
        [Required(ErrorMessage = "{0} is required !")]
        [Display(Name = "Stock Name")]
        public string? StockName { get; set; }
        [MinimumYearValidator]
        public DateTime? DateAndTimeOfOrder { get; set; }
        [Range(1, 100000, ErrorMessage = "{0} should be between {1} and {2} !")]
        public int? Quantity { get; set; }
        [Range(1, 10000, ErrorMessage = "{0} should be between {1} and {2} !")]
        public double? Price { get; set; }

        public SellOrder ToSellOrder()
        {
            return new SellOrder()
            {
                StockSymbol = StockSymbol,
                StockName = StockName,
                DateAndTimeOfOrder = DateAndTimeOfOrder,
                Quantity = Quantity,
                Price = Price
            };
        }
    }
}
