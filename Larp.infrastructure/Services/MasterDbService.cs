using System;
using System.Data;
using Microsoft.Data.SqlClient;
using Larp.domain.Entities;

namespace Larp.infrastructure.Services
{

    public class MasterDbService
    {
        private readonly string _masterConnectionString;

        public MasterDbService(string masterConnectionString)
        {
            _masterConnectionString = masterConnectionString;
        }

        /// <summary>
        /// Looks up the tenant a given username belongs to.
        /// Returns null if the username isn't in the directory.
        /// </summary>
        public async Task<TenantLookup?> FindTenantForUserAsync(string username)
        {
            const string sql = @"
                SELECT t.TenantId, t.TenantCode, t.TenantName, t.DatabaseName,
                       t.ServerName, t.SqlLoginUsername, t.SqlLoginPassword,
                       t.IsActive, t.SubscriptionStatus
                FROM UserDirectory ud
                INNER JOIN Tenants t ON t.TenantId = ud.TenantId
                WHERE ud.Username = @Username AND ud.IsActive = 1;";

            using var conn = new SqlConnection(_masterConnectionString);
            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.Add("@Username", SqlDbType.NVarChar, 100).Value = username;

            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            if (!await reader.ReadAsync())
                return null;

            return new TenantLookup
            {
                TenantId = reader.GetInt32(reader.GetOrdinal("TenantId")),
                TenantCode = reader.GetString(reader.GetOrdinal("TenantCode")),
                TenantName = reader.GetString(reader.GetOrdinal("TenantName")),
                DatabaseName = reader.GetString(reader.GetOrdinal("DatabaseName")),
                ServerName = reader.GetString(reader.GetOrdinal("ServerName")),
                SqlLoginUsername = reader.GetString(reader.GetOrdinal("SqlLoginUsername")),
                SqlLoginPassword = reader.GetString(reader.GetOrdinal("SqlLoginPassword")),
                IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive")),
                SubscriptionStatus = reader.GetString(reader.GetOrdinal("SubscriptionStatus"))
            };
        }

        /// <summary>
        /// Builds a connection string to a specific tenant's database using SQL
        /// Server authentication — required on shared/cloud hosts like MonsterASP,
        /// which don't support Windows Integrated Security for remote connections.
        /// </summary>
        public string BuildTenantConnectionString(TenantLookup tenant)
        {
            var builder = new SqlConnectionStringBuilder
            {
                DataSource = tenant.ServerName,     
                InitialCatalog = tenant.DatabaseName,
                UserID = tenant.SqlLoginUsername,
                Password = tenant.SqlLoginPassword,
                TrustServerCertificate = true,
                ConnectTimeout = 8
            };
            return builder.ConnectionString;
        }
    }

    public class TenantLookup
    {
        public int TenantId { get; set; }
        public string TenantCode { get; set; }
        public string TenantName { get; set; }
        public string DatabaseName { get; set; }
        public string ServerName { get; set; }
        public string SqlLoginUsername { get; set; }
        public string SqlLoginPassword { get; set; }
        public bool IsActive { get; set; }
        public string SubscriptionStatus { get; set; }
    }
}