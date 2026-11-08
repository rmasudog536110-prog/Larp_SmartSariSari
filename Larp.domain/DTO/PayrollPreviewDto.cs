namespace Larp.domain.DTO
{
    // What the Add Payroll form shows after an employee is picked. Nothing is saved.
    public class PayrollPreviewDto
    {
        public int EmployeeId { get; set; }

        public string EmployeeCode { get; set; } = string.Empty;

        public string EmployeeName { get; set; } = string.Empty;

        public string Position { get; set; } = string.Empty;

        public DateTime PayPeriodStart { get; set; }

        public DateTime PayPeriodEnd { get; set; }

        // Half of the monthly salary (paid every 15 days).
        public decimal BasicSalary { get; set; }

        public decimal Allowances { get; set; }

        public decimal SssEmployee { get; set; }

        public decimal PhilHealthEmployee { get; set; }

        public decimal PagibigEmployee { get; set; }

        public decimal Deductions { get; set; }

        public decimal NetPay { get; set; }

        // False when the amounts could not be worked out; Message says why.
        public bool Ready { get; set; }

        public string? Message { get; set; }
    }
}