using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Larp.domain.Entities;
using System.Collections.Generic;
using System.Text;

namespace Larp.infrastructure.Data
{
    public class MasterErpDbContext : IdentityDbContext<ApplicationUser>
    {
        private const string BasicModules =
            "products,inventory,sales,customers,stock-movements,reports";

        private const string PremiumModules = BasicModules +
            ",suppliers,purchases,expenses,expense-categories,employees,payroll," +
            "approvals,benefits,contribution-rates,income-statement";

        private const string EnterpriseModules = PremiumModules + ",branches";

        public MasterErpDbContext(DbContextOptions<MasterErpDbContext> options) : base(options)
        {
        }

        public DbSet<Company> Companies => Set<Company>();

        public DbSet<CompanyDatabase> CompanyDatabases => Set<CompanyDatabase>();

        public DbSet<TermsAndConditions> TermsAndConditions { get; set; }

        public DbSet<Device> Devices { get; set; }

        public DbSet<Plan> Plans => Set<Plan>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>(entity =>
            {
                entity.Property(x => x.FullName).HasMaxLength(200).IsRequired();

                entity.HasOne(x => x.Company)
                    .WithMany()
                    .HasForeignKey(x => x.CompanyId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<Plan>(entity =>
            {
                entity.HasKey(p => p.PlanId);
                entity.Property(p => p.Name).IsRequired().HasMaxLength(50);
                entity.HasIndex(p => p.Name).IsUnique();
                entity.Property(p => p.Description).HasMaxLength(200);
                entity.Property(p => p.MonthlyPrice).HasPrecision(18, 2);
                entity.Property(p => p.YearlyPrice).HasPrecision(18, 2);
                entity.Property(p => p.Modules).IsRequired().HasMaxLength(1000);

                var seededOn = new DateTime(2026, 10, 4, 0, 0, 0, DateTimeKind.Utc);

                entity.HasData(
                    new Plan
                    {
                        PlanId = 1,
                        Name = "Basic",
                        Description = "Micro store",
                        MonthlyPrice = 499m,
                        YearlyPrice = 4990m,
                        MaxEmployees = 5,
                        MaxBranches = 0,
                        Modules = BasicModules,
                        IsActive = true,
                        SortOrder = 1,
                        CreatedAt = seededOn
                    },
                    new Plan
                    {
                        PlanId = 2,
                        Name = "Premium",
                        Description = "Small company",
                        MonthlyPrice = 1499m,
                        YearlyPrice = 14990m,
                        MaxEmployees = 25,
                        MaxBranches = 0,
                        Modules = PremiumModules,
                        IsActive = true,
                        SortOrder = 2,
                        CreatedAt = seededOn
                    },
                    new Plan
                    {
                        PlanId = 3,
                        Name = "Enterprise",
                        Description = "Medium company",
                        MonthlyPrice = 3999m,
                        YearlyPrice = 39990m,
                        MaxEmployees = 100,
                        MaxBranches = 10,
                        Modules = EnterpriseModules,
                        IsActive = true,
                        SortOrder = 3,
                        CreatedAt = seededOn
                    });
            });

            builder.Entity<Company>(entity =>
            {
                entity.HasKey(e => e.CompanyId);
                entity.Property(e => e.CompanyCode).IsRequired().HasMaxLength(50);
                entity.Property(e => e.CompanyName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.isActive).IsRequired();
                entity.Property(e => e.isCreated).IsRequired();

                entity.Property(e => e.SubscriptionStatus).IsRequired().HasMaxLength(20);
                entity.Property(e => e.SubscriptionPlan).HasMaxLength(50);
                entity.Property(e => e.SubscriptionExpiry);

                entity.Property(e => e.Address).HasMaxLength(300);
                entity.Property(e => e.Tin).HasMaxLength(30);
                entity.Property(e => e.ContactNumber).HasMaxLength(30);

                entity.HasOne(e => e.Plan)
                    .WithMany()
                    .HasForeignKey(e => e.PlanId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<CompanyDatabase>(entity =>
            {
                entity.HasKey(e => e.CompanyDatabaseId);
                entity.Property(e => e.ServerName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.DatabaseName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.CredentialKey).IsRequired().HasMaxLength(100);
                entity.Property(e => e.isActive).IsRequired();
                entity.HasOne(d => d.Company)
                      .WithMany()
                      .HasForeignKey(d => d.CompanyId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<Device>(entity =>

            {
                entity.HasKey(x => x.DeviceId);
                entity.Property(x => x.DeviceCode).HasMaxLength(50).IsRequired();
                entity.Property(x => x.DeviceName).HasMaxLength(200).IsRequired();
                entity.HasOne(x => x.Company).WithMany(x => x.Devices).HasForeignKey(x => x.CompanyId).OnDelete(DeleteBehavior.Restrict);
                entity.HasIndex(x => new { x.CompanyId, x.DeviceCode }).IsUnique();
                entity.Property(x => x.DeviceType).HasMaxLength(50);
                entity.Property(x => x.ActivationCode).HasMaxLength(50).IsRequired();
                entity.HasIndex(x => x.ActivationCode).IsUnique().HasFilter("[ActivationCode] <> ''");

            });


        }
    }
}