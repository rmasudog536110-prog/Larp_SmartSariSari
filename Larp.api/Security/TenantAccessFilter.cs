using Larp.infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Larp.api.Security
{
    public class TenantAccessFilter : IAsyncAuthorizationFilter
    {
        private static readonly string[] Admin = { "Admin" };
        private static readonly string[] Manager = { "Manager" };
        private static readonly string[] Staff = { "Staff" };

        private static string[] Roles(params string[][] groups) =>
            groups.SelectMany(g => g).ToArray();

        // module -> (roles allowed to read, roles allowed to write)
        // SuperAdmin is always allowed. EDIT THIS TABLE to change permissions.
        // Which modules a company may use at all is decided by its plan (PlanService).
        private static readonly Dictionary<string, (string[] Read, string[] Write)> Rules =
            new(StringComparer.OrdinalIgnoreCase)
            {
                ["products"] = (Roles(Admin, Manager, Staff), Roles(Manager, Staff)),
                ["inventory"] = (Roles(Admin, Manager, Staff), Roles(Manager, Staff)),
                ["sales"] = (Roles(Admin, Manager, Staff), Roles(Manager, Staff)),
                ["customers"] = (Roles(Manager, Staff), Roles(Manager, Staff)),
                ["reports"] = (Roles(Admin, Manager), Roles(Admin, Manager)),
                ["employees"] = (Roles(Admin, Manager), Roles(Admin, Manager)),
                ["payroll"] = (Roles(Admin, Manager), Roles(Admin, Manager)),
                ["suppliers"] = (Roles(Admin, Manager), Roles(Admin, Manager)),
                ["purchases"] = (Roles(Admin, Manager), Roles(Admin, Manager)),
                ["expenses"] = (Roles(Admin, Manager), Roles(Admin, Manager)),
                ["expense-categories"] = (Roles(Admin, Manager), Roles(Admin, Manager)),
                ["stock-movements"] = (Roles(Admin, Manager), Roles(Admin, Manager)),
                ["branches"] = (Roles(Admin), Roles(Admin)),
                ["approvals"] = (Roles(Admin), Roles(Admin)),
                ["benefits"] = (Roles(Admin), Roles(Admin)),
                ["contribution-rates"] = (Roles(Admin), Roles(Admin)),
            };

        private readonly PlanService _plans;

        public TenantAccessFilter(PlanService plans)
        {
            _plans = plans;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            // /auth/login and anything marked [AllowAnonymous]
            if (context.ActionDescriptor.EndpointMetadata.OfType<IAllowAnonymous>().Any())
                return;

            var http = context.HttpContext;
            var user = http.User;

            if (user.Identity?.IsAuthenticated != true)
            {
                context.Result = new UnauthorizedObjectResult(new { message = "Login required." });
                return;
            }

            var segments = http.Request.Path.Value!
                .Split('/', StringSplitOptions.RemoveEmptyEntries);

            bool isSuperAdmin = user.IsInRole("SuperAdmin");

            // /admin/... has its own [Authorize(Roles = "SuperAdmin")]
            if (segments.Length > 0 &&
                segments[0].Equals("admin", StringComparison.OrdinalIgnoreCase))
                return;

            // Everything outside /tenant/... (companies, company-databases, ...) is SuperAdmin only.
            if (segments.Length < 2 ||
                !segments[0].Equals("tenant", StringComparison.OrdinalIgnoreCase))
            {
                if (!isSuperAdmin)
                    context.Result = Deny("Only the platform admin can do this.");
                return;
            }

            if (isSuperAdmin)
                return;

            // Company check: the token's company must match the route's company.
            var claim = user.FindFirst("companyId")?.Value;
            if (!int.TryParse(segments[1], out var routeCompanyId) ||
                !int.TryParse(claim, out var tokenCompanyId) ||
                routeCompanyId != tokenCompanyId)
            {
                context.Result = Deny("You do not have access to this company.");
                return;
            }

            // /tenant/{id} alone (product count): any user of that company.
            if (segments.Length == 2)
                return;

            // Unknown modules are denied.
            string module = segments[2];

            if (!Rules.TryGetValue(module, out var rule))
            {
                context.Result = Deny("Access denied.");
                return;
            }

            // Plan check: is this module part of the company's plan?
            // The income statement is a separate Premium feature under /reports.
            string planKey = module;

            if (module.Equals("reports", StringComparison.OrdinalIgnoreCase) &&
                segments.Length > 3 &&
                segments[3].Equals("income-statement", StringComparison.OrdinalIgnoreCase))
            {
                planKey = "income-statement";
            }

            var plan = await _plans.GetForCompanyAsync(routeCompanyId);

            if (!plan.Includes(planKey))
            {
                context.Result = Deny(plan.PlanId == null
                    ? "No plan is assigned to this company. Contact support."
                    : $"Your {plan.Name} plan does not include {Pretty(planKey)}. Upgrade to use it.");
                return;
            }

            // Role check per module.
            bool isRead = HttpMethods.IsGet(http.Request.Method) ||
                          HttpMethods.IsHead(http.Request.Method);
            var allowed = isRead ? rule.Read : rule.Write;

            if (!allowed.Any(user.IsInRole))
                context.Result = Deny("Your role is not allowed to do this.");
        }

        // "contribution-rates" -> "Contribution rates"
        private static string Pretty(string key) =>
            char.ToUpper(key[0]) + key[1..].Replace('-', ' ');

        private static IActionResult Deny(string message) =>
            new ObjectResult(new { message }) { StatusCode = StatusCodes.Status403Forbidden };
    }
}