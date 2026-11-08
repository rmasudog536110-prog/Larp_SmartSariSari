using System.ComponentModel.DataAnnotations.Schema;

namespace Larp.domain.Entities
{
    public class EmployeeEntity
    {
        public int EmployeeId { get; set; }

        public string? UserId { get; set; }

        public string EmployeeCode { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string? MiddleName { get; set; }

        public string Position { get; set; } = string.Empty;

        [NotMapped]
        public string? Role { get; set; }

        public string? ContactNumber { get; set; }

        public string? EmailAddress { get; set; }

        public string? Address { get; set; }

        public DateTime DateHired { get; set; }

        public DateTime? DateSeparated { get; set; }

        public string EmploymentStatus { get; set; } = "Active";

        public string? SeparationReason { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public string ApprovalStatus { get; set; } = "Approved";

        public int? BranchId { get; set; }

        public Branch? Branch { get; set; }

        public ICollection<PayrollEntity> Payrolls { get; set; } = new List<PayrollEntity>();
    }

    public class EmployeeBenefits            // one row per employee
    {
        public int BenefitsId { get; set; }
        public int EmployeeId { get; set; }
        public string? SssNumber { get; set; }
        public string? PhilHealthNumber { get; set; }
        public string? PagibigNumber { get; set; }
        public decimal MonthlySalary { get; set; }

        public EmployeeEntity? Employee { get; set; }
    }

    public class ContributionRate       
    {
        public int ContributionRateId { get; set; }
        public string Type { get; set; } = string.Empty;
        public decimal MinSalary { get; set; }
        public decimal MaxSalary { get; set; }
        public bool IsPercentage { get; set; }        
        public decimal EmployeeShare { get; set; }
        public decimal EmployerShare { get; set; }
        public decimal? MaxContribution { get; set; }    
        public DateTime EffectiveFrom { get; set; }
    }
}