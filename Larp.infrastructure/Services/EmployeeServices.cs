using Larp.domain.Entities;
using Larp.infrastructure.Data;
using Larp.infrastructure.Services;
using Microsoft.EntityFrameworkCore;

namespace Larp.infrastructure.Services
{
    public class EmployeeService
    {
        private readonly ITenantDBContextFactory _tenantFactory;

        public EmployeeService(ITenantDBContextFactory tenantFactory)
        {
            _tenantFactory = tenantFactory;
        }

        public async Task<List<EmployeeEntity>> GetAllAsync(int companyId)
        {
            await using var db =
                await _tenantFactory.CreateAsync(companyId);

            return await db.Employees
                .AsNoTracking()
                .OrderBy(x => x.EmployeeId)
                .ToListAsync();
        }

        public async Task<List<EmployeeEntity>> GetActiveAsync(int companyId)
        {
            await using var db =
                await _tenantFactory.CreateAsync(companyId);

            return await db.Employees
                .AsNoTracking()
                .Where(x => x.IsActive)
                .OrderBy(x => x.LastName)
                .ThenBy(x => x.FirstName)
                .ToListAsync();
        }

        public async Task<EmployeeEntity?> GetByIdAsync(
            int companyId,
            int employeeId)
        {
            await using var db =
                await _tenantFactory.CreateAsync(companyId);

            return await db.Employees
                .AsNoTracking()
                .FirstOrDefaultAsync(
                    x => x.EmployeeId == employeeId);
        }

        public async Task<EmployeeEntity?> GetByCodeAsync(
            int companyId,
            string employeeCode)
        {
            await using var db =
                await _tenantFactory.CreateAsync(companyId);

            return await db.Employees
                .AsNoTracking()
                .FirstOrDefaultAsync(
                    x => x.EmployeeCode == employeeCode);
        }
        public async Task<EmployeeEntity?> LinkAccountAsync(
            int companyId,
            int employeeId,
            string userId,
            string approvalStatus)
        {
            await using var db =
                await _tenantFactory.CreateAsync(companyId);

            var employee =
                await db.Employees.FirstOrDefaultAsync(
                    x => x.EmployeeId == employeeId);

            if (employee == null)
            {
                return null;
            }

            employee.UserId = userId;
            employee.ApprovalStatus = approvalStatus;

            await db.SaveChangesAsync();

            return employee;
        }

        public async Task<EmployeeEntity> CreateAsync(
            int companyId,
            EmployeeEntity employee)
        {
            await using var db =
                await _tenantFactory.CreateAsync(companyId);

            bool exists =
                await db.Employees.AnyAsync(
                    x => x.EmployeeCode == employee.EmployeeCode);

            if (exists)
            {
                throw new InvalidOperationException(
                    "Employee code already exists.");
            }

            employee.CreatedAt = DateTime.UtcNow;
            employee.IsActive = true;
            employee.EmploymentStatus = "Active";
            employee.DateSeparated = null;
            employee.SeparationReason = null;

            db.Employees.Add(employee);

            await db.SaveChangesAsync();

            return employee;
        }

        public async Task<EmployeeEntity?> UpdateAsync(
            int companyId,
            int employeeId,
            EmployeeEntity updatedEmployee)
        {
            await using var db =
                await _tenantFactory.CreateAsync(companyId);

            var employee =
                await db.Employees.FirstOrDefaultAsync(
                    x => x.EmployeeId == employeeId);

            if (employee == null)
            {
                return null;
            }

            bool duplicate =
                await db.Employees.AnyAsync(
                    x =>
                        x.EmployeeCode ==
                        updatedEmployee.EmployeeCode &&
                        x.EmployeeId != employeeId);

            if (duplicate)
            {
                throw new InvalidOperationException(
                    "Employee code already exists.");
            }

            employee.EmployeeCode =
                updatedEmployee.EmployeeCode;

            employee.FirstName =
                updatedEmployee.FirstName;

            employee.LastName =
                updatedEmployee.LastName;

            employee.MiddleName =
                updatedEmployee.MiddleName;

            employee.Position =
                updatedEmployee.Position;

            employee.ContactNumber =
                updatedEmployee.ContactNumber;

            employee.EmailAddress =
                updatedEmployee.EmailAddress;

            employee.Address =
                updatedEmployee.Address;

            employee.DateHired =
                updatedEmployee.DateHired;

            employee.EmploymentStatus =
                updatedEmployee.EmploymentStatus;

            employee.DateSeparated =
                updatedEmployee.DateSeparated;

            employee.SeparationReason =
                updatedEmployee.SeparationReason;

            employee.IsActive =
                updatedEmployee.EmploymentStatus == "Active";

            await db.SaveChangesAsync();

            return employee;
        }

        public async Task<EmployeeEntity?> ReactivateAsync(
            int companyId,
            int employeeId)
        {
            await using var db =
                await _tenantFactory.CreateAsync(companyId);

            var employee =
                await db.Employees
                    .FirstOrDefaultAsync(x =>
                        x.EmployeeId == employeeId);

            if (employee == null)
            {
                return null;
            }

            employee.EmploymentStatus = "Active";
            employee.IsActive = true;

            await db.SaveChangesAsync();

            return employee;
        }

        public async Task<EmployeeEntity?> SeparateAsync(
            int companyId,
            int employeeId,
            string status,
            string? reason,
            DateTime separationDate)
        {
            if (status != "Resigned" &&
                status != "Fired")
            {
                throw new InvalidOperationException(
                    "Invalid separation status.");
            }

            await using var db =
                await _tenantFactory.CreateAsync(companyId);

            var employee =
                await db.Employees.FirstOrDefaultAsync(
                    x => x.EmployeeId == employeeId);

            if (employee == null)
            {
                return null;
            }

            employee.EmploymentStatus = status;
            employee.DateSeparated = separationDate;
            employee.SeparationReason = reason;
            employee.IsActive = false;

            await db.SaveChangesAsync();

            return employee;
        }

        public async Task<bool> DeleteAsync(
            int companyId,
            int employeeId)
        {
            await using var db =
                await _tenantFactory.CreateAsync(companyId);

            var employee =
                await db.Employees.FirstOrDefaultAsync(
                    x => x.EmployeeId == employeeId);

            if (employee == null)
            {
                return false;
            }

            employee.IsActive = false;

            if (employee.EmploymentStatus == "Active")
            {
                employee.EmploymentStatus = "Resigned";
                employee.DateSeparated = DateTime.UtcNow;
            }

            await db.SaveChangesAsync();

            return true;
        }
    }
}