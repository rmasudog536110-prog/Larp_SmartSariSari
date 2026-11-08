using System;
using System.Collections.Generic;
using System.Text;

namespace Larp.infrastructure.Services
{
    public interface ITenantDBResolver
    {
        Task<TenantDBInfo> GetDatabaseInfoAsync(int companyId);
    }
}
