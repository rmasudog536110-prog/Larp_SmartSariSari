using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Larp.infrastructure.Migrations.TenantErpDb
{
    /// <inheritdoc />
    public partial class AddProcurementAndInventoryAudit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "Sales",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Purchases",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CreatedByEmployeeId",
                table: "Purchases",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Purchases",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedByEmployeeId",
                table: "Purchases",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedByEmployeeId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Products",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedByEmployeeId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Inventory",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CreatedByEmployeeId",
                table: "Inventory",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedByEmployeeId",
                table: "Inventory",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "StockMovements",
                columns: table => new
                {
                    StockMovementId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    MovementType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    QuantityBefore = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    QuantityAfter = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PurchaseId = table.Column<int>(type: "int", nullable: true),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    MovementDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockMovements", x => x.StockMovementId);
                    table.ForeignKey(
                        name: "FK_StockMovements_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StockMovements_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StockMovements_Purchases_PurchaseId",
                        column: x => x.PurchaseId,
                        principalTable: "Purchases",
                        principalColumn: "PurchaseId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sales_EmployeeId",
                table: "Sales",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_CreatedByEmployeeId",
                table: "Purchases",
                column: "CreatedByEmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_UpdatedByEmployeeId",
                table: "Purchases",
                column: "UpdatedByEmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CreatedByEmployeeId",
                table: "Products",
                column: "CreatedByEmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_UpdatedByEmployeeId",
                table: "Products",
                column: "UpdatedByEmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Inventory_CreatedByEmployeeId",
                table: "Inventory",
                column: "CreatedByEmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Inventory_UpdatedByEmployeeId",
                table: "Inventory",
                column: "UpdatedByEmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_StockMovements_EmployeeId",
                table: "StockMovements",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_StockMovements_ProductId",
                table: "StockMovements",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_StockMovements_PurchaseId",
                table: "StockMovements",
                column: "PurchaseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Inventory_Employees_CreatedByEmployeeId",
                table: "Inventory",
                column: "CreatedByEmployeeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Inventory_Employees_UpdatedByEmployeeId",
                table: "Inventory",
                column: "UpdatedByEmployeeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Employees_CreatedByEmployeeId",
                table: "Products",
                column: "CreatedByEmployeeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Employees_UpdatedByEmployeeId",
                table: "Products",
                column: "UpdatedByEmployeeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_Employees_CreatedByEmployeeId",
                table: "Purchases",
                column: "CreatedByEmployeeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_Employees_UpdatedByEmployeeId",
                table: "Purchases",
                column: "UpdatedByEmployeeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_Employees_EmployeeId",
                table: "Sales",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inventory_Employees_CreatedByEmployeeId",
                table: "Inventory");

            migrationBuilder.DropForeignKey(
                name: "FK_Inventory_Employees_UpdatedByEmployeeId",
                table: "Inventory");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Employees_CreatedByEmployeeId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Employees_UpdatedByEmployeeId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_Employees_CreatedByEmployeeId",
                table: "Purchases");

            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_Employees_UpdatedByEmployeeId",
                table: "Purchases");

            migrationBuilder.DropForeignKey(
                name: "FK_Sales_Employees_EmployeeId",
                table: "Sales");

            migrationBuilder.DropTable(
                name: "StockMovements");

            migrationBuilder.DropIndex(
                name: "IX_Sales_EmployeeId",
                table: "Sales");

            migrationBuilder.DropIndex(
                name: "IX_Purchases_CreatedByEmployeeId",
                table: "Purchases");

            migrationBuilder.DropIndex(
                name: "IX_Purchases_UpdatedByEmployeeId",
                table: "Purchases");

            migrationBuilder.DropIndex(
                name: "IX_Products_CreatedByEmployeeId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_UpdatedByEmployeeId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Inventory_CreatedByEmployeeId",
                table: "Inventory");

            migrationBuilder.DropIndex(
                name: "IX_Inventory_UpdatedByEmployeeId",
                table: "Inventory");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "CreatedByEmployeeId",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "UpdatedByEmployeeId",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "CreatedByEmployeeId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "UpdatedByEmployeeId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Inventory");

            migrationBuilder.DropColumn(
                name: "CreatedByEmployeeId",
                table: "Inventory");

            migrationBuilder.DropColumn(
                name: "UpdatedByEmployeeId",
                table: "Inventory");
        }
    }
}
