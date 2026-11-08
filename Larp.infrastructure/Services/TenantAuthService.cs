using System.Data;
using Microsoft.Data.SqlClient;

namespace Larp.infrastructure.Services
{
    /// <summary>
    /// Talks to a single TENANT database: validates employee credentials
    /// and returns their role. Nothing here ever touches another tenant's data.
    /// </summary>
    public class TenantAuthService
    {
        public class EmployeeRecord
        {
            public int EmployeeId { get; set; }
            public string FullName { get; set; }
            public string RoleName { get; set; }
            public byte[] PasswordHash { get; set; }
            public byte[] PasswordSalt { get; set; }
            public bool IsActive { get; set; }
        }

        /// <summary>Returns null if no such employee exists in this tenant DB.</summary>
        public async Task<EmployeeRecord?> FindEmployeeAsync(string tenantConnectionString, string username)
        {
            const string sql = @"
                SELECT e.EmployeeId, e.FullName, e.PasswordHash, e.PasswordSalt, e.IsActive,
                       r.RoleName
                FROM Employees e
                INNER JOIN Roles r ON r.RoleId = e.RoleId
                WHERE e.Username = @Username;";

            using var conn = new SqlConnection(tenantConnectionString);
            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.Add("@Username", SqlDbType.NVarChar, 100).Value = username;

            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            if (!await reader.ReadAsync())
                return null;

            return new EmployeeRecord
            {
                EmployeeId = reader.GetInt32(reader.GetOrdinal("EmployeeId")),
                FullName = reader.GetString(reader.GetOrdinal("FullName")),
                RoleName = reader.GetString(reader.GetOrdinal("RoleName")),
                PasswordHash = (byte[])reader["PasswordHash"],
                PasswordSalt = (byte[])reader["PasswordSalt"],
                IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive"))
            };
        }

        public async Task UpdateLastLoginAsync(string tenantConnectionString, int employeeId)
        {
            const string sql = @"UPDATE Employees SET LastLoginAt = SYSUTCDATETIME() WHERE EmployeeId = @Id;";
            using var conn = new SqlConnection(tenantConnectionString);
            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.Add("@Id", SqlDbType.Int).Value = employeeId;
            await conn.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
        }
    }
}