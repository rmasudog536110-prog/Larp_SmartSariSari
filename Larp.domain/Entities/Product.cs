namespace Larp.domain.Entities
{
    public class Product
    {
        public int ProductId { get; set; }

        public string ProductCode { get; set; } = string.Empty;

        public string ProductName { get; set; } = string.Empty;

        public decimal UnitPrice { get; set; }

        public bool IsActive { get; set; } = true;

        public int? CreatedByEmployeeId { get; set; }

        public int? UpdatedByEmployeeId { get; set; }

        public bool IsVatable { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        public EmployeeEntity? CreatedByEmployee { get; set; }

        public EmployeeEntity? UpdatedByEmployee { get; set; }
    }
}