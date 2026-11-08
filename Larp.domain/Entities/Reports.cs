using System;
using System.Collections.Generic;
using System.Text;

namespace Larp.domain.Entities
{
    public class Report
    {
        public int ReportId { get; set; }

        public string ReportName { get; set; } = string.Empty;

        public string ReportType { get; set; } = string.Empty;

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public decimal TotalSales { get; set; }

        public decimal TotalExpenses { get; set; }

        public decimal TotalProfit { get; set; }

        public string GeneratedBy { get; set; } = string.Empty;

        public DateTime GeneratedAt { get; set; } = DateTime.UtcNow;

        public string Notes { get; set; } = string.Empty;
    }
}