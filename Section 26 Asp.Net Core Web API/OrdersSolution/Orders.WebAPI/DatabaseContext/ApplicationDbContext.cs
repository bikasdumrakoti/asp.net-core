using Microsoft.EntityFrameworkCore;
using Orders.WebAPI.Models;

namespace Orders.WebAPI.DatabaseContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
        }

        public ApplicationDbContext()
        {
        }

        public virtual DbSet<Order> Orders { get; set; }

        public virtual DbSet<OrderItem> OrdersItem { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Order>().HasData(new Order()
            {
                OrderId = Guid.Parse("9285C177-172A-4687-8BD1-6AF02EC4B9D0"),
                OrderNumber = "Order_" + DateTime.Now.ToString(),
                CustomerName = "Smith Jones",
                OrderDate = DateTime.Now,
                TotalAmount = 1000.05
            });
            modelBuilder.Entity<Order>().HasData(new Order()
            {
                OrderId = Guid.Parse("A2CD89C1-3580-456E-9A63-7AA9E6A1D03C"),
                OrderNumber = "Order_" + DateTime.Now.ToString(),
                CustomerName = "William Macbeth",
                OrderDate = DateTime.Now,
                TotalAmount = 90.25
            });

            modelBuilder.Entity<OrderItem>().HasData(new OrderItem()
            {
                OrderItemId = Guid.Parse("4BE27E4B-4FE4-4C9F-8CD9-EAA9CE06950A"),
                OrderId = Guid.Parse("9285C177-172A-4687-8BD1-6AF02EC4B9D0"),
                ProductName = "Sports Magazine",
                Quantity = 50,
                UnitPrice = 10.001,
                TotalPrice = 500.05
            });
            modelBuilder.Entity<OrderItem>().HasData(new OrderItem()
            {
                OrderItemId = Guid.Parse("7F1BF628-B5C3-40B7-A807-91A5B06E5F55"),
                OrderId = Guid.Parse("9285C177-172A-4687-8BD1-6AF02EC4B9D0"),
                ProductName = "Soccer Ball",
                Quantity = 5,
                UnitPrice = 100,
                TotalPrice = 500
            });

            modelBuilder.Entity<OrderItem>().HasData(new OrderItem()
            {
                OrderItemId = Guid.Parse("2307C2CE-48A4-49B2-9AAA-CB5DD353F6E6"),
                OrderId = Guid.Parse("A2CD89C1-3580-456E-9A63-7AA9E6A1D03C"),
                ProductName = "Mock Sword",
                Quantity = 1,
                UnitPrice = 50.25,
                TotalPrice = 50.25
            });
            modelBuilder.Entity<OrderItem>().HasData(new OrderItem()
            {
                OrderItemId = Guid.Parse("29ED4BDC-3765-472A-8BE3-2A46DA276FEA"),
                OrderId = Guid.Parse("A2CD89C1-3580-456E-9A63-7AA9E6A1D03C"),
                ProductName = "Fantasy Map",
                Quantity = 1,
                UnitPrice = 40,
                TotalPrice = 40
            });
        }
    }
}
