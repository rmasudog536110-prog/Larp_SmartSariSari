using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace Larp.infrastructure.Services
{
    public class SubscriptionService
    {
        private readonly string _masterConnectionString;

        public SubscriptionService(string masterConnectionString)
        {
            _masterConnectionString = masterConnectionString;
        }

        public void UpdateSubscription(int tenantId, string status, string plan, DateTime? expiry)
        {
            const string sql = @"
                UPDATE Tenants
                SET SubscriptionStatus = @Status,
                    SubscriptionPlan = @Plan,
                    SubscriptionExpiry = @Expiry,
                    IsActive = CASE WHEN @Status IN ('Suspended','Cancelled') THEN 0 ELSE 1 END
                WHERE TenantId = @TenantId;";

            using var conn = new SqlConnection(_masterConnectionString);
            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.Add("@Status", SqlDbType.NVarChar, 20).Value = status;
            cmd.Parameters.Add("@Plan", SqlDbType.NVarChar, 50).Value = (object)plan ?? DBNull.Value;
            cmd.Parameters.Add("@Expiry", SqlDbType.DateTime2).Value = (object)expiry ?? DBNull.Value;
            cmd.Parameters.Add("@TenantId", SqlDbType.Int).Value = tenantId;

            conn.Open();
            cmd.ExecuteNonQuery();
        }

        public void Suspend(int tenantId) => UpdateSubscription(tenantId, "Suspended", null, null);

        public void Reactivate(int tenantId, string plan, DateTime expiry) =>
            UpdateSubscription(tenantId, "Active", plan, expiry);
    }
}