using Larp.domain.Entities;

namespace Larp.infrastructure.Services
{
    public class PayrollCalculator
    {
        public (decimal Employee, decimal Employer) Contribution(
            IEnumerable<ContributionRate> rates,
            string type,
            decimal monthlySalary,
            DateTime asOf)
        {
            var rate = rates
                .Where(r =>
                    string.Equals(r.Type, type, StringComparison.OrdinalIgnoreCase) &&
                    r.EffectiveFrom <= asOf &&
                    monthlySalary >= r.MinSalary &&
                    monthlySalary <= r.MaxSalary)
                .OrderByDescending(r => r.EffectiveFrom)
                .FirstOrDefault();

            if (rate == null)
            {
                throw new InvalidOperationException(
                    $"No {type} rate matches a monthly salary of " +
                    $"{monthlySalary:N2} as of {asOf:yyyy-MM-dd}. " +
                    "Add it in Contribution Rates first.");
            }

            decimal employee = rate.IsPercentage
                ? monthlySalary * rate.EmployeeShare
                : rate.EmployeeShare;

            decimal employer = rate.IsPercentage
                ? monthlySalary * rate.EmployerShare
                : rate.EmployerShare;

            if (rate.MaxContribution is decimal cap)
            {
                employee = Math.Min(employee, cap);
                employer = Math.Min(employer, cap);
            }

            return (
                Math.Round(employee, 2, MidpointRounding.AwayFromZero),
                Math.Round(employer, 2, MidpointRounding.AwayFromZero));
        }
    }
}