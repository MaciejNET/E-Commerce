using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerce.Modules.Returns.Infrastructure.EF.Migrations
{
    /// <inheritdoc />
    public partial class Returns_Module_Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "returns");

            migrationBuilder.CreateTable(
                name: "Orders",
                schema: "returns",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderPlaced = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompletionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderProducts",
                schema: "returns",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Sku = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsReturn = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderProducts_Orders_OrderId",
                        column: x => x.OrderId,
                        principalSchema: "returns",
                        principalTable: "Orders",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Returns",
                schema: "returns",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OrderProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false),
                    ReturnStatus = table.Column<int>(type: "int", nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Returns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Returns_OrderProducts_OrderProductId",
                        column: x => x.OrderProductId,
                        principalSchema: "returns",
                        principalTable: "OrderProducts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Returns_Orders_OrderId",
                        column: x => x.OrderId,
                        principalSchema: "returns",
                        principalTable: "Orders",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderProducts_OrderId",
                schema: "returns",
                table: "OrderProducts",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Returns_OrderId",
                schema: "returns",
                table: "Returns",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Returns_OrderProductId",
                schema: "returns",
                table: "Returns",
                column: "OrderProductId",
                unique: true,
                filter: "[OrderProductId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Returns",
                schema: "returns");

            migrationBuilder.DropTable(
                name: "OrderProducts",
                schema: "returns");

            migrationBuilder.DropTable(
                name: "Orders",
                schema: "returns");
        }
    }
}
