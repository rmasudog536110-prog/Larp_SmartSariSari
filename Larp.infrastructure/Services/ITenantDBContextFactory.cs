using Larp.infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Larp.infrastructure.Services
{
    public interface ITenantDBContextFactory
    {
        Task<TenantErpDbContext> CreateAsync(int companyId);
    }
}
