namespace Larp.domain.Entities
{
    public class StockMovement
    {
        public int StockMovementId { get; set; }

        public int ProductId { get; set; }

        public string MovementType { get; set; } = string.Empty;

        public decimal Quantity { get; set; }

        public decimal QuantityBefore { get; set; }

        public decimal QuantityAfter { get; set; }

        public int? PurchaseId { get; set; }

        public int EmployeeId { get; set; }

        public DateTime MovementDate { get; set; } = DateTime.UtcNow;

        public string? Remarks { get; set; }

        public Product? Product { get; set; }

        public Purchase? Purchase { get; set; }

        public EmployeeEntity? Employee { get; set; }
    }
}