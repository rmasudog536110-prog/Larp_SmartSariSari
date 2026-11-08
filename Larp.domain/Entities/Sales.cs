namespace Larp.domain.Entities
{
    public class Sales
    {
        public int SalesId { get; set; }
        public string SaleNumber { get; set; } = string.Empty;
        public DateTime SaleDate { get; set; } = DateTime.UtcNow;
        public int? CustomerId { get; set; }
        public int? EmployeeId { get; set; }
        public int? BranchId { get; set; }

        public decimal Subtotal { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalAmount { get; set; }

        public decimal VatableSales { get; set; }
        public decimal VatExempt { get; set; }
        public decimal VatAmount { get; set; }

        public decimal CashReceived { get; set; }
        public decimal ChangeAmount { get; set; }
        public string PaymentMethod { get; set; } = "Cash";

        public string Status { get; set; } = "Completed";

        public Customer? Customer { get; set; }
        public EmployeeEntity? Employee { get; set; }
        public Branch? Branch { get; set; }

        public ICollection<SaleItem> SaleItems { get; set; } = new List<SaleItem>();
    }
}