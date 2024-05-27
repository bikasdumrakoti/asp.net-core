using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Orders.WebAPI.Models
{
    public class OrderItem
    {
        [Key]
        public Guid OrderItemId { get; set; }

        [Required(ErrorMessage = "Order Id can't be null")]
        public Guid OrderId { get; set; }

        [Required(ErrorMessage = "Product name can't be blank")]
        [StringLength(50)]
        public string? ProductName { get; set; }

        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }

        [Range(0, double.MaxValue)]
        public double UnitPrice { get; set; }

        public double TotalPrice
        {
            get
            {
                return Quantity * UnitPrice;
            }
            set
            {

            }
        }

        [ForeignKey(nameof(OrderId))]
        public virtual Order? Order { get; set; }
    }
}
