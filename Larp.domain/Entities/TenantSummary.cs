using System;
using System.Collections.Generic;
using System.Text;

namespace Larp.domain.Entities
{
    public class TenantSummary
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
        public string SubscriptionPlan { get; set; }
        public DateTime? SubscriptionExpiry { get; set; }

        // Populated live from the tenant DB when building the BI tab.
        // Null / -1 means "couldn't reach this tenant's DB right now".
        public decimal? SalesToday { get; set; }
        public int? LowStockCount { get; set; }
        public bool Reachable { get; set; } = true;
        public string ReachError { get; set; }
    }
}
