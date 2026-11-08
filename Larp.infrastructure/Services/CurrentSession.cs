using Larp.domain.Entities;

namespace Larp.infrastructure.Services
{
    /// <summary>
    /// Holds the logged-in context for the lifetime of the desktop app.
    /// Set once by LoginForm after a successful login; read everywhere else
    /// (Dashboard, per-tenant forms, and the Admin form's role check).
    /// </summary>
    public static class CurrentSession
    {
        public static int TenantId { get; private set; }
        public static string TenantCode { get; private set; }
        public static string TenantName { get; private set; }
        public static string TenantConnectionString { get; private set; }

        public static int EmployeeId { get; private set; }
        public static string EmployeeFullName { get; private set; }
        public static string Role { get; private set; }

        public static bool IsLoggedIn { get; private set; }

        public static void Start(LoginResult result, string tenantConnectionString)
        {
            TenantId = result.TenantId;
            TenantCode = result.TenantCode;
            TenantName = result.TenantName;
            TenantConnectionString = tenantConnectionString;

            EmployeeId = result.EmployeeId;
            EmployeeFullName = result.EmployeeFullName;
            Role = result.Role;

            IsLoggedIn = true;
        }

        public static bool IsAdmin => IsLoggedIn && Role == "Admin";

        public static void Clear()
        {
            IsLoggedIn = false;
            TenantId = 0;
            TenantCode = TenantName = TenantConnectionString = null;
            EmployeeId = 0;
            EmployeeFullName = Role = null;
        }
    }
}