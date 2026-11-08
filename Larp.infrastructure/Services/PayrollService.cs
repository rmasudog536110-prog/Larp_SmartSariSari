using Larp.domain.DTO;
using Larp.domain.Entities;
using Larp.infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Larp.infrastructure.Services
{
    public class PayrollService
    {
        // Paid every 15 days: 2 pay periods per month.
        private const int PeriodsPerMonth = 2;

        private readonly ITenantDBContextFactory _tenantFactory;
        private readonly PayrollCalculator _calculator;

        public PayrollService(
            ITenantDBContextFactory tenantFactory,
            PayrollCalculator calculator)
        {
            _tenantFactory = tenantFactory;
            _calculator = calculator;
        }

        private static readonly Expression<Func<PayrollEntity, PayrollDto>> ToDto =
            x => new PayrollDto
            {
                PayrollId = x.PayrollId,
                EmployeeId = x.EmployeeId,
                EmployeeCode = x.Employee!.EmployeeCode,
                EmployeeName = x.Employee.FirstName + " " + x.Employee.LastName,
                Position = x.Employee.Position,
                PayPeriodStart = x.PayPeriodStart,
                PayPeriodEnd = x.PayPeriodEnd,
                BasicSalary = x.BasicSalary,
                Allowances = x.Allowances,
                Deductions = x.Deductions,
                SssEmployee = x.SssEmployee,
                SssEmployer = x.SssEmployer,
                PhilHealthEmployee = x.PhilHealthEmployee,
                PhilHealthEmployer = x.PhilHealthEmployer,
                PagibigEmployee = x.PagibigEmployee,
                PagibigEmployer = x.PagibigEmployer,
                NetPay = x.NetPay,
                Status = x.Status,
                BranchId = x.BranchId,
                BranchName = x.Branch != null ? x.Branch.BranchName : null,
                CreatedAt = x.CreatedAt
            };

        public async Task<List<PayrollDto>> GetAllAsync(int companyId)
        {
            await using var db = await _tenantFactory.CreateAsync(companyId);

            return await db.Payrolls
                .AsNoTracking()
                .Where(x => x.Status == "Approved" || x.Status == "Paid")
                .Select(ToDto)
                .ToListAsync();
        }

        public async Task<PayrollPreviewDto> PreviewAsync(
            int companyId, int employeeId, DateTime start, DateTime end, decimal allowances)
        {
            await using var db = await _tenantFactory.CreateAsync(companyId);

            var employee = await db.Employees.AsNoTracking()
                .FirstOrDefaultAsync(x => x.EmployeeId == employeeId)
                ?? throw new InvalidOperationException("Employee not found.");

            var preview = new PayrollPreviewDto
            {
                EmployeeId = employee.EmployeeId,
                EmployeeCode = employee.EmployeeCode,
                EmployeeName = $"{employee.FirstName} {employee.LastName}".Trim(),
                Position = employee.Position,
                PayPeriodStart = start,
                PayPeriodEnd = end,
                Allowances = allowances
            };

            if (!employee.IsActive)
            { preview.Message = "This employee is inactive."; return preview; }

            if (end < start)
            { preview.Message = "Pay period end cannot be earlier than the start."; return preview; }

            if (allowances < 0)
            { preview.Message = "Allowances cannot be negative."; return preview; }

            var draft = new PayrollEntity
            {
                EmployeeId = employeeId,
                PayPeriodStart = start,
                PayPeriodEnd = end,
                Allowances = allowances
            };

            if (await db.Payrolls.AnyAsync(x => x.EmployeeId == employeeId &&
                    x.PayPeriodStart == start && x.PayPeriodEnd == end))
            { preview.Message = "Payroll for this employee and pay period already exists."; return preview; }

            try
            {
                await ApplyBenefitsAsync(db, draft);   // same calculation as saving
            }
            catch (InvalidOperationException ex)
            {
                preview.Message = ex.Message;
                return preview;
            }

            preview.BasicSalary = draft.BasicSalary;
            preview.SssEmployee = draft.SssEmployee;
            preview.PhilHealthEmployee = draft.PhilHealthEmployee;
            preview.PagibigEmployee = draft.PagibigEmployee;
            preview.Deductions = draft.Deductions;
            preview.NetPay = draft.NetPay;
            preview.Ready = true;

            return preview;
        }

        public async Task<PayrollDto?> GetByIdAsync(int companyId, int payrollId)
        {
            await using var db = await _tenantFactory.CreateAsync(companyId);

            return await db.Payrolls
                .AsNoTracking()
                .Where(x => x.PayrollId == payrollId)
                .Select(ToDto)
                .FirstOrDefaultAsync();
        }

        public async Task<PayrollDto> CreateAsync(
            int companyId,
            PayrollEntity payroll,
            bool autoApprove = false)
        {
            await using var db = await _tenantFactory.CreateAsync(companyId);

            ValidatePayroll(payroll);
            await EnsureEmployeeCanBePaidAsync(db, payroll.EmployeeId);
            await EnsureNoDuplicateAsync(db, payroll, null);

            // Everything except employee, period and allowances is server-controlled.
            payroll.PayrollId = 0;
            payroll.Employee = null;
            payroll.Branch = null;
            payroll.BranchId = await db.Employees.AsNoTracking()
                .Where(x => x.EmployeeId == payroll.EmployeeId)
                .Select(x => x.BranchId)
                .FirstOrDefaultAsync();

            await ApplyBenefitsAsync(db, payroll);

            payroll.Status = autoApprove ? "Approved" : "Pending";
            payroll.CreatedAt = DateTime.UtcNow;

            db.Payrolls.Add(payroll);
            await db.SaveChangesAsync();

            return await db.Payrolls
                .AsNoTracking()
                .Where(x => x.PayrollId == payroll.PayrollId)
                .Select(ToDto)
                .FirstAsync();
        }

        public async Task<PayrollDto?> UpdateAsync(
            int companyId,
            int payrollId,
            PayrollEntity updated)
        {
            await using var db = await _tenantFactory.CreateAsync(companyId);

            var payroll = await db.Payrolls
                .FirstOrDefaultAsync(x => x.PayrollId == payrollId);

            if (payroll == null)
                return null;

            ValidatePayroll(updated);

            if (payroll.Status == "Pending")
            {
                // Still a draft: recompute from the employee's current benefits.
                await EnsureEmployeeCanBePaidAsync(db, updated.EmployeeId);
                await EnsureNoDuplicateAsync(db, updated, payrollId);

                payroll.EmployeeId = updated.EmployeeId;
                payroll.BranchId = await db.Employees.AsNoTracking()
                    .Where(x => x.EmployeeId == payroll.EmployeeId)
                    .Select(x => x.BranchId)
                    .FirstOrDefaultAsync();
                payroll.PayPeriodStart = updated.PayPeriodStart;
                payroll.PayPeriodEnd = updated.PayPeriodEnd;
                payroll.Allowances = updated.Allowances;

                await ApplyBenefitsAsync(db, payroll);
            }
            else
            {
                // Approved or paid: salary and contributions stay frozen.
                payroll.Allowances = updated.Allowances;
                payroll.NetPay =
                    payroll.BasicSalary + payroll.Allowances - payroll.Deductions;

                EnsureNetPayNotNegative(payroll);
            }

            if (!string.IsNullOrWhiteSpace(updated.Status))
                payroll.Status = updated.Status;

            await db.SaveChangesAsync();

            return await db.Payrolls
                .AsNoTracking()
                .Where(x => x.PayrollId == payroll.PayrollId)
                .Select(ToDto)
                .FirstAsync();
        }

        public async Task<bool> DeleteAsync(int companyId, int payrollId)
        {
            await using var db = await _tenantFactory.CreateAsync(companyId);

            var payroll = await db.Payrolls
                .FirstOrDefaultAsync(x => x.PayrollId == payrollId);

            if (payroll == null)
                return false;

            db.Payrolls.Remove(payroll);
            await db.SaveChangesAsync();

            return true;
        }

        // ---------------- Helpers ----------------

        private async Task ApplyBenefitsAsync(
            TenantErpDbContext db,
            PayrollEntity payroll)
        {
            var benefits = await db.EmployeeBenefits
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.EmployeeId == payroll.EmployeeId);

           if (benefits == null || benefits.MonthlySalary <= 0)
           {
               throw new InvalidOperationException(
                    "Set this employee's monthly salary in Benefits first.");
           }

            var rates = await db.ContributionRates
                .AsNoTracking()
                .ToListAsync();

            decimal monthly = benefits.MonthlySalary;
            DateTime asOf = payroll.PayPeriodEnd;

            var sss = _calculator.Contribution(rates, "SSS", monthly, asOf);
            var phil = _calculator.Contribution(rates, "PhilHealth", monthly, asOf);
            var pagibig = _calculator.Contribution(rates, "Pagibig", monthly, asOf);

            payroll.BasicSalary = PerPeriod(monthly);

            payroll.SssEmployee = PerPeriod(sss.Employee);
            payroll.SssEmployer = PerPeriod(sss.Employer);
            payroll.PhilHealthEmployee = PerPeriod(phil.Employee);
            payroll.PhilHealthEmployer = PerPeriod(phil.Employer);
            payroll.PagibigEmployee = PerPeriod(pagibig.Employee);
            payroll.PagibigEmployer = PerPeriod(pagibig.Employer);

            // Only the employee shares come out of pay.
            payroll.Deductions =
                payroll.SssEmployee +
                payroll.PhilHealthEmployee +
                payroll.PagibigEmployee;

            payroll.NetPay =
                payroll.BasicSalary + payroll.Allowances - payroll.Deductions;

            EnsureNetPayNotNegative(payroll);
        }

        private static decimal PerPeriod(decimal monthlyAmount) =>
            Math.Round(monthlyAmount / PeriodsPerMonth, 2, MidpointRounding.AwayFromZero);

        private static async Task EnsureEmployeeCanBePaidAsync(
            TenantErpDbContext db, int employeeId)
        {
            var employee = await db.Employees
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.EmployeeId == employeeId);

            if (employee == null)
                throw new InvalidOperationException("Employee not found.");

            if (!employee.IsActive)
                throw new InvalidOperationException(
                    "Cannot create payroll for an inactive employee.");
        }

        private static async Task EnsureNoDuplicateAsync(
            TenantErpDbContext db, PayrollEntity payroll, int? excludePayrollId)
        {
            bool exists = await db.Payrolls.AnyAsync(x =>
                x.EmployeeId == payroll.EmployeeId &&
                x.PayPeriodStart == payroll.PayPeriodStart &&
                x.PayPeriodEnd == payroll.PayPeriodEnd &&
                (excludePayrollId == null || x.PayrollId != excludePayrollId));

            if (exists)
                throw new InvalidOperationException(
                    "Payroll for this employee and pay period already exists.");
        }

        private static void EnsureNetPayNotNegative(PayrollEntity payroll)
        {
            if (payroll.NetPay < 0)
                throw new InvalidOperationException("Net pay cannot be negative.");
        }

        // BasicSalary and Deductions are no longer client input, so only
        // the fields the client still sends are checked here.
        private static void ValidatePayroll(PayrollEntity payroll)
        {
            if (payroll.EmployeeId <= 0)
                throw new InvalidOperationException(
                    "Employee ID must be greater than zero.");

            if (payroll.PayPeriodEnd < payroll.PayPeriodStart)
                throw new InvalidOperationException(
                    "Pay period end cannot be earlier than pay period start.");

            if (payroll.Allowances < 0)
                throw new InvalidOperationException(
                    "Allowances cannot be negative.");
        }
    }
}