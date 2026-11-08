namespace Larp.domain.Entities
{
    public class PurchaseItem
    {
        public int PurchaseItemId { get; set; }

        public int PurchaseId { get; set; }

        public int ProductId { get; set; }

        public decimal Quantity { get; set; }

        public decimal UnitCost { get; set; }

        public decimal TotalAmount { get; set; }

        public Purchase? Purchase { get; set; }

        public Product? Product { get; set; }
    }
}