using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerce.Modules.Orders.Infrastructure.EF.Migrations
{
    /// <inheritdoc />
    public partial class Add_Checkout_Cart_Item_And_Update_Product_Price : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_CheckoutCarts_CheckoutCartId",
                schema: "orders",
                table: "CartItems");

            migrationBuilder.DropIndex(
                name: "IX_CartItems_CheckoutCartId",
                schema: "orders",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "StandardPrice",
                schema: "orders",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CheckoutCartId",
                schema: "orders",
                table: "CartItems");

            migrationBuilder.RenameColumn(
                name: "DiscountedPrice",
                schema: "orders",
                table: "Products",
                newName: "StandardPrice_Amount");

            migrationBuilder.AddColumn<decimal>(
                name: "DiscountedPrice_Amount",
                schema: "orders",
                table: "Products",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DiscountedPrice_Currency",
                schema: "orders",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StandardPrice_Currency",
                schema: "orders",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Currency",
                schema: "orders",
                table: "OrderLine",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Currency",
                schema: "orders",
                table: "CheckoutCarts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "PreferredCurrency",
                schema: "orders",
                table: "Carts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "CheckoutCartItems",
                schema: "orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price_Amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    Price_Currency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiscountedPrice_Amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    DiscountedPrice_Currency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CheckoutCartId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckoutCartItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CheckoutCartItems_CheckoutCarts_CheckoutCartId",
                        column: x => x.CheckoutCartId,
                        principalSchema: "orders",
                        principalTable: "CheckoutCarts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CheckoutCartItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "orders",
                        principalTable: "Products",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CheckoutCartItems_CheckoutCartId",
                schema: "orders",
                table: "CheckoutCartItems",
                column: "CheckoutCartId");

            migrationBuilder.CreateIndex(
                name: "IX_CheckoutCartItems_ProductId",
                schema: "orders",
                table: "CheckoutCartItems",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CheckoutCartItems",
                schema: "orders");

            migrationBuilder.DropColumn(
                name: "DiscountedPrice_Amount",
                schema: "orders",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "DiscountedPrice_Currency",
                schema: "orders",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "StandardPrice_Currency",
                schema: "orders",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Currency",
                schema: "orders",
                table: "OrderLine");

            migrationBuilder.DropColumn(
                name: "Currency",
                schema: "orders",
                table: "CheckoutCarts");

            migrationBuilder.DropColumn(
                name: "PreferredCurrency",
                schema: "orders",
                table: "Carts");

            migrationBuilder.RenameColumn(
                name: "StandardPrice_Amount",
                schema: "orders",
                table: "Products",
                newName: "DiscountedPrice");

            migrationBuilder.AddColumn<decimal>(
                name: "StandardPrice",
                schema: "orders",
                table: "Products",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<Guid>(
                name: "CheckoutCartId",
                schema: "orders",
                table: "CartItems",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_CheckoutCartId",
                schema: "orders",
                table: "CartItems",
                column: "CheckoutCartId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_CheckoutCarts_CheckoutCartId",
                schema: "orders",
                table: "CartItems",
                column: "CheckoutCartId",
                principalSchema: "orders",
                principalTable: "CheckoutCarts",
                principalColumn: "Id");
        }
    }
}
