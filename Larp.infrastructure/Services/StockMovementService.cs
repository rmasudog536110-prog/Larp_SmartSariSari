using Larp.domain.Entities;
using Larp.infrastructure.Services;
using Microsoft.EntityFrameworkCore;

namespace Larp.infrastructure.Services
{
    public class StockMovementService
    {
        private readonly ITenantDBContextFactory _tenantFactory;

        public StockMovementService(
            ITenantDBContextFactory tenantFactory)
        {
            _tenantFactory = tenantFactory;
        }

        public async Task<List<StockMovement>> GetByProductAsync(
            int companyId,
            int productId)
        {
            await using var db =
                await _tenantFactory.CreateAsync(companyId);

            return await db.StockMovements
                .AsNoTracking()
                .Include(x => x.Product)
                .Include(x => x.Employee)
                .Include(x => x.Purchase)
                .Where(x => x.ProductId == productId)
                .OrderByDescending(x => x.MovementDate)
                .ThenByDescending(x => x.StockMovementId)
                .ToListAsync();
        }

        public async Task<StockMovement?> GetByIdAsync(
            int companyId,
            int movementId)
        {
            await using var db =
                await _tenantFactory.CreateAsync(companyId);

            return await db.StockMovements
                .AsNoTracking()
                .Include(x => x.Product)
                .Include(x => x.Employee)
                .Include(x => x.Purchase)
                .FirstOrDefaultAsync(
                    x => x.StockMovementId == movementId);
        }

        public async Task<List<StockMovement>> GetByDateRangeAsync(
            int companyId,
            DateTime from,
            DateTime to)
        {
            await using var db =
                await _tenantFactory.CreateAsync(companyId);

            var startDate = from.Date;
            var endDate = to.Date.AddDays(1);

            return await db.StockMovements
                .AsNoTracking()
                .Include(x => x.Product)
                .Include(x => x.Employee)
                .Include(x => x.Purchase)
                .Where(x =>
                    x.MovementDate >= startDate &&
                    x.MovementDate < endDate)
                .OrderByDescending(x => x.MovementDate)
                .ThenByDescending(x => x.StockMovementId)
                .ToListAsync();
        }
    }
}