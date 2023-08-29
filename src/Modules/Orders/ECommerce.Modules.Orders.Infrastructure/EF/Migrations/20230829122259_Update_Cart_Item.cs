using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerce.Modules.Orders.Infrastructure.EF.Migrations
{
    /// <inheritdoc />
    public partial class Update_Cart_Item : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CheckoutsCartItems",
                schema: "orders");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "CheckoutCartId",
                schema: "orders",
                table: "CartItems");

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
                name: "IX_CheckoutsCartItems_ItemsId",
                schema: "orders",
                table: "CheckoutsCartItems",
                column: "ItemsId");
        }
    }
}
