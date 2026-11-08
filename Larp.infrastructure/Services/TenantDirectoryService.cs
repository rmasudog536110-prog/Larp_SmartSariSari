using Larp.domain.Entities;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;

namespace Larp.infrastructure.Services
{
    /// <summary>
    /// Master-DB-only reads/writes for the admin panel: list tenants, update
    /// subscription status. Does NOT touch any tenant database.
    /// </summary>
    public class TenantDirectoryService
    {
        private readonly string _masterConnectionString;

        public TenantDirectoryService(string masterConnectionString)
        {
            _masterConnectionString = masterConnectionString;
        }

        public List<TenantSummary> GetAllTenants()
        {
            const string sql = @"
                SELECT TenantId, TenantCode, TenantName, DatabaseName, ServerName,
                       SqlLoginUsername, SqlLoginPassword,
                       IsActive, SubscriptionStatus, SubscriptionPlan, SubscriptionExpiry
                FROM Tenants
                ORDER BY TenantName;";

            var tenants = new List<TenantSummary>();

            using var conn = new SqlConnection(_masterConnectionString);
            using var cmd = new SqlCommand(sql, conn);
            conn.Open();
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                tenants.Add(new TenantSummary
                {
                    TenantId = reader.GetInt32(reader.GetOrdinal("TenantId")),
                    TenantCode = reader.GetString(reader.GetOrdinal("TenantCode")),
                    TenantName = reader.GetString(reader.GetOrdinal("TenantName")),
                    DatabaseName = reader.GetString(reader.GetOrdinal("DatabaseName")),
                    ServerName = reader.GetString(reader.GetOrdinal("ServerName")),
                    SqlLoginUsername = reader.GetString(reader.GetOrdinal("SqlLoginUsername")),
                    SqlLoginPassword = reader.GetString(reader.GetOrdinal("SqlLoginPassword")),
                    IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive")),
                    SubscriptionStatus = reader.GetString(reader.GetOrdinal("SubscriptionStatus")),
                    SubscriptionPlan = reader.IsDBNull(reader.GetOrdinal("SubscriptionPlan"))
                        ? null : reader.GetString(reader.GetOrdinal("SubscriptionPlan")),
                    SubscriptionExpiry = reader.IsDBNull(reader.GetOrdinal("SubscriptionExpiry"))
                        ? (System.DateTime?)null : reader.GetDateTime(reader.GetOrdinal("SubscriptionExpiry"))
                });
            }
            return tenants;
        }

        public string BuildTenantConnectionString(TenantSummary tenant)
        {
            // SQL Server authentication — MonsterASP (and shared/cloud SQL hosts
            // generally) don't support Windows Integrated Security remotely.
            var builder = new SqlConnectionStringBuilder
            {
                DataSource = tenant.ServerName,
                InitialCatalog = tenant.DatabaseName,
                UserID = tenant.SqlLoginUsername,
                Password = tenant.SqlLoginPassword,
                TrustServerCertificate = true,
                ConnectTimeout = 5
            };
            return builder.ConnectionString;
        }
    }
}