using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace E_Commerce.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration_SeedingData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Devices and gadgets", "Electronics" },
                    { 2, "Printed and digital books", "Books" },
                    { 3, "Apparel and accessories", "Clothing" },
                    { 4, "Kitchen and home utilities", "Home Appliances" },
                    { 5, "Toys and games for kids", "Toys" }
                });

            migrationBuilder.InsertData(
                table: "Order",
                columns: new[] { "Id", "CustomerName", "OrderDate" },
                values: new object[,]
                {
                    { 1, "Alice Johnson", new DateTime(2025, 1, 19, 21, 30, 18, 6, DateTimeKind.Local).AddTicks(1490) },
                    { 2, "Bob Smith", new DateTime(2025, 1, 21, 21, 30, 18, 6, DateTimeKind.Local).AddTicks(1516) },
                    { 3, "Charlie Davis", new DateTime(2025, 1, 24, 21, 30, 18, 6, DateTimeKind.Local).AddTicks(1518) },
                    { 4, "David Wilson", new DateTime(2025, 1, 25, 21, 30, 18, 6, DateTimeKind.Local).AddTicks(1520) },
                    { 5, "Eva Roberts", new DateTime(2025, 1, 27, 21, 30, 18, 6, DateTimeKind.Local).AddTicks(1522) },
                    { 6, "Fay Green", new DateTime(2025, 1, 29, 21, 30, 18, 6, DateTimeKind.Local).AddTicks(1524) },
                    { 7, "George Turner", new DateTime(2025, 1, 31, 21, 30, 18, 6, DateTimeKind.Local).AddTicks(1527) },
                    { 8, "Helen Adams", new DateTime(2025, 2, 1, 21, 30, 18, 6, DateTimeKind.Local).AddTicks(1528) },
                    { 9, "Ivy Lee", new DateTime(2025, 2, 2, 21, 30, 18, 6, DateTimeKind.Local).AddTicks(1531) },
                    { 10, "James Clark", new DateTime(2025, 2, 3, 21, 30, 18, 6, DateTimeKind.Local).AddTicks(1532) }
                });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "Id", "Description", "Name", "Price", "StockQuantity" },
                values: new object[,]
                {
                    { 1, "Latest model smartphone", "Smartphone", 699.99m, 50 },
                    { 2, "High performance laptop", "Laptop", 1299.99m, 30 },
                    { 3, "Bestselling fiction novel", "Fiction Novel", 19.99m, 100 },
                    { 4, "100% cotton t-shirt", "T-Shirt", 15.99m, 200 },
                    { 5, "High-speed kitchen blender", "Blender", 89.99m, 40 }
                });

            migrationBuilder.InsertData(
                table: "OrderItem",
                columns: new[] { "Id", "OrderId", "ProductId", "Quantity", "UnitPrice" },
                values: new object[,]
                {
                    { 1, 1, 1, 1, 699.99m },
                    { 2, 1, 3, 2, 19.99m },
                    { 3, 2, 2, 1, 1299.99m },
                    { 4, 2, 4, 3, 15.99m },
                    { 5, 3, 5, 1, 89.99m },
                    { 6, 4, 1, 1, 699.99m },
                    { 7, 5, 2, 2, 1299.99m },
                    { 8, 6, 4, 4, 15.99m },
                    { 9, 7, 3, 5, 19.99m },
                    { 10, 8, 5, 3, 89.99m }
                });

            migrationBuilder.InsertData(
                table: "ProductCategory",
                columns: new[] { "CategoryId", "ProductId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 2, 3 },
                    { 3, 4 },
                    { 4, 5 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Order",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Order",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "OrderItem",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "OrderItem",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "OrderItem",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "OrderItem",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "OrderItem",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "OrderItem",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "OrderItem",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "OrderItem",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "OrderItem",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "OrderItem",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "ProductCategory",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "ProductCategory",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "ProductCategory",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 2, 3 });

            migrationBuilder.DeleteData(
                table: "ProductCategory",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 3, 4 });

            migrationBuilder.DeleteData(
                table: "ProductCategory",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 4, 5 });

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Order",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Order",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Order",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Order",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Order",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Order",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Order",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Order",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
