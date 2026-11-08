using System;
using System.Collections.Generic;

namespace Larp.domain.Entities
{
    public class Company
    {
        public int CompanyId { get; set; }
        public string CompanyCode { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public ICollection<Device> Devices { get; set; } = new List<Device>();
        public bool isActive { get; set; } = true;
        public DateTime isCreated { get; set; } = DateTime.UtcNow;

        public string SubscriptionStatus { get; set; } = "Active"; // Active/PastDue/Suspended/Cancelled
        public string? SubscriptionPlan { get; set; } 
        public DateTime? SubscriptionExpiry { get; set; } 

        public int? PlanId { get; set; }
        public Plan? Plan { get; set; }

        // Printed on the sales receipt.
        public string? Address { get; set; }
        public string? Tin { get; set; }
        public string? ContactNumber { get; set; }
    }
}