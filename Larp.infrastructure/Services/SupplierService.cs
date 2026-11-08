using Larp.domain.Entities;
using Larp.infrastructure.Data;
using Larp.infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Text.RegularExpressions;

namespace Larp.infrastructure.Services
{
    public class SupplierService
    {
        // Matches SUP-0007, SUP-7 and SUP007, so numbering continues after older codes.
        private static readonly Regex CodePattern =
            new(@"^SUP-?(\d+)$", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        private readonly ITenantDBContextFactory _tenantFactory;

        public SupplierService(ITenantDBContextFactory tenantFactory)
        {
            _tenantFactory = tenantFactory;
        }

        public async Task<List<Supplier>> GetAllAsync(int companyId)
        {
            await using var db =
                await _tenantFactory.CreateAsync(companyId);

            return await db.Suppliers
                .AsNoTracking()
                .Where(x => x.ApprovalStatus == "Approved")
                .OrderBy(x => x.SupplierId)
                .ToListAsync();
        }

        public async Task<Supplier?> GetByIdAsync(
            int companyId,
            int supplierId)
        {
            await using var db =
                await _tenantFactory.CreateAsync(companyId);

            return await db.Suppliers
                .AsNoTracking()
                .FirstOrDefaultAsync(x =>
                    x.SupplierId == supplierId);
        }

        public async Task<Supplier?> GetByCodeAsync(
            int companyId,
            string supplierCode)
        {
            await using var db =
                await _tenantFactory.CreateAsync(companyId);

            return await db.Suppliers
                .AsNoTracking()
                .FirstOrDefaultAsync(x =>
                    x.SupplierCode == supplierCode);
        }

        // The code is always generated here: SUP-0001, SUP-0002, ...
        // Whatever code the client sends is ignored.
        public async Task<Supplier> CreateAsync(
            int companyId,
            Supplier supplier)
        {
            if (string.IsNullOrWhiteSpace(supplier.SupplierName))
            {
                throw new InvalidOperationException(
                    "Supplier name is required.");
            }

            await using var db =
                await _tenantFactory.CreateAsync(companyId);

            // Serializable, so two people saving at the same moment cannot get the same number.
            await using var transaction =
                await db.Database.BeginTransactionAsync(IsolationLevel.Serializable);

            var codes = await db.Suppliers
                .Select(x => x.SupplierCode)
                .ToListAsync();

            int last = codes
                .Select(code => CodePattern.Match(code ?? string.Empty))
                .Where(match => match.Success)
                .Select(match => int.TryParse(match.Groups[1].Value, out var n) ? n : 0)
                .DefaultIfEmpty(0)
                .Max();

            supplier.SupplierId = 0;
            supplier.SupplierCode = $"SUP-{last + 1:D4}";
            supplier.SupplierName = supplier.SupplierName.Trim();
            supplier.CreatedAt = DateTime.UtcNow;
            supplier.IsActive = true;

            db.Suppliers.Add(supplier);

            await db.SaveChangesAsync();
            await transaction.CommitAsync();

            return supplier;
        }

        // supplierCode is accepted so existing callers keep compiling, but it is
        // ignored: a supplier's code never changes after it is created.
        public async Task<Supplier?> UpdateAsync(
            int companyId,
            int supplierId,
            string supplierCode,
            string supplierName,
            string? contactPerson,
            string? contactNumber,
            string? emailAddress,
            string? address,
            bool isActive)
        {
            if (string.IsNullOrWhiteSpace(supplierName))
            {
                throw new InvalidOperationException(
                    "Supplier name is required.");
            }

            await using var db =
                await _tenantFactory.CreateAsync(companyId);

            var supplier = await db.Suppliers
                .FirstOrDefaultAsync(x =>
                    x.SupplierId == supplierId);

            if (supplier == null)
                return null;

            supplier.SupplierName = supplierName.Trim();
            supplier.ContactPerson = contactPerson;
            supplier.ContactNumber = contactNumber;
            supplier.EmailAddress = emailAddress;
            supplier.Address = address;
            supplier.IsActive = isActive;

            await db.SaveChangesAsync();

            return supplier;
        }

        public async Task<bool> DeleteAsync(
            int companyId,
            int supplierId)
        {
            await using var db =
                await _tenantFactory.CreateAsync(companyId);

            var supplier = await db.Suppliers
                .FirstOrDefaultAsync(x =>
                    x.SupplierId == supplierId);

            if (supplier == null)
                return false;

            supplier.IsActive = false;

            await db.SaveChangesAsync();

            return true;
        }
    }
}