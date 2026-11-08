namespace Larp.domain.DTO
{
    public class PayrollDto
    {
        public int PayrollId { get; set; }

        public int EmployeeId { get; set; }

        public string EmployeeCode { get; set; } = string.Empty;

        public string EmployeeName { get; set; } = string.Empty;

        public string Position { get; set; } = string.Empty;

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

        public DateTime CreatedAt { get; set; }

        public int? BranchId { get; set; }

        public string? BranchName { get; set; }
    }
}