using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerce.Modules.Discounts.Core.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Update_Product_Price : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NewPrice",
                schema: "discounts",
                table: "ProductDiscounts");

            migrationBuilder.AddColumn<decimal>(
                name: "NewPrice_Amount",
                schema: "discounts",
                table: "ProductDiscounts",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NewPrice_Currency",
                schema: "discounts",
                table: "ProductDiscounts",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NewPrice_Amount",
                schema: "discounts",
                table: "ProductDiscounts");

            migrationBuilder.DropColumn(
                name: "NewPrice_Currency",
                schema: "discounts",
                table: "ProductDiscounts");

            migrationBuilder.AddColumn<decimal>(
                name: "NewPrice",
                schema: "discounts",
                table: "ProductDiscounts",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);
        }
    }
}
