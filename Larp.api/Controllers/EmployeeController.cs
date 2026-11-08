using Larp.domain.DTO;
using Larp.domain.Entities;
using Larp.infrastructure.Data;
using Larp.infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Larp.api.Controllers
{
    [ApiController]
    [Route("tenant/{companyId}/employees")]
    public class EmployeeController : ControllerBase
    {
        private static readonly string[] RolePriority =
            { "SuperAdmin", "Admin", "Manager", "Staff" };

        private readonly EmployeeService _employeeService;
        private readonly MasterErpDbContext _masterDb;
        private readonly UserManager<ApplicationUser> _users;

        private readonly PlanService _plans;

        public EmployeeController(
            EmployeeService employeeService,
            MasterErpDbContext masterDb,
            UserManager<ApplicationUser> users,
            PlanService plans
            )
        {
            _employeeService = employeeService;
            _masterDb = masterDb;
            _users = users;
            _plans = plans;
        }

        // ---------------- Visibility ----------------
        // Admin sees Managers and Staff (not Admins).
        // Manager sees Staff only.
        // Employees without a login are visible to both, so a login can be created.

        private bool CanSee(string? targetRole)
        {
            if (User.IsInRole("SuperAdmin")) return true;
            if (string.IsNullOrEmpty(targetRole)) return true;

            if (User.IsInRole("Admin"))
                return targetRole == "Manager" || targetRole == "Staff";

            if (User.IsInRole("Manager"))
                return targetRole == "Staff";

            return false;
        }

        private async Task<Dictionary<string, string>> LoadRolesAsync(
            IEnumerable<EmployeeEntity> employees)
        {
            var ids = employees
                .Where(e => !string.IsNullOrEmpty(e.UserId))
                .Select(e => e.UserId!)
                .Distinct()
                .ToList();

            if (ids.Count == 0)
            {
                return new Dictionary<string, string>();
            }

            var rows = await (
                from ur in _masterDb.UserRoles
                join r in _masterDb.Roles on ur.RoleId equals r.Id
                where ids.Contains(ur.UserId)
                select new { ur.UserId, Role = r.Name })
                .ToListAsync();

            return rows
                .GroupBy(x => x.UserId)
                .ToDictionary(
                    g => g.Key,
                    g => RolePriority.FirstOrDefault(
                        p => g.Any(x => x.Role == p)) ?? string.Empty);
        }

        private static string? RoleOf(
            EmployeeEntity employee,
            Dictionary<string, string> roles)
        {
            if (!string.IsNullOrEmpty(employee.UserId) &&
                roles.TryGetValue(employee.UserId, out var role) &&
                role.Length > 0)
            {
                return role;
            }

            return null;
        }

        private async Task<List<EmployeeEntity>> FilterVisibleAsync(
            List<EmployeeEntity> employees)
        {
            var roles = await LoadRolesAsync(employees);
            var visible = new List<EmployeeEntity>();

            foreach (var employee in employees)
            {
                employee.Role = RoleOf(employee, roles);

                if (CanSee(employee.Role))
                {
                    visible.Add(employee);
                }
            }

            return visible;
        }

        private async Task<bool> IsHiddenAsync(int companyId, int employeeId)
        {
            var employee =
                await _employeeService.GetByIdAsync(companyId, employeeId);

            if (employee == null)
            {
                return false;
            }

            var roles = await LoadRolesAsync(new[] { employee });

            return !CanSee(RoleOf(employee, roles));
        }

        private ObjectResult NoAccess() =>
            StatusCode(403, new
            {
                message = "You do not have access to this employee."
            });

        // ---------------- Endpoints ----------------

        [HttpGet]
        public async Task<ActionResult<List<EmployeeEntity>>> GetAll(
            int companyId)
        {
            var employees =
                await _employeeService.GetAllAsync(companyId);

            return Ok(await FilterVisibleAsync(employees));
        }

        [HttpGet("active")]
        public async Task<ActionResult<List<EmployeeEntity>>> GetActive(
            int companyId)
        {
            var employees =
                await _employeeService.GetActiveAsync(companyId);

            return Ok(await FilterVisibleAsync(employees));
        }

        [HttpGet("{employeeId:int}")]
        public async Task<ActionResult<EmployeeEntity>> GetById(
            int companyId,
            int employeeId)
        {
            var employee =
                await _employeeService.GetByIdAsync(
                    companyId,
                    employeeId);

            if (employee == null)
            {
                return NotFound();
            }

            var visible = await FilterVisibleAsync(
                new List<EmployeeEntity> { employee });

            if (visible.Count == 0)
            {
                return NotFound();
            }

            return Ok(visible[0]);
        }

        [HttpPost]
        public async Task<ActionResult<EmployeeEntity>> Create(
            int companyId,
            [FromBody] EmployeeEntity employee)
        {
            try
            {
                employee.ApprovalStatus =
                    User.IsInRole("Admin") || User.IsInRole("SuperAdmin")
                        ? "Approved"
                        : "Pending";

                var limit = await _plans.CheckEmployeeLimitAsync(companyId);
                if (limit != null) return StatusCode(403, new { message = limit });

                var created =
                    await _employeeService.CreateAsync(
                        companyId,
                        employee);

                return Ok(created);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new
                {
                    message = ex.Message
                });
            }
        }

        // Creates a login for an employee that does not have one yet.
        [HttpPost("{employeeId:int}/account")]
        public async Task<IActionResult> CreateAccount(
            int companyId,
            int employeeId,
            [FromBody] CreateEmployeeAccountRequest request)
        {
            string[] allowed =
                User.IsInRole("SuperAdmin") ? new[] { "Admin", "Manager", "Staff" } :
                User.IsInRole("Admin") ? new[] { "Manager", "Staff" } :
                User.IsInRole("Manager") ? new[] { "Staff" } :
                Array.Empty<string>();

            if (request == null || !allowed.Contains(request.Role))
            {
                return StatusCode(403, new
                {
                    message = "You cannot create a login with that role."
                });
            }

            string username = request.Username?.Trim() ?? string.Empty;

            if (username.Length == 0 || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest(new
                {
                    message = "Username and password are required."
                });
            }

            var employee =
                await _employeeService.GetByIdAsync(companyId, employeeId);

            if (employee == null)
            {
                return NotFound(new { message = "Employee not found." });
            }

            if (!employee.IsActive)
            {
                return BadRequest(new
                {
                    message = "Cannot create a login for an inactive employee."
                });
            }

            if (!string.IsNullOrEmpty(employee.UserId))
            {
                return Conflict(new
                {
                    message = "This employee already has a login."
                });
            }

            if (await _users.FindByNameAsync(username) != null)
            {
                return Conflict(new { message = "Username already exists." });
            }

            var user = new ApplicationUser
            {
                UserName = username,
                Email = string.IsNullOrWhiteSpace(employee.EmailAddress)
                    ? null
                    : employee.EmailAddress,
                FullName = $"{employee.FirstName} {employee.LastName}".Trim(),
                CompanyId = companyId,
                IsActive = true
            };

            var created = await _users.CreateAsync(user, request.Password);

            if (!created.Succeeded)
            {
                return BadRequest(new
                {
                    message = string.Join(
                        "; ",
                        created.Errors.Select(e => e.Description))
                });
            }

            try
            {
                await _users.AddToRoleAsync(user, request.Role);

                // A Manager's request waits for an Admin to approve it.
                string status =
                    User.IsInRole("Manager") ? "Pending" : "Approved";

                await _employeeService.LinkAccountAsync(
                    companyId,
                    employeeId,
                    user.Id,
                    status);

                return Ok(new
                {
                    employeeId,
                    username,
                    role = request.Role,
                    approvalStatus = status
                });
            }
            catch (Exception ex)
            {
                await _users.DeleteAsync(user);

                return StatusCode(500, new
                {
                    message = "Could not link the login: " + ex.Message
                });
            }
        }

        [HttpPut("{employeeId:int}")]
        public async Task<ActionResult<EmployeeEntity>> Update(
            int companyId,
            int employeeId,
            [FromBody] EmployeeEntity employee)
        {
            if (await IsHiddenAsync(companyId, employeeId))
            {
                return NoAccess();
            }

            try
            {
                var updated =
                    await _employeeService.UpdateAsync(
                        companyId,
                        employeeId,
                        employee);

                if (updated == null)
                {
                    return NotFound();
                }

                return Ok(updated);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new
                {
                    message = ex.Message
                });
            }
        }

        [HttpPut("{employeeId:int}/separate")]
        public async Task<ActionResult<EmployeeEntity>> Separate(
            int companyId,
            int employeeId,
            [FromBody] SeparationRequest request)
        {
            if (await IsHiddenAsync(companyId, employeeId))
            {
                return NoAccess();
            }

            try
            {
                var employee =
                    await _employeeService.SeparateAsync(
                        companyId,
                        employeeId,
                        request.Status,
                        request.Reason,
                        request.SeparationDate);

                if (employee == null)
                {
                    return NotFound();
                }

                return Ok(employee);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }

        [HttpPut("{employeeId:int}/reactivate")]
        public async Task<ActionResult<EmployeeEntity>> Reactivate(
            int companyId,
            int employeeId)
        {
            if (await IsHiddenAsync(companyId, employeeId))
            {
                return NoAccess();
            }

            var current = await _employeeService.GetByIdAsync(companyId, employeeId);
            if (current != null && !current.IsActive)
            {
                var limit = await _plans.CheckEmployeeLimitAsync(companyId);
                if (limit != null) return StatusCode(403, new { message = limit });
            }

            try
            {
                var employee =
                    await _employeeService.ReactivateAsync(
                        companyId,
                        employeeId);

                if (employee == null)
                {
                    return NotFound(new
                    {
                        message = "Employee not found."
                    });
                }

                return Ok(employee);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }

        [HttpDelete("{employeeId:int}")]
        public async Task<IActionResult> Delete(
            int companyId,
            int employeeId)
        {
            if (await IsHiddenAsync(companyId, employeeId))
            {
                return NoAccess();
            }

            var deleted =
                await _employeeService.DeleteAsync(
                    companyId,
                    employeeId);

            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }

    public class SeparationRequest
    {
        public string Status { get; set; } = string.Empty;

        public string? Reason { get; set; }

        public DateTime SeparationDate { get; set; }
    }
}