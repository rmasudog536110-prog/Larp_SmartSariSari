using Larp.domain.DTO;
using Larp.domain.Entities;
using Larp.infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Larp.infrastructure.Services
{
    public class BranchService
    {
        private readonly ITenantDBContextFactory _tenantFactory;

        public BranchService(ITenantDBContextFactory tenantFactory)
        {
            _tenantFactory = tenantFactory;
        }

        public async Task<List<BranchDto>> GetAllAsync(int companyId)
        {
            await using var db = await _tenantFactory.CreateAsync(companyId);

            return await LoadDtosAsync(db, null);
        }

        public async Task<BranchDto?> GetByIdAsync(int companyId, int branchId)
        {
            await using var db = await _tenantFactory.CreateAsync(companyId);

            var list = await LoadDtosAsync(db, branchId);
            return list.FirstOrDefault();
        }

        public async Task<BranchDto> CreateAsync(int companyId, SaveBranchRequest request)
        {
            Validate(request);

            await using var db = await _tenantFactory.CreateAsync(companyId);

            var code = request.BranchCode.Trim();

            if (await db.Branches.AnyAsync(x => x.BranchCode == code))
                throw new InvalidOperationException("Branch code already exists.");

            var branch = new Branch
            {
                BranchCode = code,
                BranchName = request.BranchName.Trim(),
                Address = Clean(request.Address),
                ContactNumber = Clean(request.ContactNumber),
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            db.Branches.Add(branch);
            await db.SaveChangesAsync();

            return (await LoadDtosAsync(db, branch.BranchId)).First();
        }

        public async Task<BranchDto?> UpdateAsync(
            int companyId,
            int branchId,
            SaveBranchRequest request)
        {
            Validate(request);

            await using var db = await _tenantFactory.CreateAsync(companyId);

            var branch = await db.Branches.FirstOrDefaultAsync(x => x.BranchId == branchId);
            if (branch == null) return null;

            var code = request.BranchCode.Trim();

            if (await db.Branches.AnyAsync(x => x.BranchCode == code && x.BranchId != branchId))
                throw new InvalidOperationException("Branch code already exists.");

            branch.BranchCode = code;
            branch.BranchName = request.BranchName.Trim();
            branch.Address = Clean(request.Address);
            branch.ContactNumber = Clean(request.ContactNumber);
            branch.IsActive = request.IsActive;

            await db.SaveChangesAsync();

            return (await LoadDtosAsync(db, branchId)).First();
        }

        // Soft delete, like suppliers.
        public async Task<bool> DeactivateAsync(int companyId, int branchId)
        {
            await using var db = await _tenantFactory.CreateAsync(companyId);

            var branch = await db.Branches.FirstOrDefaultAsync(x => x.BranchId == branchId);
            if (branch == null) return false;

            branch.IsActive = false;
            await db.SaveChangesAsync();

            return true;
        }

        /// <summary>
        /// Moves an employee to a branch. Returns false if the branch or employee
        /// does not exist. Sales recorded afterwards by that employee use this branch.
        /// </summary>
        public async Task<bool> AssignEmployeeAsync(int companyId, int branchId, int employeeId)
        {
            await using var db = await _tenantFactory.CreateAsync(companyId);

            var branch = await db.Branches.FirstOrDefaultAsync(x => x.BranchId == branchId);
            if (branch == null) return false;

            if (!branch.IsActive)
                throw new InvalidOperationException("Cannot assign employees to an inactive branch.");

            var employee = await db.Employees.FirstOrDefaultAsync(x => x.EmployeeId == employeeId);
            if (employee == null) return false;

            employee.BranchId = branchId;
            await db.SaveChangesAsync();

            return true;
        }

        // ---------------- Helpers ----------------

        private static async Task<List<BranchDto>> LoadDtosAsync(TenantErpDbContext db, int? onlyBranchId)
        {
            var query = db.Branches.AsNoTracking();
            if (onlyBranchId.HasValue)
                query = query.Where(x => x.BranchId == onlyBranchId.Value);

            var branches = await query.OrderBy(x => x.BranchName).ToListAsync();

            var employeeCounts = await db.Employees
                .Where(e => e.BranchId != null && e.IsActive)
                .GroupBy(e => e.BranchId)
                .Select(g => new { BranchId = g.Key, Count = g.Count() })
                .ToListAsync();

            var salesTotals = await db.Sales
                .Where(s => s.BranchId != null && s.Status != "Cancelled")
                .GroupBy(s => s.BranchId)
                .Select(g => new { BranchId = g.Key, Count = g.Count(), Total = g.Sum(x => x.TotalAmount) })
                .ToListAsync();

            return branches.Select(b =>
            {
                var employees = employeeCounts.FirstOrDefault(x => x.BranchId == b.BranchId);
                var sales = salesTotals.FirstOrDefault(x => x.BranchId == b.BranchId);

                return new BranchDto
                {
                    BranchId = b.BranchId,
                    BranchCode = b.BranchCode,
                    BranchName = b.BranchName,
                    Address = b.Address,
                    ContactNumber = b.ContactNumber,
                    IsActive = b.IsActive,
                    EmployeeCount = employees?.Count ?? 0,
                    SalesCount = sales?.Count ?? 0,
                    SalesTotal = sales?.Total ?? 0m
                };
            }).ToList();
        }

        private static void Validate(SaveBranchRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.BranchCode))
                throw new InvalidOperationException("Branch code is required.");

            if (string.IsNullOrWhiteSpace(request.BranchName))
                throw new InvalidOperationException("Branch name is required.");

            if (request.BranchCode.Trim().Length > 30)
                throw new InvalidOperationException("Branch code is too long (30 characters max).");

            if (request.BranchName.Trim().Length > 150)
                throw new InvalidOperationException("Branch name is too long (150 characters max).");
        }

        private static string? Clean(string? value) =>
            string.IsNullOrWhiteSpace(value) ? null : value.Trim();
    }
}