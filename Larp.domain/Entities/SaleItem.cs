using System;
using System.Collections.Generic;
using System.Text;

namespace Larp.domain.Entities
{
    public class SaleItem
    {
        public int SaleItemId { get; set; }

        public int SaleId { get; set; }

        public int ProductId { get; set; }

        public decimal Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal Discount { get; set; }

        public decimal TotalAmount { get; set; }

        public decimal TaxRate { get; set; }

        public decimal TaxAmount { get; set; }

        public Sales? Sale { get; set; }

        public Product? Product { get; set; }
    }
}
