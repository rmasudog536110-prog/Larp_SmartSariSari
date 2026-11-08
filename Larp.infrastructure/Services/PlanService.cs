using Larp.domain.Entities;
using Larp.infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Larp.infrastructure.Services
{
    // What the access filter and the login response need to know about a company's plan.
    public record CompanyPlan(
        int? PlanId,
        string Name,
        int MaxEmployees,
        int MaxBranches,
        HashSet<string> Modules)
    {
        public static readonly CompanyPlan None =
            new(null, "No plan", 0, 0, new HashSet<string>(StringComparer.OrdinalIgnoreCase));

        public bool Includes(string module) => Modules.Contains(module);
    }

    public class PlanService
    {
        private static readonly TimeSpan CacheFor = TimeSpan.FromSeconds(60);

        private readonly MasterErpDbContext _master;
        private readonly ITenantDBContextFactory _tenantFactory;
        private readonly IMemoryCache _cache;

        public PlanService(
            MasterErpDbContext master,
            ITenantDBContextFactory tenantFactory,
            IMemoryCache cache)
        {
            _master = master;
            _tenantFactory = tenantFactory;
            _cache = cache;
        }

        private static string Key(int companyId) => $"plan:{companyId}";

        // Call after a company's plan changes so the change applies at once.
        public void Invalidate(int companyId) => _cache.Remove(Key(companyId));

        public async Task<CompanyPlan> GetForCompanyAsync(int companyId, bool fresh = false)
        {
            if (fresh)
                Invalidate(companyId);

            var plan = await _cache.GetOrCreateAsync(Key(companyId), async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = CacheFor;
                return await LoadAsync(companyId);
            });

            return plan ?? CompanyPlan.None;
        }

        private async Task<CompanyPlan> LoadAsync(int companyId)
        {
            var company = await _master.Companies
                .AsNoTracking()
                .Include(c => c.Plan)
                .FirstOrDefaultAsync(c => c.CompanyId == companyId);

            if (company == null)
                return CompanyPlan.None;

            var plan = company.Plan;

            // Older companies: fall back to the plan name text.
            if (plan == null && !string.IsNullOrWhiteSpace(company.SubscriptionPlan))
            {
                plan = await _master.Plans
                    .AsNoTracking()
                    .FirstOrDefaultAsync(p => p.Name == company.SubscriptionPlan);
            }

            if (plan == null)
                return CompanyPlan.None;

            var modules = new HashSet<string>(
                plan.Modules.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries),
                StringComparer.OrdinalIgnoreCase);

            return new CompanyPlan(plan.PlanId, plan.Name, plan.MaxEmployees, plan.MaxBranches, modules);
        }

        // ---------------- Limits (return a message when blocked, null when allowed) ----------------

        public async Task<string?> CheckEmployeeLimitAsync(int companyId)
        {
            var plan = await GetForCompanyAsync(companyId);

            if (plan.PlanId == null)
                return "No plan is assigned to this company. Contact support.";

            await using var db = await _tenantFactory.CreateAsync(companyId);

            int count = await db.Employees
                .CountAsync(e => e.IsActive && e.ApprovalStatus != "Rejected");

            return count >= plan.MaxEmployees
                ? $"Your {plan.Name} plan allows up to {plan.MaxEmployees} employees. Upgrade to add more."
                : null;
        }

        public async Task<string?> CheckBranchLimitAsync(int companyId)
        {
            var plan = await GetForCompanyAsync(companyId);

            if (plan.PlanId == null)
                return "No plan is assigned to this company. Contact support.";

            if (plan.MaxBranches <= 0)
                return $"Your {plan.Name} plan does not include branches. Upgrade to use them.";

            await using var db = await _tenantFactory.CreateAsync(companyId);

            int count = await db.Branches.CountAsync(b => b.IsActive);

            return count >= plan.MaxBranches
                ? $"Your {plan.Name} plan allows up to {plan.MaxBranches} branches. Upgrade to add more."
                : null;
        }
    }
}