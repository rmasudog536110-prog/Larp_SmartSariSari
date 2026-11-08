namespace Larp.domain.DTO
{
    public class SalesReportRequest
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int TotalTransactions { get; set; }

        public decimal TotalSales { get; set; }

        public decimal TotalItemsSold { get; set; }

        public List<SalesReportItemDto> Items { get; set; } = new();
    }

    public class SalesReportItemDto
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; } = string.Empty;

        public decimal UnitPrice { get; set; }

        public decimal QuantitySold { get; set; }

        public decimal TotalAmount { get; set; }
    }
}