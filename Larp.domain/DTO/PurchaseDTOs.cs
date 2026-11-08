namespace Larp.domain.DTO
{
    public class CreatePurchaseRequest
    {
        public int SupplierId { get; set; }

        public string? PurchaseNumber { get; set; }

        public decimal Discount { get; set; }

        // Only an Admin can choose this. For everyone else it is ignored and
        // the creating employee's branch is used.
        public int? BranchId { get; set; }

        public List<CreatePurchaseItemRequest> Items { get; set; } = new();
    }

    public class CreatePurchaseItemRequest
    {
        public int ProductId { get; set; }

        public decimal Quantity { get; set; }

        public decimal UnitCost { get; set; }
    }

    public class PurchaseDto
    {
        public int PurchaseId { get; set; }

        public string PurchaseNumber { get; set; } = string.Empty;

        public int SupplierId { get; set; }

        public string SupplierName { get; set; } = string.Empty;

        public DateTime PurchaseDate { get; set; }

        public decimal Subtotal { get; set; }

        public decimal Discount { get; set; }

        public decimal TotalAmount { get; set; }

        public string Status { get; set; } = string.Empty;

        public int CreatedByEmployeeId { get; set; }

        public DateTime CreatedAt { get; set; }

        public int? BranchId { get; set; }

        public string? BranchName { get; set; }

        public List<PurchaseItemDto> Items { get; set; } = new();
    }

    public class PurchaseItemDto
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; } = string.Empty;

        public decimal Quantity { get; set; }

        public decimal UnitCost { get; set; }

        public decimal TotalAmount { get; set; }
    }
}