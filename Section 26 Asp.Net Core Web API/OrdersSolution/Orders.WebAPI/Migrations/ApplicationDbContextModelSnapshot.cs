﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Orders.WebAPI.DatabaseContext;

#nullable disable

namespace Orders.WebAPI.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.18")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Orders.WebAPI.Models.Order", b =>
                {
                    b.Property<Guid>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CustomerName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("OrderNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("TotalAmount")
                        .HasColumnType("float");

                    b.HasKey("OrderId");

                    b.ToTable("Orders");

                    b.HasData(
                        new
                        {
                            OrderId = new Guid("9285c177-172a-4687-8bd1-6af02ec4b9d0"),
                            CustomerName = "Smith Jones",
                            OrderDate = new DateTime(2024, 5, 27, 15, 0, 51, 444, DateTimeKind.Local).AddTicks(7485),
                            OrderNumber = "Order_5/27/2024 3:00:51 PM",
                            TotalAmount = 1000.05
                        },
                        new
                        {
                            OrderId = new Guid("a2cd89c1-3580-456e-9a63-7aa9e6a1d03c"),
                            CustomerName = "William Macbeth",
                            OrderDate = new DateTime(2024, 5, 27, 15, 0, 51, 444, DateTimeKind.Local).AddTicks(7570),
                            OrderNumber = "Order_5/27/2024 3:00:51 PM",
                            TotalAmount = 90.25
                        });
                });

            modelBuilder.Entity("Orders.WebAPI.Models.OrderItem", b =>
                {
                    b.Property<Guid>("OrderItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<double>("TotalPrice")
                        .HasColumnType("float");

                    b.Property<double>("UnitPrice")
                        .HasColumnType("float");

                    b.HasKey("OrderItemId");

                    b.HasIndex("OrderId");

                    b.ToTable("OrdersItem");

                    b.HasData(
                        new
                        {
                            OrderItemId = new Guid("4be27e4b-4fe4-4c9f-8cd9-eaa9ce06950a"),
                            OrderId = new Guid("9285c177-172a-4687-8bd1-6af02ec4b9d0"),
                            ProductName = "Sports Magazine",
                            Quantity = 50,
                            TotalPrice = 500.04999999999995,
                            UnitPrice = 10.000999999999999
                        },
                        new
                        {
                            OrderItemId = new Guid("7f1bf628-b5c3-40b7-a807-91a5b06e5f55"),
                            OrderId = new Guid("9285c177-172a-4687-8bd1-6af02ec4b9d0"),
                            ProductName = "Soccer Ball",
                            Quantity = 5,
                            TotalPrice = 500.0,
                            UnitPrice = 100.0
                        },
                        new
                        {
                            OrderItemId = new Guid("2307c2ce-48a4-49b2-9aaa-cb5dd353f6e6"),
                            OrderId = new Guid("a2cd89c1-3580-456e-9a63-7aa9e6a1d03c"),
                            ProductName = "Mock Sword",
                            Quantity = 1,
                            TotalPrice = 50.25,
                            UnitPrice = 50.25
                        },
                        new
                        {
                            OrderItemId = new Guid("29ed4bdc-3765-472a-8be3-2a46da276fea"),
                            OrderId = new Guid("a2cd89c1-3580-456e-9a63-7aa9e6a1d03c"),
                            ProductName = "Fantasy Map",
                            Quantity = 1,
                            TotalPrice = 40.0,
                            UnitPrice = 40.0
                        });
                });

            modelBuilder.Entity("Orders.WebAPI.Models.OrderItem", b =>
                {
                    b.HasOne("Orders.WebAPI.Models.Order", "Order")
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");
                });

            modelBuilder.Entity("Orders.WebAPI.Models.Order", b =>
                {
                    b.Navigation("OrderItems");
                });
#pragma warning restore 612, 618
        }
    }
}
