using Larp.domain.DTO;

namespace Larp.infrastructure.Services
{
    public interface IReportService
    {
        Task<SalesReportRequest> GetSalesReportAsync(
            int companyId,
            DateTime startDate,
            DateTime endDate);

        Task<IncomeStatementDto> GetIncomeStatementAsync(
            int companyId,
            DateTime startDate,
            DateTime endDate);
    }
}