using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Larp.infrastructure.Migrations.TenantErpDb
{
    /// <inheritdoc />
    public partial class AddBranchDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BranchId",
                table: "Purchases",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BranchId",
                table: "Payrolls",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BranchId",
                table: "Expenses",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_BranchId",
                table: "Purchases",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Payrolls_BranchId",
                table: "Payrolls",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_BranchId",
                table: "Expenses",
                column: "BranchId");

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_Branches_BranchId",
                table: "Expenses",
                column: "BranchId",
                principalTable: "Branches",
                principalColumn: "BranchId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Payrolls_Branches_BranchId",
                table: "Payrolls",
                column: "BranchId",
                principalTable: "Branches",
                principalColumn: "BranchId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_Branches_BranchId",
                table: "Purchases",
                column: "BranchId",
                principalTable: "Branches",
                principalColumn: "BranchId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_Branches_BranchId",
                table: "Expenses");

            migrationBuilder.DropForeignKey(
                name: "FK_Payrolls_Branches_BranchId",
                table: "Payrolls");

            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_Branches_BranchId",
                table: "Purchases");

            migrationBuilder.DropIndex(
                name: "IX_Purchases_BranchId",
                table: "Purchases");

            migrationBuilder.DropIndex(
                name: "IX_Payrolls_BranchId",
                table: "Payrolls");

            migrationBuilder.DropIndex(
                name: "IX_Expenses_BranchId",
                table: "Expenses");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "Payrolls");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "Expenses");
        }
    }
}
