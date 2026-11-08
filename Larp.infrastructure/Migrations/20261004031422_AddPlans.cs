using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Larp.infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPlans : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Companies",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContactNumber",
                table: "Companies",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PlanId",
                table: "Companies",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Tin",
                table: "Companies",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Plans",
                columns: table => new
                {
                    PlanId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    MonthlyPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    YearlyPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    MaxEmployees = table.Column<int>(type: "int", nullable: false),
                    MaxBranches = table.Column<int>(type: "int", nullable: false),
                    Modules = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plans", x => x.PlanId);
                });

            migrationBuilder.InsertData(
                table: "Plans",
                columns: new[] { "PlanId", "CreatedAt", "Description", "IsActive", "MaxBranches", "MaxEmployees", "Modules", "MonthlyPrice", "Name", "SortOrder", "YearlyPrice" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 10, 4, 0, 0, 0, 0, DateTimeKind.Utc), "Micro store", true, 0, 5, "products,inventory,sales,customers,stock-movements,reports", 499m, "Basic", 1, 4990m },
                    { 2, new DateTime(2026, 10, 4, 0, 0, 0, 0, DateTimeKind.Utc), "Small company", true, 0, 25, "products,inventory,sales,customers,stock-movements,reports,suppliers,purchases,expenses,expense-categories,employees,payroll,approvals,benefits,contribution-rates,income-statement", 1499m, "Premium", 2, 14990m },
                    { 3, new DateTime(2026, 10, 4, 0, 0, 0, 0, DateTimeKind.Utc), "Medium company", true, 10, 100, "products,inventory,sales,customers,stock-movements,reports,suppliers,purchases,expenses,expense-categories,employees,payroll,approvals,benefits,contribution-rates,income-statement,branches", 3999m, "Enterprise", 3, 39990m }
                });

            migrationBuilder.Sql(@"
            UPDATE Companies SET PlanId = (SELECT PlanId FROM Plans WHERE Name = 'Basic'),
                   SubscriptionPlan = 'Basic'      WHERE CompanyCode = 'COM-001';
            UPDATE Companies SET PlanId = (SELECT PlanId FROM Plans WHERE Name = 'Premium'),
                   SubscriptionPlan = 'Premium'    WHERE CompanyCode = 'COM-002';
            UPDATE Companies SET PlanId = (SELECT PlanId FROM Plans WHERE Name = 'Enterprise'),
                   SubscriptionPlan = 'Enterprise' WHERE CompanyCode = 'COM-003';");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_PlanId",
                table: "Companies",
                column: "PlanId");

            migrationBuilder.CreateIndex(
                name: "IX_Plans_Name",
                table: "Plans",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_Plans_PlanId",
                table: "Companies",
                column: "PlanId",
                principalTable: "Plans",
                principalColumn: "PlanId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companies_Plans_PlanId",
                table: "Companies");

            migrationBuilder.DropTable(
                name: "Plans");

            migrationBuilder.DropIndex(
                name: "IX_Companies_PlanId",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "ContactNumber",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "PlanId",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "Tin",
                table: "Companies");
        }
    }
}
