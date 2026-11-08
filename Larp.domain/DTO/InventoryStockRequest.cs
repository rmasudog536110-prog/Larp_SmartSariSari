using System;
using System.Collections.Generic;
using System.Text;

namespace Larp.domain.DTO
{
    public class InventoryStockRequest
    {
        public decimal QuantityOnHand { get; set; }
        public string ProductCode { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
        public decimal ReorderLevel { get; set; }
    }
}
