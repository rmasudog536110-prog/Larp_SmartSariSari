using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.Data.SqlClient;
using Larp.domain.Entities;

namespace Larp.infrastructure.Services
{
    /// <summary>
    /// Live cross-tenant BI. Queries EACH tenant DB individually (no rollup
    /// table), so this scales with the number of tenants — fine for a
    /// handful of stores, but expect it to slow down as tenant count grows.
    /// Any tenant DB that can't be reached is marked unreachable instead of
    /// throwing, so one corrupted tenant doesn't break the whole dashboard.
    ///
    /// NOTE: the Sales/Products column names below (SaleDate, TotalAmount,
    /// StockQuantity, ReorderLevel) are placeholders — adjust the SQL to
    /// match your actual tenant-DB schema (the same one Dashboard/Sales/
    /// Inventory WinForms already read from).
    /// </summary>
    public class BusinessIntelligenceService
    {
        private readonly TenantDirectoryService _directory;

        public BusinessIntelligenceService(TenantDirectoryService directory)
        {
            _directory = directory;
        }

        /// <summary>Pulls live KPIs for every active tenant, in parallel.</summary>
        public List<TenantSummary> GetLiveOverview()
        {
            var tenants = _directory.GetAllTenants();

            System.Threading.Tasks.Parallel.ForEach(tenants, tenant =>
            {
                try
                {
                    string connStr = _directory.BuildTenantConnectionString(tenant);
                    (tenant.SalesToday, tenant.LowStockCount) = QueryTenantKpis(connStr);
                    tenant.Reachable = true;
                }
                catch (Exception ex)
                {
                    tenant.Reachable = false;
                    tenant.ReachError = ex.Message;
                }
            });

            return tenants;
        }

        private (decimal salesToday, int lowStockCount) QueryTenantKpis(string tenantConnStr)
        {
            const string salesSql = @"
                SELECT ISNULL(SUM(TotalAmount), 0)
                FROM Sales
                WHERE CAST(SaleDate AS DATE) = CAST(SYSUTCDATETIME() AS DATE);";

            const string stockSql = @"
                SELECT COUNT(*)
                FROM Products
                WHERE StockQuantity <= ReorderLevel;";

            using var conn = new SqlConnection(tenantConnStr);
            conn.Open();

            decimal salesToday;
            using (var cmd = new SqlCommand(salesSql, conn))
                salesToday = (decimal)cmd.ExecuteScalar();

            int lowStock;
            using (var cmd = new SqlCommand(stockSql, conn))
                lowStock = (int)cmd.ExecuteScalar();

            return (salesToday, lowStock);
        }

        public decimal TotalSalesTodayAcrossTenants(IEnumerable<TenantSummary> tenants) =>
            tenants.Where(t => t.Reachable).Sum(t => t.SalesToday ?? 0);

        public int TenantsWithLowStock(IEnumerable<TenantSummary> tenants) =>
            tenants.Count(t => t.Reachable && (t.LowStockCount ?? 0) > 0);
    }
}