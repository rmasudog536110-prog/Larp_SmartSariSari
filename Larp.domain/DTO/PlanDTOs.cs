namespace Larp.domain.DTO
{
    // The modules a plan can include. Keys match the API address: /tenant/{id}/{key}
    public static class PlanModules
    {
        public static readonly (string Key, string Label)[] All =
        {
            ("products", "Products"),
            ("inventory", "Inventory"),
            ("sales", "Sales"),
            ("customers", "Customers"),
            ("stock-movements", "Stock movements"),
            ("reports", "Sales and inventory reports"),
            ("income-statement", "Income statement"),
            ("suppliers", "Suppliers"),
            ("purchases", "Purchases"),
            ("expenses", "Expenses"),
            ("expense-categories", "Expense categories"),
            ("employees", "User management"),
            ("payroll", "Payroll"),
            ("approvals", "Approvals"),
            ("benefits", "Employee benefits"),
            ("contribution-rates", "Contribution rates"),
            ("branches", "Branches and branch details")
        };

        public static bool IsKnown(string key) =>
            All.Any(m => m.Key.Equals(key, StringComparison.OrdinalIgnoreCase));
    }

    public class PlanDto
    {
        public int PlanId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal MonthlyPrice { get; set; }
        public decimal YearlyPrice { get; set; }
        public int MaxEmployees { get; set; }
        public int MaxBranches { get; set; }
        public List<string> Modules { get; set; } = new();
        public bool IsActive { get; set; }
        public int SortOrder { get; set; }

        // Active companies currently on this plan.
        public int CompanyCount { get; set; }
    }

    public class SavePlanRequest
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal MonthlyPrice { get; set; }
        public decimal YearlyPrice { get; set; }
        public int MaxEmployees { get; set; }
        public int MaxBranches { get; set; }
        public List<string> Modules { get; set; } = new();
        public bool IsActive { get; set; } = true;
        public int SortOrder { get; set; }
    }

    public class UpdateTenantRequest
    {
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Plan { get; set; } = string.Empty;
        public DateTime? Expiry { get; set; }
        public string? Address { get; set; }
        public string? Tin { get; set; }
        public string? ContactNumber { get; set; }
    }
}