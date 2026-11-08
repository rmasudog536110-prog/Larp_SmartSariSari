using System.Diagnostics;
using Larp.infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Larp.infrastructure.Services
{
    public class TenantDBContextFactory : ITenantDBContextFactory
    {
        private readonly ITenantDBResolver _resolver;
        private readonly IConfiguration _configuration;

        public TenantDBContextFactory(
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

            var userId = _configuration[
                $"TenantCredentials:{databaseInfo.CredentialKey}:UserId"];

            var password = _configuration[
                $"TenantCredentials:{databaseInfo.CredentialKey}:Password"];

            if (string.IsNullOrWhiteSpace(userId) ||
                string.IsNullOrWhiteSpace(password))
            {
                throw new InvalidOperationException(
                    $"Credentials not found for key '{databaseInfo.CredentialKey}'.");
            }

            var connectionString =
                $"Server={databaseInfo.ServerName};" +
                $"Database={databaseInfo.DatabaseName};" +
                $"User Id={userId};" +
                $"Password={password};" +
                $"Encrypt=True;" +
                $"TrustServerCertificate=True;" +
                $"MultipleActiveResultSets=True;";

            Debug.WriteLine("========================================");
            Debug.WriteLine($"COMPANY ID: {companyId}");
            Debug.WriteLine($"SERVER: {databaseInfo.ServerName}");
            Debug.WriteLine($"DATABASE: {databaseInfo.DatabaseName}");
            Debug.WriteLine($"CREDENTIAL KEY: {databaseInfo.CredentialKey}");
            Debug.WriteLine("========================================");

            var options =
                new DbContextOptionsBuilder<TenantErpDbContext>()
                    .UseSqlServer(connectionString)
                    .Options;

            return new TenantErpDbContext(options);
        }
    }
}