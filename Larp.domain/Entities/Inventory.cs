using System;
using System.Collections.Generic;
using System.Text;

namespace Larp.domain.Entities
{
    public class Inventory
    {
        public int InventoryId { get; set; }

        public int ProductId { get; set; }

        public decimal QuantityOnHand { get; set; }

        public decimal ReorderLevel { get; set; }

        public DateTime LastUpdatedAt { get; set; } = DateTime.UtcNow;

        public int?  CreatedByEmployeeId { get; set; }

        public int? UpdatedByEmployeeId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public Product? Product { get; set; }

        public EmployeeEntity? CreatedByEmployee { get; set; }

        public EmployeeEntity? UpdatedByEmployee { get; set; }
    }
}

