using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerce.Modules.Orders.Infrastructure.EF.Migrations
{
    /// <inheritdoc />
    public partial class Orders_Module_Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "orders");

            migrationBuilder.CreateTable(
                name: "Carts",
                schema: "orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Version = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Discounts",
                schema: "orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Percentage = table.Column<int>(type: "int", nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                schema: "orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Shipment_City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Shipment_StreetName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Shipment_StreetNumber = table.Column<int>(type: "int", nullable: true),
                    Shipment_ReceiverFullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentMethod = table.Column<int>(type: "int", nullable: false),
                    PlaceDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CompletionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Version = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                schema: "orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sku = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    StandardPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    DiscountedPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    StockQuantity = table.Column<int>(type: "int", nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CheckoutCarts",
                schema: "orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Payment = table.Column<int>(type: "int", nullable: false),
                    Shipment_City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Shipment_StreetName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Shipment_StreetNumber = table.Column<int>(type: "int", nullable: true),
                    Shipment_ReceiverFullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiscountId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Version = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckoutCarts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CheckoutCarts_Discounts_DiscountId",
                        column: x => x.DiscountId,
                        principalSchema: "orders",
                        principalTable: "Discounts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OrderLine",
                schema: "orders",
                columns: table => new
                {
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderLineNumber = table.Column<int>(type: "int", nullable: false),
                    Sku = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderLine", x => new { x.OrderId, x.Id });
                    table.ForeignKey(
                        name: "FK_OrderLine_Orders_OrderId",
                        column: x => x.OrderId,
                        principalSchema: "orders",
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CartItems",
                schema: "orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CartId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartItems_Carts_CartId",
                        column: x => x.CartId,
                        principalSchema: "orders",
                        principalTable: "Carts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CartItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "orders",
                        principalTable: "Products",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DiscountsProducts",
                schema: "orders",
                columns: table => new
                {
                    DiscountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscountsProducts", x => new { x.DiscountId, x.ProductsId });
                    table.ForeignKey(
                        name: "FK_DiscountsProducts_Discounts_DiscountId",
                        column: x => x.DiscountId,
                        principalSchema: "orders",
                        principalTable: "Discounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DiscountsProducts_Products_ProductsId",
                        column: x => x.ProductsId,
                        principalSchema: "orders",
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CheckoutsCartItems",
                schema: "orders",
                columns: table => new
                {
                    CheckoutCartId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ItemsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckoutsCartItems", x => new { x.CheckoutCartId, x.ItemsId });
                    table.ForeignKey(
                        name: "FK_CheckoutsCartItems_CartItems_ItemsId",
                        column: x => x.ItemsId,
                        principalSchema: "orders",
                        principalTable: "CartItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CheckoutsCartItems_CheckoutCarts_CheckoutCartId",
                        column: x => x.CheckoutCartId,
                        principalSchema: "orders",
                        principalTable: "CheckoutCarts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_CartId",
                schema: "orders",
                table: "CartItems",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_ProductId",
                schema: "orders",
                table: "CartItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_UserId",
                schema: "orders",
                table: "Carts",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CheckoutCarts_DiscountId",
                schema: "orders",
                table: "CheckoutCarts",
                column: "DiscountId");

            migrationBuilder.CreateIndex(
                name: "IX_CheckoutCarts_UserId",
                schema: "orders",
                table: "CheckoutCarts",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CheckoutsCartItems_ItemsId",
                schema: "orders",
                table: "CheckoutsCartItems",
                column: "ItemsId");

            migrationBuilder.CreateIndex(
                name: "IX_Discounts_Code",
                schema: "orders",
                table: "Discounts",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_DiscountsProducts_ProductsId",
                schema: "orders",
                table: "DiscountsProducts",
                column: "ProductsId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_Sku",
                schema: "orders",
                table: "Products",
                column: "Sku",
                unique: true,
                filter: "[Sku] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CheckoutsCartItems",
                schema: "orders");

            migrationBuilder.DropTable(
                name: "DiscountsProducts",
                schema: "orders");

            migrationBuilder.DropTable(
                name: "OrderLine",
                schema: "orders");

            migrationBuilder.DropTable(
                name: "CartItems",
                schema: "orders");

            migrationBuilder.DropTable(
                name: "CheckoutCarts",
                schema: "orders");

            migrationBuilder.DropTable(
                name: "Orders",
                schema: "orders");

            migrationBuilder.DropTable(
                name: "Carts",
                schema: "orders");

            migrationBuilder.DropTable(
                name: "Products",
                schema: "orders");

            migrationBuilder.DropTable(
                name: "Discounts",
                schema: "orders");
        }
    }
}
