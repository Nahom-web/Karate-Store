using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace nhH60Store.Migrations
{
    public partial class AddTablesAndData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    CustomerId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(maxLength: 20, nullable: true),
                    LastName = table.Column<string>(maxLength: 30, nullable: true),
                    Email = table.Column<string>(maxLength: 30, nullable: true),
                    PhoneNumber = table.Column<string>(maxLength: 10, nullable: true),
                    Province = table.Column<string>(maxLength: 2, nullable: false),
                    CreditCard = table.Column<string>(maxLength: 16, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.CustomerId);
                });

            //migrationBuilder.CreateTable(
            //    name: "ProductCategory",
            //    columns: table => new
            //    {
            //        CategoryID = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        ProdCat = table.Column<string>(unicode: false, maxLength: 60, nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_ProductCategory", x => x.CategoryID);
            //    });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    OrderId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(nullable: false),
                    DateCreated = table.Column<DateTime>(type: "date", nullable: false),
                    DateFulfilled = table.Column<DateTime>(type: "date", nullable: false),
                    Total = table.Column<decimal>(type: "numeric(10,2)", nullable: true),
                    Taxes = table.Column<decimal>(type: "numeric(8,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Order_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShoppingCart",
                columns: table => new
                {
                    CartId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(nullable: false),
                    DateCreated = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingCart", x => x.CartId);
                    table.ForeignKey(
                        name: "FK_ShoppingCart_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                });

            //migrationBuilder.CreateTable(
            //    name: "Product",
            //    columns: table => new
            //    {
            //        ProductID = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        ProdCatId = table.Column<int>(nullable: false),
            //        Description = table.Column<string>(unicode: false, maxLength: 80, nullable: true),
            //        Manufacturer = table.Column<string>(unicode: false, maxLength: 80, nullable: true),
            //        Stock = table.Column<int>(nullable: true),
            //        BuyPrice = table.Column<decimal>(type: "numeric(8, 2)", nullable: true),
            //        SellPrice = table.Column<decimal>(type: "numeric(8, 2)", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Product", x => x.ProductID);
            //        table.ForeignKey(
            //            name: "FK_Product_ProductCategory",
            //            column: x => x.ProdCatId,
            //            principalTable: "ProductCategory",
            //            principalColumn: "CategoryID",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            migrationBuilder.CreateTable(
                name: "CartItem",
                columns: table => new
                {
                    CartItemId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CartId = table.Column<int>(nullable: false),
                    ProductId = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    Price = table.Column<decimal>(type: "numeric(8,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItem", x => x.CartItemId);
                    table.ForeignKey(
                        name: "FK_CartItem_ShoppingCart_CartId",
                        column: x => x.CartId,
                        principalTable: "ShoppingCart",
                        principalColumn: "CartId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartItem_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItem",
                columns: table => new
                {
                    OrderItemId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(nullable: false),
                    ProductId = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    Price = table.Column<decimal>(type: "numeric(8,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItem", x => x.OrderItemId);
                    table.ForeignKey(
                        name: "FK_OrderItem_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItem_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "CustomerId", "CreditCard", "Email", "FirstName", "LastName", "PhoneNumber", "Province" },
                values: new object[] { 1, null, "nahomH@gmail.com", "Nahom", "Haile", "8191111111", " " });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "CustomerId", "CreditCard", "Email", "FirstName", "LastName", "PhoneNumber", "Province" },
                values: new object[] { 2, null, "sergeH@gmail.com", "Serge", "Arsenault", "8192222222", " " });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "CustomerId", "CreditCard", "Email", "FirstName", "LastName", "PhoneNumber", "Province" },
                values: new object[] { 3, null, "emmanuelleF@gmail.com", "Emmanuelle", "Fontaine", "8193333333", " " });

            migrationBuilder.InsertData(
                table: "Order",
                columns: new[] { "OrderId", "CustomerId", "DateCreated", "DateFulfilled", "Taxes", "Total" },
                values: new object[] { 1, 1, new DateTime(2021, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 9, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 3.00m, 540.00m });

            migrationBuilder.InsertData(
                table: "ShoppingCart",
                columns: new[] { "CartId", "CustomerId", "DateCreated" },
                values: new object[] { 1, 2, new DateTime(2021, 9, 4, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "CartItem",
                columns: new[] { "CartItemId", "CartId", "Price", "ProductId", "Quantity" },
                values: new object[,]
                {
                    { 1, 1, 180.00m, 24, 1 },
                    { 2, 1, 60.00m, 28, 6 }
                });

            migrationBuilder.InsertData(
                table: "OrderItem",
                columns: new[] { "OrderItemId", "OrderId", "Price", "ProductId", "Quantity" },
                values: new object[,]
                {
                    { 1, 1, 20m, 1, 2 },
                    { 2, 1, 55m, 9, 1 },
                    { 3, 1, 465m, 18, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartItem_CartId",
                table: "CartItem",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItem_ProductId",
                table: "CartItem",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_CustomerId",
                table: "Order",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_OrderId",
                table: "OrderItem",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_ProductId",
                table: "OrderItem",
                column: "ProductId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Product_ProdCatId",
            //    table: "Product",
            //    column: "ProdCatId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCart_CustomerId",
                table: "ShoppingCart",
                column: "CustomerId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartItem");

            migrationBuilder.DropTable(
                name: "OrderItem");

            migrationBuilder.DropTable(
                name: "ShoppingCart");

            migrationBuilder.DropTable(
                name: "Order");

            //migrationBuilder.DropTable(
            //    name: "Product");

            migrationBuilder.DropTable(
                name: "Customer");

            //migrationBuilder.DropTable(
            //    name: "ProductCategory");
        }
    }
}
