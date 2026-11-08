using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Larp.infrastructure.Data
{
    public class TenantErpDbContextFactory
        : IDesignTimeDbContextFactory<TenantErpDbContext>
    {
        public TenantErpDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(
                    Path.Combine(
                        Directory.GetCurrentDirectory(),
                        "../Larp.api"))
                .AddJsonFile("appsettings.json", optional: false)
                .AddJsonFile(
                    "appsettings.Development.json",
                    optional: true)
                .Build();

            var connectionString =
                configuration.GetConnectionString("SmallTenantErp");

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InvalidOperationException(
                    "SmallTenantErp connection string was not found.");
            }

            var options =
                new DbContextOptionsBuilder<TenantErpDbContext>()
                    .UseSqlServer(connectionString)
                    .Options;

            return new TenantErpDbContext(options);
        }
    }
}