using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerce.Modules.Discounts.Core.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Discount_Code_Configuration_Updated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Code",
                schema: "discounts",
                table: "DiscountCodes",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DiscountCodes_Code",
                schema: "discounts",
                table: "DiscountCodes",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_DiscountCodes_Code",
                schema: "discounts",
                table: "DiscountCodes");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                schema: "discounts",
                table: "DiscountCodes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }
    }
}
