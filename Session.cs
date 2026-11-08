namespace Larp.winforms;

public static class Session
{
    private static HashSet<string> _modules = new(StringComparer.OrdinalIgnoreCase);

    public static string Token { get; private set; } = "";
    public static string FullName { get; private set; } = "";
    public static string Role { get; private set; } = "";
    public static int? CompanyId { get; private set; }
    public static string? CompanyName { get; private set; }
    public static int? EmployeeId { get; private set; }

    // From the company's plan (empty for SuperAdmin, who is never limited).
    public static string? PlanName { get; private set; }
    public static int MaxEmployees { get; private set; }
    public static int MaxBranches { get; private set; }

    public static bool IsLoggedIn => !string.IsNullOrEmpty(Token);
    public static bool IsSuperAdmin => Role == "SuperAdmin";

    // True when the company's plan includes the module (the same words as the API address).
    public static bool HasModule(string module) =>
        IsSuperAdmin || _modules.Contains(module);

    public static void Start(string token, string fullName, string role,
        int? companyId, string? companyName, int? employeeId,
        string? planName = null, IEnumerable<string>? modules = null,
        int maxEmployees = 0, int maxBranches = 0)
    {
        Token = token;
        FullName = fullName;
        Role = role;
        CompanyId = companyId;
        CompanyName = companyName;
        EmployeeId = employeeId;

        PlanName = planName;
        MaxEmployees = maxEmployees;
        MaxBranches = maxBranches;
        _modules = new HashSet<string>(
            modules ?? Array.Empty<string>(),
            StringComparer.OrdinalIgnoreCase);
    }

    public static void Clear()
    {
        Token = FullName = Role = "";
        CompanyId = null;
        CompanyName = null;
        EmployeeId = null;
        PlanName = null;
        MaxEmployees = 0;
        MaxBranches = 0;
        _modules = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
    }
}