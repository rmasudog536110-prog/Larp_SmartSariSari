namespace Larp.domain.DTO
{
    public class IncomeStatementDto
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public decimal TotalSales { get; set; }

        public decimal TotalPurchases { get; set; }

        public decimal TotalExpenses { get; set; }

        public decimal TotalPayroll { get; set; }

        public decimal NetIncome { get; set; }

        // Ready-to-display statement rows, built on the server so the
        // numbers and the layout come from one place.
        public List<IncomeStatementLineDto> Lines { get; set; } = new();
    }

    public class IncomeStatementLineDto
    {
        public string Label { get; set; } = string.Empty;

        // Null for section headers.
        public decimal? Amount { get; set; }

        // "Header", "Line", "Subtotal" or "Total"
        public string Kind { get; set; } = "Line";
    }
}