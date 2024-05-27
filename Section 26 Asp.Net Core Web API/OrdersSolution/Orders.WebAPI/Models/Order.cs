using System.ComponentModel.DataAnnotations;

namespace Orders.WebAPI.Models
{
    public class Order
    {
        //public Order()
        //{
        //    OrderItems = new List<OrderItem>();
        //}

        [Key]
        public Guid OrderId { get; set; }

        public string? OrderNumber { get; set; }

        [Required(ErrorMessage = "Customer name can't be blank")]
        [StringLength(50)]
        public string? CustomerName { get; set; }

        [Required(ErrorMessage = "Order date can't be blank")]
        public DateTime OrderDate { get; set; }

        [Range(0, double.MaxValue)]
        public double TotalAmount { get; set; }

        //public virtual ICollection<OrderItem>? OrderItems { get; set; }
    }
}
