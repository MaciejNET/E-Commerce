using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerce.Modules.Products.Core.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Update_Product_Price : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StandardPrice",
                schema: "products",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "DiscountedPrice",
                schema: "products",
                table: "Products",
                newName: "StandardPrice_Amount");

            migrationBuilder.AddColumn<decimal>(
                name: "DiscountedPrice_Amount",
                schema: "products",
                table: "Products",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DiscountedPrice_Currency",
                schema: "products",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StandardPrice_Currency",
                schema: "products",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiscountedPrice_Amount",
                schema: "products",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "DiscountedPrice_Currency",
                schema: "products",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "StandardPrice_Currency",
                schema: "products",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "StandardPrice_Amount",
                schema: "products",
                table: "Products",
                newName: "DiscountedPrice");

            migrationBuilder.AddColumn<decimal>(
                name: "StandardPrice",
                schema: "products",
                table: "Products",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);
        }
    }
}
