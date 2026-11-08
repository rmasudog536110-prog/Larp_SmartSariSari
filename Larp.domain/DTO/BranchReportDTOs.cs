namespace Larp.domain.DTO
{
    // Header + KPI cards for one branch (or "unassigned").
    public class BranchSummaryDto
    {
        public int? BranchId { get; set; }
        public string BranchCode { get; set; } = string.Empty;
        public string BranchName { get; set; } = string.Empty;
        public string? Address { get; set; }
        public string? ContactNumber { get; set; }
        public bool IsActive { get; set; }
        public int EmployeeCount { get; set; }

        // Gross = sum of completed sale totals (VAT included), like the income statement.
        public decimal GrossSales { get; set; }
        // Of which VAT.
        public decimal VatAmount { get; set; }
        public decimal NetSales { get; set; }
        public int Transactions { get; set; }
        public decimal ItemsSold { get; set; }
        public decimal AverageSale { get; set; }

        public decimal Purchases { get; set; }
        public decimal Expenses { get; set; }
        public decimal Payroll { get; set; }
        public decimal TotalCosts { get; set; }

        // Gross sales minus purchases, expenses and payroll (same as the income statement).
        public decimal Profit { get; set; }
    }

    public class BranchCompareRowDto
    {
        public int? BranchId { get; set; }
        public string BranchCode { get; set; } = string.Empty;
        public string BranchName { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public int Transactions { get; set; }
        public decimal GrossSales { get; set; }
        public decimal VatAmount { get; set; }
        public decimal NetSales { get; set; }
        public decimal Purchases { get; set; }
        public decimal Expenses { get; set; }
        public decimal Payroll { get; set; }
        public decimal TotalCosts { get; set; }
        public decimal Profit { get; set; }
        public decimal SharePercent { get; set; }
    }

    public class BranchCompareDto
    {
        // One row per branch plus an "Unassigned" row.
        public List<BranchCompareRowDto> Rows { get; set; } = new();

        // Sum of all rows. Must equal the company income statement.
        public BranchCompareRowDto Total { get; set; } = new();
    }

    // "Counted" is false for rows listed but left out of totals (cancelled, pending...).
    public class BranchSaleRowDto
    {
        public int SalesId { get; set; }
        public string SaleNumber { get; set; } = string.Empty;
        public DateTime SaleDate { get; set; }          // Philippine time
        public string Cashier { get; set; } = string.Empty;
        public decimal ItemCount { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Discount { get; set; }
        public decimal VatAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = string.Empty;
        public bool Counted { get; set; }
    }

    public class BranchPurchaseRowDto
    {
        public int PurchaseId { get; set; }
        public string PurchaseNumber { get; set; } = string.Empty;
        public DateTime PurchaseDate { get; set; }      // Philippine time
        public string Supplier { get; set; } = string.Empty;
        public decimal Discount { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = string.Empty;
        public bool Counted { get; set; }
    }

    public class BranchExpenseRowDto
    {
        public int ExpenseId { get; set; }
        public DateTime ExpenseDate { get; set; }
        public string Category { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public bool Counted { get; set; }
    }

    public class BranchEmployeeRowDto
    {
        public int EmployeeId { get; set; }
        public string EmployeeCode { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public int SalesCount { get; set; }
        public decimal SalesTotal { get; set; }
    }

    public class BranchPayrollRowDto
    {
        public int PayrollId { get; set; }
        public string Employee { get; set; } = string.Empty;
        public DateTime PeriodStart { get; set; }
        public DateTime PeriodEnd { get; set; }
        // Basic salary + allowances: the cost to the business.
        public decimal Gross { get; set; }
        public decimal Deductions { get; set; }
        public decimal NetPay { get; set; }
        public string Status { get; set; } = string.Empty;
        public bool Counted { get; set; }
    }

    public class BranchTopProductDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal Quantity { get; set; }
        public decimal Amount { get; set; }
        public decimal SharePercent { get; set; }
    }

    public class BranchTrendPointDto
    {
        public DateTime Date { get; set; }
        public string Label { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public int Count { get; set; }
    }
}