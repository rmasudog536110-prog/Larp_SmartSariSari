using Larp.domain.Entities;
using Larp.infrastructure.Data;
using Larp.infrastructure.Services;
using Microsoft.EntityFrameworkCore;

namespace Larp.infrastructure.Services
{
    public class ExpenseService
    {
        private readonly ITenantDBContextFactory _tenantFactory;

        public ExpenseService(
            ITenantDBContextFactory tenantFactory)
        {
            _tenantFactory = tenantFactory;
        }

        public async Task<List<ExpenseCategory>> GetCategoriesAsync(
            int companyId)
        {
            await using var db =
                await _tenantFactory.CreateAsync(companyId);

            return await db.ExpenseCategories
                .AsNoTracking()
                .Where(x => x.IsActive)
                .OrderBy(x => x.CategoryName)
                .ToListAsync();
        }

        public async Task<ExpenseCategory?> GetCategoryByIdAsync(
            int companyId,
            int categoryId)
        {
            await using var db =
                await _tenantFactory.CreateAsync(companyId);

            return await db.ExpenseCategories
                .AsNoTracking()
                .FirstOrDefaultAsync(
                    x => x.ExpenseCategoryId == categoryId);
        }

        public async Task<ExpenseCategory> CreateCategoryAsync(
            int companyId,
            ExpenseCategory category)
        {
            await using var db =
                await _tenantFactory.CreateAsync(companyId);

            if (string.IsNullOrWhiteSpace(category.CategoryName))
            {
                throw new InvalidOperationException(
                    "Category name is required.");
            }

            category.CategoryName = category.CategoryName.Trim();

            bool exists =
                await db.ExpenseCategories.AnyAsync(
                    x => x.CategoryName == category.CategoryName);

            if (exists)
            {
                throw new InvalidOperationException(
                    "Expense category already exists.");
            }

            category.ExpenseCategoryId = 0;
            category.Expenses = new List<Expense>();
            category.CreatedAt = DateTime.UtcNow;
            category.IsActive = true;

            db.ExpenseCategories.Add(category);

            await db.SaveChangesAsync();

            return category;
        }

        public async Task<List<Expense>> GetAllAsync(
            int companyId)
        {
            await using var db =
                await _tenantFactory.CreateAsync(companyId);

            return await db.Expenses
                .AsNoTracking()
                .Include(x => x.ExpenseCategory)
                .Include(x => x.Branch)
                .OrderByDescending(x => x.ExpenseDate)
                .ThenByDescending(x => x.ExpenseId)
                .ToListAsync();
        }

        public async Task<List<Expense>> GetByDateRangeAsync(
            int companyId,
            DateTime from,
            DateTime to)
        {
            await using var db =
                await _tenantFactory.CreateAsync(companyId);

            var startDate = from.Date;
            var endDate = to.Date.AddDays(1);

            return await db.Expenses
                .AsNoTracking()
                .Include(x => x.ExpenseCategory)
                .Include(x => x.Branch)
                .Where(x =>
                    x.ExpenseDate >= startDate &&
                    x.ExpenseDate < endDate)
                .OrderByDescending(x => x.ExpenseDate)
                .ThenByDescending(x => x.ExpenseId)
                .ToListAsync();
        }

        public async Task<Expense?> GetByIdAsync(
            int companyId,
            int expenseId)
        {
            await using var db =
                await _tenantFactory.CreateAsync(companyId);

            return await db.Expenses
                .AsNoTracking()
                .Include(x => x.ExpenseCategory)
                .Include(x => x.Branch)
                .FirstOrDefaultAsync(
                    x => x.ExpenseId == expenseId);
        }

        /// <summary>
        /// The branch is the creating employee's branch. Only when
        /// canChooseBranch is true (Admin) is the branch on the request used.
        /// </summary>
        public async Task<Expense> CreateAsync(
            int companyId,
            Expense expense,
            int? employeeId = null,
            bool canChooseBranch = false)
        {
            await using var db =
                await _tenantFactory.CreateAsync(companyId);

            bool categoryExists =
                await db.ExpenseCategories.AnyAsync(
                    x =>
                        x.ExpenseCategoryId ==
                        expense.ExpenseCategoryId &&
                        x.IsActive);

            if (!categoryExists)
            {
                throw new InvalidOperationException(
                    "Expense category does not exist.");
            }

            if (expense.Amount <= 0)
            {
                throw new InvalidOperationException(
                    "Expense amount must be greater than zero.");
            }

            expense.ExpenseId = 0;
            expense.ExpenseCategory = null;
            expense.Branch = null;
            expense.CreatedAt = DateTime.UtcNow;

            if (expense.ExpenseDate == default)
            {
                expense.ExpenseDate = DateTime.UtcNow;
            }

            if (string.IsNullOrWhiteSpace(
                expense.PaymentMethod))
            {
                expense.PaymentMethod = "Cash";
            }

            if (string.IsNullOrWhiteSpace(expense.Status))
            {
                expense.Status = "Paid";
            }

            expense.BranchId = await ResolveCreateBranchAsync(
                db, expense.BranchId, canChooseBranch, employeeId);

            db.Expenses.Add(expense);

            await db.SaveChangesAsync();

            return await db.Expenses
                .Include(x => x.ExpenseCategory)
                .Include(x => x.Branch)
                .FirstAsync(
                    x => x.ExpenseId == expense.ExpenseId);
        }

        /// <summary>
        /// Only an Admin (canChooseBranch) can change the branch of an existing
        /// expense. For everyone else the branch stays as it is.
        /// </summary>
        public async Task<Expense?> UpdateAsync(
            int companyId,
            int expenseId,
            Expense updatedExpense,
            bool canChooseBranch = false)
        {
            await using var db =
                await _tenantFactory.CreateAsync(companyId);

            var expense =
                await db.Expenses.FirstOrDefaultAsync(
                    x => x.ExpenseId == expenseId);

            if (expense == null)
            {
                return null;
            }

            bool categoryExists =
                await db.ExpenseCategories.AnyAsync(
                    x =>
                        x.ExpenseCategoryId ==
                        updatedExpense.ExpenseCategoryId &&
                        x.IsActive);

            if (!categoryExists)
            {
                throw new InvalidOperationException(
                    "Expense category does not exist.");
            }

            if (updatedExpense.Amount <= 0)
            {
                throw new InvalidOperationException(
                    "Expense amount must be greater than zero.");
            }

            if (canChooseBranch)
            {
                // Keeping the current branch is always allowed, even if it
                // was deactivated later. A different branch must be active.
                if (updatedExpense.BranchId.HasValue &&
                    updatedExpense.BranchId != expense.BranchId)
                {
                    await EnsureBranchUsableAsync(
                        db, updatedExpense.BranchId.Value);
                }

                expense.BranchId = updatedExpense.BranchId;
            }

            expense.ExpenseCategoryId =
                updatedExpense.ExpenseCategoryId;

            expense.Description =
                updatedExpense.Description;

            expense.Amount =
                updatedExpense.Amount;

            expense.ExpenseDate =
                updatedExpense.ExpenseDate;

            expense.PaymentMethod =
                updatedExpense.PaymentMethod;

            expense.Status =
                updatedExpense.Status;

            await db.SaveChangesAsync();

            return await db.Expenses
                .Include(x => x.ExpenseCategory)
                .Include(x => x.Branch)
                .FirstAsync(
                    x => x.ExpenseId == expenseId);
        }

        public async Task<bool> DeleteAsync(
            int companyId,
            int expenseId)
        {
            await using var db =
                await _tenantFactory.CreateAsync(companyId);

            var expense =
                await db.Expenses.FirstOrDefaultAsync(
                    x => x.ExpenseId == expenseId);

            if (expense == null)
            {
                return false;
            }

            db.Expenses.Remove(expense);

            await db.SaveChangesAsync();

            return true;
        }

        // ---------------- Helpers ----------------

        private static async Task<int?> ResolveCreateBranchAsync(
            TenantErpDbContext db,
            int? requestedBranchId,
            bool canChooseBranch,
            int? employeeId)
        {
            if (canChooseBranch && requestedBranchId.HasValue)
            {
                await EnsureBranchUsableAsync(db, requestedBranchId.Value);
                return requestedBranchId;
            }

            if (employeeId.HasValue)
            {
                return await db.Employees
                    .AsNoTracking()
                    .Where(x => x.EmployeeId == employeeId.Value)
                    .Select(x => x.BranchId)
                    .FirstOrDefaultAsync();
            }

            return null;
        }

        private static async Task EnsureBranchUsableAsync(
            TenantErpDbContext db, int branchId)
        {
            bool ok = await db.Branches.AnyAsync(
                x => x.BranchId == branchId && x.IsActive);

            if (!ok)
            {
                throw new InvalidOperationException(
                    "Branch does not exist or is inactive.");
            }
        }
    }
}