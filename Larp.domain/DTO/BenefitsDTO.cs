namespace Larp.domain.DTO
{
    public class BenefitsDto
    {
        public int EmployeeId { get; set; }
        public string? SssNumber { get; set; }
        public string? PhilHealthNumber { get; set; }
        public string? PagibigNumber { get; set; }
        public decimal MonthlySalary { get; set; }
    }

    public class SaveBenefitsRequest
    {
        public string? SssNumber { get; set; }
        public string? PhilHealthNumber { get; set; }
        public string? PagibigNumber { get; set; }
        public decimal MonthlySalary { get; set; }
    }
}