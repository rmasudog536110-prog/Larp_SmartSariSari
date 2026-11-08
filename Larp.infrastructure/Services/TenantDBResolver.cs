using Larp.infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Larp.infrastructure.Services
{
    public class TenantDBResolver : ITenantDBResolver
    {
        private readonly MasterErpDbContext _masterDb;

        public TenantDBResolver(MasterErpDbContext masterDb)
        {
            _masterDb = masterDb;
        }

        public async Task<TenantDBInfo> GetDatabaseInfoAsync(int companyId)
        {
            var tenantDatabase = await _masterDb.CompanyDatabases.AsNoTracking().FirstOrDefaultAsync(x =>x.CompanyId == companyId && x.isActive);

            if (tenantDatabase == null)
            {
                throw new InvalidOperationException(
                    $"No active tenant database found for CompanyId {companyId}.");
            }

            return new TenantDBInfo
            {
                ServerName = tenantDatabase.ServerName,
                DatabaseName = tenantDatabase.DatabaseName,
                CredentialKey = tenantDatabase.CredentialKey
            };
        }

    }
}
