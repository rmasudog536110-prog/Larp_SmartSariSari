using Larp.domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Larp.infrastructure.Data
{
    public static class IdentitySeeder
    {
        public static readonly string[] Roles =
            { "SuperAdmin", "Admin", "Manager", "Staff" };

        public static async Task SeedAsync(IServiceProvider services)
        {
            using var scope = services.CreateScope();

            var roleManager = scope.ServiceProvider
                .GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = scope.ServiceProvider
                .GetRequiredService<UserManager<ApplicationUser>>();
            var config = scope.ServiceProvider
                .GetRequiredService<IConfiguration>();

            foreach (var role in Roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));
            }

            var userName = config["PlatformAdmin:UserName"];
            var password = config["PlatformAdmin:Password"];

            if (string.IsNullOrWhiteSpace(userName) ||
                string.IsNullOrWhiteSpace(password))
            {
                throw new InvalidOperationException(
                    "PlatformAdmin:UserName and PlatformAdmin:Password must be configured.");
            }

            var admin = await userManager.FindByNameAsync(userName);

            if (admin == null)
            {
                admin = new ApplicationUser
                {
                    UserName = userName,
                    FullName = "Platform Admin",
                    CompanyId = null,   // SuperAdmin belongs to no company
                    IsActive = true
                };

                var result = await userManager.CreateAsync(admin, password);

                if (!result.Succeeded)
                {
                    throw new InvalidOperationException(
                        "Could not create SuperAdmin: " +
                        string.Join("; ", result.Errors.Select(e => e.Description)));
                }
            }

            if (!await userManager.IsInRoleAsync(admin, "SuperAdmin"))
                await userManager.AddToRoleAsync(admin, "SuperAdmin");
        }
    }
}