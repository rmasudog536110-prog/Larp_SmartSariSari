namespace Larp.domain.Entities
{
    public class Purchase
    {
        public int PurchaseId { get; set; }

        public string PurchaseNumber { get; set; } = string.Empty;

        public int SupplierId { get; set; }

        public DateTime PurchaseDate { get; set; } = DateTime.UtcNow;

        public decimal Subtotal { get; set; }

        public decimal Discount { get; set; }

        public decimal TotalAmount { get; set; }

        public string Status { get; set; } = "Received";

        public int CreatedByEmployeeId { get; set; }

        public int? UpdatedByEmployeeId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        // Branch this purchase belongs to. Null = Unassigned (older records).
        public int? BranchId { get; set; }

        public Branch? Branch { get; set; }

        public Supplier? Supplier { get; set; }

        public EmployeeEntity? CreatedByEmployee { get; set; }

        public EmployeeEntity? UpdatedByEmployee { get; set; }

        public ICollection<PurchaseItem> PurchaseItems { get; set; }
            = new List<PurchaseItem>();
    }
}