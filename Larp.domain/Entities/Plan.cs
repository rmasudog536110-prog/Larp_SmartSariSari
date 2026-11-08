namespace Larp.domain.Entities
{
    public class Plan
    {
        public int PlanId { get; set; }

        public string Name { get; set; } = string.Empty;          // Basic / Premium / Enterprise

        public string? Description { get; set; }                   // "Micro store", ...

        public decimal MonthlyPrice { get; set; }

        public decimal YearlyPrice { get; set; }

        public int MaxEmployees { get; set; }

        // 0 = the plan has no branching at all.
        public int MaxBranches { get; set; }

        // Comma-separated, same words as the API address: /tenant/{id}/{module}
        public string Modules { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;

        public int SortOrder { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}