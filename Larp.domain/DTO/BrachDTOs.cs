namespace Larp.domain.DTO
{
    public class SaveBranchRequest
    {
        public string BranchCode { get; set; } = string.Empty;

        public string BranchName { get; set; } = string.Empty;

        public string? Address { get; set; }

        public string? ContactNumber { get; set; }

        public bool IsActive { get; set; } = true;
    }

    public class BranchDto
    {
        public int BranchId { get; set; }

        public string BranchCode { get; set; } = string.Empty;

        public string BranchName { get; set; } = string.Empty;

        public string? Address { get; set; }

        public string? ContactNumber { get; set; }

        public bool IsActive { get; set; }

        // Active employees assigned to this branch.
        public int EmployeeCount { get; set; }

        // Sales recorded by this branch's cashiers (cancelled sales excluded).
        public int SalesCount { get; set; }

        public decimal SalesTotal { get; set; }
    }
}