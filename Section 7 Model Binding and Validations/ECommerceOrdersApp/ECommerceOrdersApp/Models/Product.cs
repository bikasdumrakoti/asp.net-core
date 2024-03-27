using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ECommerceOrdersApp.Models
{
    public class Product
    {
        [DisplayName("Product Code")]
        [Required(ErrorMessage = "{0} is required.")]
        public int? ProductCode { get; set; }

        [Required(ErrorMessage = "{0} is required.")]
        public double? Price { get; set; }

        [Required(ErrorMessage = "{0} is required.")]
        public int? Quantity { get; set; }
    }
}
