using Larp.domain.DTO;
using Microsoft.EntityFrameworkCore;

namespace Larp.infrastructure.Services
{
    public class ApprovalService
    {
        private readonly ITenantDBContextFactory _tenantFactory;

        public ApprovalService(ITenantDBContextFactory tenantFactory)
        {
            _tenantFactory = tenantFactory;
        }

        public async Task<List<ApprovalItemDto>> GetPendingAsync(int companyId)
        {
            await using var db = await _tenantFactory.CreateAsync(companyId);

            var items = new List<ApprovalItemDto>();

            var employees = await db.Employees
                .AsNoTracking()
                .Where(e => e.ApprovalStatus == "Pending")
                .ToListAsync();

            items.AddRange(employees.Select(e => new ApprovalItemDto
            {
                Type = "Employee",
                ItemId = e.EmployeeId,
                Title = $"{e.FirstName} {e.LastName}",
                Details = $"{e.Position} ({e.EmployeeCode})",
                RequestedAt = e.CreatedAt
            }));

            var suppliers = await db.Suppliers
                .AsNoTracking()
                .Where(s => s.ApprovalStatus == "Pending")
                .ToListAsync();

            items.AddRange(suppliers.Select(s => new ApprovalItemDto
            {
                Type = "Supplier",
                ItemId = s.SupplierId,
                Title = s.SupplierName,
                Details = string.IsNullOrWhiteSpace(s.ContactPerson)
                    ? $"Code {s.SupplierCode}"
                    : $"Code {s.SupplierCode}, contact {s.ContactPerson}",
                RequestedAt = s.CreatedAt
            }));

            var payrolls = await db.Payrolls
                .AsNoTracking()
                .Include(p => p.Employee)
                .Where(p => p.Status == "Pending")
                .ToListAsync();

            items.AddRange(payrolls.Select(p => new ApprovalItemDto
            {
                Type = "Payroll",
                ItemId = p.PayrollId,
                Title = p.Employee != null
                    ? $"{p.Employee.FirstName} {p.Employee.LastName}"
                    : $"Employee {p.EmployeeId}",
                Details = $"Pay period {p.PayPeriodStart:yyyy-MM-dd} to {p.PayPeriodEnd:yyyy-MM-dd}, net pay {p.NetPay:N2}",
                RequestedAt = p.CreatedAt
            }));

            return items.OrderBy(x => x.RequestedAt).ToList();
        }

        /// <summary>Returns false if the item does not exist.</summary>
        public Task<bool> ApproveAsync(int companyId, string type, int itemId) =>
            ChangeAsync(companyId, type, itemId, approve: true);

        /// <summary>Returns false if the item does not exist.</summary>
        public Task<bool> RejectAsync(int companyId, string type, int itemId) =>
            ChangeAsync(companyId, type, itemId, approve: false);

        private async Task<bool> ChangeAsync(int companyId, string type, int itemId, bool approve)
        {
            await using var db = await _tenantFactory.CreateAsync(companyId);

            switch ((type ?? string.Empty).Trim().ToLowerInvariant())
            {
                case "employee":
                    {
                        var employee = await db.Employees.FirstOrDefaultAsync(x => x.EmployeeId == itemId);
                        if (employee == null) return false;

                        if (employee.ApprovalStatus != "Pending")
                            throw new InvalidOperationException("This employee is not waiting for approval.");

                        employee.ApprovalStatus = approve ? "Approved" : "Rejected";
                        if (!approve) employee.IsActive = false;
                        break;
                    }

                case "supplier":
                    {
                        var supplier = await db.Suppliers.FirstOrDefaultAsync(x => x.SupplierId == itemId);
                        if (supplier == null) return false;

                        if (supplier.ApprovalStatus != "Pending")
                            throw new InvalidOperationException("This supplier is not waiting for approval.");

                        supplier.ApprovalStatus = approve ? "Approved" : "Rejected";
                        if (!approve) supplier.IsActive = false;
                        break;
                    }

                case "payroll":
                    {
                        var payroll = await db.Payrolls.FirstOrDefaultAsync(x => x.PayrollId == itemId);
                        if (payroll == null) return false;

                        if (payroll.Status != "Pending")
                            throw new InvalidOperationException("This payroll record is not waiting for approval.");

                        payroll.Status = approve ? "Approved" : "Rejected";
                        break;
                    }

                default:
                    throw new InvalidOperationException("Unknown item type.");
            }

            await db.SaveChangesAsync();
            return true;
        }
    }
}