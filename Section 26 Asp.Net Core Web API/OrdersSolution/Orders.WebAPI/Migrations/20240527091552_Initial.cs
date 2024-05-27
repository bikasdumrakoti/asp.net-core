using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Orders.WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalAmount = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                });

            migrationBuilder.CreateTable(
                name: "OrdersItem",
                columns: table => new
                {
                    OrderItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<double>(type: "float", nullable: false),
                    TotalPrice = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdersItem", x => x.OrderItemId);
                    table.ForeignKey(
                        name: "FK_OrdersItem_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "OrderId", "CustomerName", "OrderDate", "OrderNumber", "TotalAmount" },
                values: new object[,]
                {
                    { new Guid("9285c177-172a-4687-8bd1-6af02ec4b9d0"), "Smith Jones", new DateTime(2024, 5, 27, 15, 0, 51, 444, DateTimeKind.Local).AddTicks(7485), "Order_5/27/2024 3:00:51 PM", 1000.05 },
                    { new Guid("a2cd89c1-3580-456e-9a63-7aa9e6a1d03c"), "William Macbeth", new DateTime(2024, 5, 27, 15, 0, 51, 444, DateTimeKind.Local).AddTicks(7570), "Order_5/27/2024 3:00:51 PM", 90.25 }
                });

            migrationBuilder.InsertData(
                table: "OrdersItem",
                columns: new[] { "OrderItemId", "OrderId", "ProductName", "Quantity", "TotalPrice", "UnitPrice" },
                values: new object[,]
                {
                    { new Guid("2307c2ce-48a4-49b2-9aaa-cb5dd353f6e6"), new Guid("a2cd89c1-3580-456e-9a63-7aa9e6a1d03c"), "Mock Sword", 1, 50.25, 50.25 },
                    { new Guid("29ed4bdc-3765-472a-8be3-2a46da276fea"), new Guid("a2cd89c1-3580-456e-9a63-7aa9e6a1d03c"), "Fantasy Map", 1, 40.0, 40.0 },
                    { new Guid("4be27e4b-4fe4-4c9f-8cd9-eaa9ce06950a"), new Guid("9285c177-172a-4687-8bd1-6af02ec4b9d0"), "Sports Magazine", 50, 500.04999999999995, 10.000999999999999 },
                    { new Guid("7f1bf628-b5c3-40b7-a807-91a5b06e5f55"), new Guid("9285c177-172a-4687-8bd1-6af02ec4b9d0"), "Soccer Ball", 5, 500.0, 100.0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrdersItem_OrderId",
                table: "OrdersItem",
                column: "OrderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrdersItem");

            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
