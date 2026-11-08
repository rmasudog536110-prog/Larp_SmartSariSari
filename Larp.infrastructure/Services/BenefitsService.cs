using Larp.domain.DTO;
using Larp.domain.Entities;
using Larp.infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Larp.infrastructure.Services
{
    public class BenefitsService
    {
        private static readonly string[] ValidTypes = { "SSS", "PhilHealth", "Pagibig" };

        private readonly ITenantDBContextFactory _tenantFactory;

        public BenefitsService(ITenantDBContextFactory tenantFactory)
        {
            _tenantFactory = tenantFactory;
        }

        public async Task<BenefitsDto?> GetAsync(int companyId, int employeeId)
        {
            await using var db = await _tenantFactory.CreateAsync(companyId);

            return await db.EmployeeBenefits
                .AsNoTracking()
                .Where(x => x.EmployeeId == employeeId)
                .Select(x => new BenefitsDto
                {
                    EmployeeId = x.EmployeeId,
                    SssNumber = x.SssNumber,
                    PhilHealthNumber = x.PhilHealthNumber,
                    PagibigNumber = x.PagibigNumber,
                    MonthlySalary = x.MonthlySalary
                })
                .FirstOrDefaultAsync();
        }

        public async Task<BenefitsDto> SaveAsync(
            int companyId, int employeeId, SaveBenefitsRequest request)
        {
            if (request.MonthlySalary < 0)
                throw new InvalidOperationException("Monthly salary cannot be negative.");

            await using var db = await _tenantFactory.CreateAsync(companyId);

            if (!await db.Employees.AnyAsync(x => x.EmployeeId == employeeId))
                throw new InvalidOperationException("Employee not found.");

            var benefits = await db.EmployeeBenefits
                .FirstOrDefaultAsync(x => x.EmployeeId == employeeId);

            if (benefits == null)
            {
                benefits = new EmployeeBenefits { EmployeeId = employeeId };
                db.EmployeeBenefits.Add(benefits);
            }

            benefits.SssNumber = request.SssNumber?.Trim();
            benefits.PhilHealthNumber = request.PhilHealthNumber?.Trim();
            benefits.PagibigNumber = request.PagibigNumber?.Trim();
            benefits.MonthlySalary = request.MonthlySalary;

            await db.SaveChangesAsync();

            return new BenefitsDto
            {
                EmployeeId = employeeId,
                SssNumber = benefits.SssNumber,
                PhilHealthNumber = benefits.PhilHealthNumber,
                PagibigNumber = benefits.PagibigNumber,
                MonthlySalary = benefits.MonthlySalary
            };
        }

        // ---------------- Contribution rates ----------------

        public async Task<List<ContributionRate>> GetRatesAsync(int companyId)
        {
            await using var db = await _tenantFactory.CreateAsync(companyId);

            return await db.ContributionRates
                .AsNoTracking()
                .OrderBy(x => x.Type)
                .ThenBy(x => x.MinSalary)
                .ThenByDescending(x => x.EffectiveFrom)
                .ToListAsync();
        }

        public async Task<ContributionRate> CreateRateAsync(
            int companyId, ContributionRate rate)
        {
            ValidateRate(rate);

            await using var db = await _tenantFactory.CreateAsync(companyId);

            rate.ContributionRateId = 0;
            db.ContributionRates.Add(rate);
            await db.SaveChangesAsync();

            return rate;
        }

        public async Task<ContributionRate?> UpdateRateAsync(
            int companyId, int id, ContributionRate updated)
        {
            ValidateRate(updated);

            await using var db = await _tenantFactory.CreateAsync(companyId);

            var rate = await db.ContributionRates
                .FirstOrDefaultAsync(x => x.ContributionRateId == id);

            if (rate == null) return null;

            rate.Type = updated.Type;
            rate.MinSalary = updated.MinSalary;
            rate.MaxSalary = updated.MaxSalary;
            rate.IsPercentage = updated.IsPercentage;
            rate.EmployeeShare = updated.EmployeeShare;
            rate.EmployerShare = updated.EmployerShare;
            rate.MaxContribution = updated.MaxContribution;
            rate.EffectiveFrom = updated.EffectiveFrom;

            await db.SaveChangesAsync();
            return rate;
        }

        public async Task<bool> DeleteRateAsync(int companyId, int id)
        {
            await using var db = await _tenantFactory.CreateAsync(companyId);

            var rate = await db.ContributionRates
                .FirstOrDefaultAsync(x => x.ContributionRateId == id);

            if (rate == null) return false;

            db.ContributionRates.Remove(rate);
            await db.SaveChangesAsync();
            return true;
        }

        private static void ValidateRate(ContributionRate rate)
        {
            var type = ValidTypes.FirstOrDefault(t =>
                string.Equals(t, rate.Type?.Trim(), StringComparison.OrdinalIgnoreCase));

            if (type == null)
                throw new InvalidOperationException(
                    "Type must be SSS, PhilHealth or Pagibig.");

            rate.Type = type;

            if (rate.MinSalary < 0 || rate.MaxSalary < rate.MinSalary)
                throw new InvalidOperationException(
                    "Max salary must be greater than or equal to min salary.");

            if (rate.EmployeeShare < 0 || rate.EmployerShare < 0)
                throw new InvalidOperationException("Shares cannot be negative.");

            // Guards against typing 2.5 instead of 0.025 for 2.5%.
            if (rate.IsPercentage && (rate.EmployeeShare > 1 || rate.EmployerShare > 1))
                throw new InvalidOperationException(
                    "Percentage shares must be written as decimals (2.5% = 0.025).");

            if (rate.MaxContribution < 0)
                throw new InvalidOperationException("Cap cannot be negative.");
        }
    }
}