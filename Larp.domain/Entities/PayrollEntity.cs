namespace Larp.domain.Entities
{
    public class PayrollEntity
    {
        public int PayrollId { get; set; }

        public int EmployeeId { get; set; }

        public EmployeeEntity? Employee { get; set; } = null!;

        public DateTime PayPeriodStart { get; set; }

        public DateTime PayPeriodEnd { get; set; }

        public decimal BasicSalary { get; set; }

        public decimal Allowances { get; set; }

        public decimal Deductions { get; set; }

        public decimal NetPay { get; set; }

        public decimal SssEmployee { get; set; }
        public decimal SssEmployer { get; set; }
        public decimal PhilHealthEmployee { get; set; }
        public decimal PhilHealthEmployer { get; set; }
        public decimal PagibigEmployee { get; set; }
        public decimal PagibigEmployer { get; set; }

        public string Status { get; set; } = "Pending";

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int? BranchId { get; set; }

        public Branch? Branch { get; set; }
    }
}