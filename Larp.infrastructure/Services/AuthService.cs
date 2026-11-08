using System;
using Larp.domain.Entities;
using Larp.infrastructure.Services;

namespace Larp.infrastructure.Services
{
    /// <summary>
    /// Orchestrates the full login pipeline:
    /// 1) Master DB: which tenant does this username belong to, is it active?
    /// 2) Tenant DB: does the password match, what role does the employee have?
    /// This is the single entry point the LoginForm calls.
    /// </summary>
    public class AuthService
    {
        private readonly MasterDbService _masterDb;
        private readonly TenantAuthService _tenantAuth;

        public AuthService(string masterConnectionString)
        {
            _masterDb = new MasterDbService(masterConnectionString);
            _tenantAuth = new TenantAuthService();
        }

        public async Task<LoginResult> LoginAsync(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                return LoginResult.Fail(LoginOutcome.InvalidCredentials, "Enter both username and password.");

            TenantLookup tenant;
            try
            {
                tenant = await _masterDb.FindTenantForUserAsync(username);
            }
            catch (Exception ex)
            {
                return LoginResult.Fail(LoginOutcome.Error, $"Could not reach master database: {ex.Message}");
            }

            if (tenant == null)
                return LoginResult.Fail(LoginOutcome.UserNotFound, "Username not recognized.");

            if (!tenant.IsActive || tenant.SubscriptionStatus is "Suspended" or "Cancelled")
                return LoginResult.Fail(LoginOutcome.TenantInactive,
                    $"This account's store subscription is {tenant.SubscriptionStatus}. Contact support.");

            string tenantConnStr = _masterDb.BuildTenantConnectionString(tenant);

            TenantAuthService.EmployeeRecord employee;
            try
            {
                employee = await _tenantAuth.FindEmployeeAsync(tenantConnStr, username);
            }
            catch (Exception ex)
            {
                return LoginResult.Fail(LoginOutcome.TenantUnreachable,
                    $"Could not reach this store's database ({ex.Message}). It may need to be restored from backup.");
            }

            if (employee == null || !employee.IsActive)
                return LoginResult.Fail(LoginOutcome.InvalidCredentials, "Invalid username or password.");

            bool passwordOk = PasswordHasher.Verify(password, employee.PasswordHash, employee.PasswordSalt);
            if (!passwordOk)
                return LoginResult.Fail(LoginOutcome.InvalidCredentials, "Invalid username or password.");

            await _tenantAuth.UpdateLastLoginAsync(tenantConnStr, employee.EmployeeId);

            return new LoginResult
            {
                Outcome = LoginOutcome.Success,
                TenantId = tenant.TenantId,
                TenantCode = tenant.TenantCode,
                TenantName = tenant.TenantName,
                TenantDatabaseName = tenant.DatabaseName,
                TenantServerName = tenant.ServerName,
                EmployeeId = employee.EmployeeId,
                EmployeeFullName = employee.FullName,
                Role = employee.RoleName
            };
        }
    }
}