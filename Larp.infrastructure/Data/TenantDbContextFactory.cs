using Larp.infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Larp.infrastructure.Services
{
    public class TenantDbContextFactory : ITenantDBContextFactory
    {
        private readonly ITenantDBResolver _resolver;
        private readonly IConfiguration _configuration;

        public TenantDbContextFactory(
            ITenantDBResolver resolver,
            IConfiguration configuration)
        {
            _resolver = resolver;
            _configuration = configuration;
        }

        public async Task<TenantErpDbContext> CreateAsync(int companyId)
        {
            var databaseInfo =
                await _resolver.GetDatabaseInfoAsync(companyId);

            var userId =
                _configuration[
                    $"TenantCredentials:{databaseInfo.CredentialKey}:UserId"];

            var password =
                _configuration[
                    $"TenantCredentials:{databaseInfo.CredentialKey}:Password"];

            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new InvalidOperationException(
                    $"UserId not found for credential key '{databaseInfo.CredentialKey}'.");
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new InvalidOperationException(
                    $"Password not found for credential key '{databaseInfo.CredentialKey}'.");
            }

            var connectionString =
                $"Server={databaseInfo.ServerName};" +
                $"Database={databaseInfo.DatabaseName};" +
                $"User Id={userId};" +
                $"Password={password};" +
                $"Encrypt=True;" +
                $"TrustServerCertificate=True;" +
                $"MultipleActiveResultSets=True;";

            var options =
                new DbContextOptionsBuilder<TenantErpDbContext>()
                    .UseSqlServer(connectionString)
                    .Options;

            return new TenantErpDbContext(options);
        }
    }
}