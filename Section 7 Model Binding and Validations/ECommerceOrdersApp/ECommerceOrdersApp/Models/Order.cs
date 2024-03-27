using ECommerceOrdersApp.CustomValidators;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ECommerceOrdersApp.Models
{
    public class Order
    {
        [BindNever]
        public int? OrderNo { get; set; }

        [DisplayName("Order Date")]
        [Required(ErrorMessage = "{0} is required.")]
        [DataType(DataType.DateTime)]
        [MinimumYearValidator("2000-01-01", ErrorMessage = "{0} should be greater than or equal to {1}.")]
        public DateTime? OrderDate { get; set; }

        [DisplayName("Invoice Price")]
        [Required(ErrorMessage = "{0} is required.")]
        [InvoicePriceValidator("Products", ErrorMessage = "{0} should match with the total cost of all products.")]
        public double? InvoicePrice { get; set; }

        [MinimumProductValidator(1, ErrorMessage = "At least {0} product is required.")]
        public List<Product> Products { get; set; } = new List<Product>();
    }
}
