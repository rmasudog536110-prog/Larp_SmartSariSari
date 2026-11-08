using Larp.domain.Entities;
using Larp.infrastructure.Data;
using Larp.infrastructure.Services;
using Microsoft.EntityFrameworkCore;

namespace Larp.infrastructure.Services
{
    public class CustomerService
    {
        private readonly ITenantDBContextFactory _tenantFactory;

        public CustomerService(ITenantDBContextFactory tenantFactory)
        {
            _tenantFactory = tenantFactory;
        }

        public async Task<List<Customer>> GetAllAsync(int companyId)
        {
            await using var db = await _tenantFactory.CreateAsync(companyId);

            return await db.Customers
                .AsNoTracking()
                .OrderBy(x => x.CustomerId)
                .ToListAsync();
        }

        public async Task<Customer?> GetByIdAsync(
            int companyId,
            int customerId)
        {
            await using var db = await _tenantFactory.CreateAsync(companyId);

            return await db.Customers
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.CustomerId == customerId);
        }

        public async Task<Customer> CreateAsync(
            int companyId,
            Customer customer)
        {
            await using var db = await _tenantFactory.CreateAsync(companyId);

            var exists = await db.Customers
                .AnyAsync(x => x.CustomerCode == customer.CustomerCode);

            if (exists)
                throw new InvalidOperationException(
                    "Customer code already exists.");

            customer.CreatedAt = DateTime.UtcNow;
            customer.IsActive = true;

            db.Customers.Add(customer);
            await db.SaveChangesAsync();

            return customer;
        }

        public async Task<Customer?> UpdateAsync(
            int companyId,
            int customerId,
            string customerCode,
            string customerName,
            string? contactNumber,
            string? emailAddress,
            string? address,
            bool isActive)
        {
            await using var db = await _tenantFactory.CreateAsync(companyId);

            var customer = await db.Customers
                .FirstOrDefaultAsync(x => x.CustomerId == customerId);

            if (customer == null)
                return null;

            var duplicate = await db.Customers
                .AnyAsync(x =>
                    x.CustomerCode == customerCode &&
                    x.CustomerId != customerId);

            if (duplicate)
                throw new InvalidOperationException(
                    "Customer code already exists.");

            customer.CustomerCode = customerCode;
            customer.CustomerName = customerName;
            customer.ContactNumber = contactNumber;
            customer.EmailAddress = emailAddress;
            customer.Address = address;
            customer.IsActive = isActive;

            await db.SaveChangesAsync();

            return customer;
        }

        public async Task<bool> DeleteAsync(
            int companyId,
            int customerId)
        {
            await using var db = await _tenantFactory.CreateAsync(companyId);

            var customer = await db.Customers
                .FirstOrDefaultAsync(x => x.CustomerId == customerId);

            if (customer == null)
                return false;

            customer.IsActive = false;

            await db.SaveChangesAsync();

            return true;
        }
    }
}