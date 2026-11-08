using Larp.domain.DTO;
using Larp.domain.Entities;
using Larp.infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Larp.infrastructure.Services
{
    public class TermsAndConditionsService
    {
        private readonly MasterErpDbContext _db;

        public TermsAndConditionsService(MasterErpDbContext db)
        {
            _db = db;
        }

        public async Task<TermsAndConditionsDto?> GetActiveAsync()
        {
            var terms = await _db.TermsAndConditions
                .Where(x => x.IsActive)
                .OrderByDescending(x => x.UpdatedAt)
                .FirstOrDefaultAsync();

            if (terms == null)
                return null;

            return ToDto(terms);
        }

        public async Task<TermsAndConditionsDto> SaveAsync(
            SaveTermsAndConditionsRequest request)
        {
            var terms = await _db.TermsAndConditions
                .Where(x => x.IsActive)
                .OrderByDescending(x => x.UpdatedAt)
                .FirstOrDefaultAsync();

            if (terms == null)
            {
                terms = new TermsAndConditions();

                _db.TermsAndConditions.Add(terms);
            }

            terms.Version = request.Version.Trim();
            terms.EffectiveDate = request.EffectiveDate;
            terms.Content = request.Content;
            terms.IsActive = request.IsActive;
            terms.UpdatedAt = DateTime.UtcNow;

            await _db.SaveChangesAsync();

            return ToDto(terms);
        }

        private static TermsAndConditionsDto ToDto(
            TermsAndConditions terms)
        {
            return new TermsAndConditionsDto
            {
                TermsAndConditionsId = terms.TermsAndConditionsId,
                Version = terms.Version,
                EffectiveDate = terms.EffectiveDate,
                Content = terms.Content,
                IsActive = terms.IsActive,
                UpdatedAt = terms.UpdatedAt
            };
        }
    }
}