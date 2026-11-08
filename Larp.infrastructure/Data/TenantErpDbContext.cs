using Larp.domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Larp.infrastructure.Data
{
    public class TenantErpDbContext : IdentityDbContext
    {
        public TenantErpDbContext(
            DbContextOptions<TenantErpDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products => Set<Product>();
        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<Supplier> Suppliers => Set<Supplier>();
        public DbSet<Inventory> Inventory => Set<Inventory>();
        public DbSet<ExpenseCategory> ExpenseCategories => Set<ExpenseCategory>();
        public DbSet<Expense> Expenses => Set<Expense>();
        public DbSet<EmployeeEntity> Employees => Set<EmployeeEntity>();
        public DbSet<Sales> Sales => Set<Sales>();
        public DbSet<SaleItem> SaleItems => Set<SaleItem>();
        public DbSet<EmployeeBenefits> EmployeeBenefits => Set<EmployeeBenefits>();
        public DbSet<ContributionRate> ContributionRates => Set<ContributionRate>();

        public DbSet<PayrollEntity> Payrolls => Set<PayrollEntity>();

        public DbSet<StockMovement> StockMovements { get; set; }

        public DbSet<Purchase> Purchases => Set<Purchase>();
        public DbSet<PurchaseItem> PurchaseItems => Set<PurchaseItem>();

        public DbSet<Branch> Branches => Set<Branch>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Product>(entity =>
            {
                entity.HasKey(x => x.ProductId);

                entity.Property(x => x.ProductCode)
                    .HasMaxLength(50)
                    .IsRequired();

                entity.Property(x => x.ProductName)
                    .HasMaxLength(200)
                    .IsRequired();

                entity.Property(x => x.UnitPrice)
                    .HasPrecision(18, 2);

                entity.HasIndex(x => x.ProductCode)
                    .IsUnique();
            });

            builder.Entity<Branch>(entity =>
            {
                entity.HasKey(x => x.BranchId);

                entity.Property(x => x.BranchCode)
                    .HasMaxLength(30)
                    .IsRequired();

                entity.Property(x => x.BranchName)
                    .HasMaxLength(150)
                    .IsRequired();

                entity.Property(x => x.Address)
                    .HasMaxLength(300);

                entity.Property(x => x.ContactNumber)
                    .HasMaxLength(30);

                entity.HasIndex(x => x.BranchCode)
                    .IsUnique();
            });

            builder.Entity<EmployeeEntity>(entity =>
            {
                entity.HasKey(x => x.EmployeeId);

                entity.Property(x => x.EmployeeCode)
                   .HasMaxLength(50)
                   .IsRequired();

                entity.Property(x => x.FirstName)
                    .HasMaxLength(100)
                    .IsRequired();

                entity.Property(x => x.LastName)
                  .HasMaxLength(100)
                  .IsRequired();

                entity.Property(x => x.MiddleName)
                 .HasMaxLength(100);

                entity.Property(x => x.Position)
                  .HasMaxLength(100)
                  .IsRequired();

                entity.Property(x => x.ContactNumber)
                    .HasMaxLength(30);

                entity.Property(x => x.EmailAddress)
                    .HasMaxLength(150);

                entity.Property(x => x.Address)
                    .HasMaxLength(300);

                entity.Property(x => x.EmploymentStatus)
                    .HasMaxLength(30)
                    .IsRequired();

                entity.Property(x => x.SeparationReason)
                    .HasMaxLength(300);

                entity.HasIndex(x => x.EmployeeCode)
                    .IsUnique();

                entity.Property(x => x.UserId).HasMaxLength(450); // matches IdentityUser.Id
                entity.HasIndex(x => x.UserId).IsUnique();

                // Existing employees become "Approved" when the migration is applied.
                entity.Property(x => x.ApprovalStatus)
                    .HasMaxLength(20)
                    .IsRequired()
                    .HasDefaultValue("Approved");

                entity.HasOne(x => x.Branch)
                    .WithMany()
                    .HasForeignKey(x => x.BranchId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<Supplier>(entity =>
            {
                // Existing suppliers become "Approved" when the migration is applied.
                entity.Property(x => x.ApprovalStatus)
                    .HasMaxLength(20)
                    .IsRequired()
                    .HasDefaultValue("Approved");
            });

            builder.Entity<Sales>(entity =>
            {
                entity.HasKey(x => x.SalesId);

                entity.Property(x => x.SaleNumber)
                    .HasMaxLength(50)
                    .IsRequired();

                entity.Property(x => x.Subtotal)
                    .HasPrecision(18, 2);

                entity.Property(x => x.Discount)
                    .HasPrecision(18, 2);

                entity.Property(x => x.TotalAmount)
                    .HasPrecision(18, 2);

                entity.Property(x => x.VatableSales)
                    .HasPrecision(18, 2);

                entity.Property(x => x.VatExempt)
                    .HasPrecision(18, 2);

                entity.Property(x => x.VatAmount)
                    .HasPrecision(18, 2);

                entity.Property(x => x.CashReceived)
                    .HasPrecision(18, 2);

                entity.Property(x => x.ChangeAmount)
                    .HasPrecision(18, 2);

                entity.Property(x => x.PaymentMethod)
                    .HasMaxLength(30)
                    .IsRequired();

                entity.Property(x => x.Status)
                    .HasMaxLength(30)
                    .IsRequired();

                entity.HasOne(x => x.Branch)
                    .WithMany()
                    .HasForeignKey(x => x.BranchId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(x => x.Employee)
                    .WithMany()
                    .HasForeignKey(x => x.EmployeeId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasIndex(x => x.BranchId);
                entity.HasIndex(x => x.SaleDate);
                entity.HasIndex(x => x.SaleNumber);
            });

            builder.Entity<PayrollEntity>(entity =>
            {
                entity.HasKey(x => x.PayrollId);

                entity.HasOne(x => x.Employee)
                    .WithMany(x => x.Payrolls)
                    .HasForeignKey(x => x.EmployeeId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.Property(x => x.BasicSalary).HasPrecision(18, 2);
                entity.Property(x => x.Allowances).HasPrecision(18, 2);
                entity.Property(x => x.Deductions).HasPrecision(18, 2);
                entity.Property(x => x.NetPay).HasPrecision(18, 2);
                entity.Property(x => x.Status).HasMaxLength(50).IsRequired();
            });

            builder.Entity<ExpenseCategory>(entity =>
            {
                entity.HasKey(x => x.ExpenseCategoryId);

                entity.Property(x => x.CategoryName)
                    .HasMaxLength(100)
                    .IsRequired();

                entity.HasIndex(x => x.CategoryName)
                    .IsUnique();
            });

            builder.Entity<Inventory>(entity =>
            {
                entity.HasKey(x => x.InventoryId);

                entity.HasOne(x => x.CreatedByEmployee)
                    .WithMany()
                    .HasForeignKey(x => x.CreatedByEmployeeId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(x => x.UpdatedByEmployee)
                    .WithMany()
                    .HasForeignKey(x => x.UpdatedByEmployeeId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<Product>(entity =>
            {
                entity.HasKey(x => x.ProductId);

                entity.HasOne(x => x.CreatedByEmployee)
                    .WithMany()
                    .HasForeignKey(x => x.CreatedByEmployeeId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(x => x.UpdatedByEmployee)
                    .WithMany()
                    .HasForeignKey(x => x.UpdatedByEmployeeId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<StockMovement>(entity =>
            {
                entity.HasKey(x => x.StockMovementId);

                entity.Property(x => x.MovementType)
                    .HasMaxLength(50)
                    .IsRequired();

                entity.Property(x => x.Quantity)
                    .HasPrecision(18, 2);

                entity.Property(x => x.QuantityBefore)
                    .HasPrecision(18, 2);

                entity.Property(x => x.QuantityAfter)
                    .HasPrecision(18, 2);

                entity.Property(x => x.Remarks)
                    .HasMaxLength(300);

                entity.HasOne(x => x.Product)
                    .WithMany()
                    .HasForeignKey(x => x.ProductId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(x => x.Purchase)
                    .WithMany()
                    .HasForeignKey(x => x.PurchaseId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(x => x.Employee)
                    .WithMany()
                    .HasForeignKey(x => x.EmployeeId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasIndex(x => x.ProductId);
                entity.HasIndex(x => x.PurchaseId);
                entity.HasIndex(x => x.EmployeeId);
                entity.HasIndex(x => x.MovementDate);
            });

            builder.Entity<Expense>(entity =>
            {
                entity.HasKey(x => x.ExpenseId);

                entity.Property(x => x.Description)
                    .HasMaxLength(300)
                    .IsRequired();

                entity.Property(x => x.Amount)
                    .HasPrecision(18, 2);

                entity.Property(x => x.PaymentMethod)
                    .HasMaxLength(50)
                    .IsRequired();

                entity.Property(x => x.Status)
                    .HasMaxLength(30)
                    .IsRequired();

                entity.HasOne(x => x.ExpenseCategory)
                    .WithMany(x => x.Expenses)
                    .HasForeignKey(x => x.ExpenseCategoryId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<EmployeeBenefits>(e =>
            {
                e.HasKey(x => x.BenefitsId);
                e.Property(x => x.MonthlySalary).HasPrecision(18, 2);
                e.HasIndex(x => x.EmployeeId).IsUnique();          // one row per employee
                e.HasOne<EmployeeEntity>()
                 .WithMany()
                 .HasForeignKey(x => x.EmployeeId)
                 .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<ContributionRate>(e =>
            {
                e.HasKey(x => x.ContributionRateId);
                e.Property(x => x.Type).HasMaxLength(20);
                e.Property(x => x.MinSalary).HasPrecision(18, 2);
                e.Property(x => x.MaxSalary).HasPrecision(18, 2);
                e.Property(x => x.EmployeeShare).HasPrecision(18, 4);
                e.Property(x => x.EmployerShare).HasPrecision(18, 4);
                e.Property(x => x.MaxContribution).HasPrecision(18, 2);
                e.HasIndex(x => new { x.Type, x.EffectiveFrom });
            });

            builder.Entity<PayrollEntity>(e =>
            {
                e.Property(x => x.SssEmployee).HasPrecision(18, 2);
                e.Property(x => x.SssEmployer).HasPrecision(18, 2);
                e.Property(x => x.PhilHealthEmployee).HasPrecision(18, 2);
                e.Property(x => x.PhilHealthEmployer).HasPrecision(18, 2);
                e.Property(x => x.PagibigEmployee).HasPrecision(18, 2);
                e.Property(x => x.PagibigEmployer).HasPrecision(18, 2);
            });

            builder.Entity<Expense>(e =>
            {
                e.HasOne(x => x.Branch).WithMany().HasForeignKey(x => x.BranchId).OnDelete(DeleteBehavior.Restrict);
                e.HasIndex(x => x.BranchId);
            });

            builder.Entity<Purchase>(e =>
            {
                e.HasOne(x => x.Branch).WithMany().HasForeignKey(x => x.BranchId).OnDelete(DeleteBehavior.Restrict);
                e.HasIndex(x => x.BranchId);
            });

            builder.Entity<PayrollEntity>(e =>
            {
                e.HasOne(x => x.Branch).WithMany().HasForeignKey(x => x.BranchId).OnDelete(DeleteBehavior.Restrict);
                e.HasIndex(x => x.BranchId);
            });
        }
    }
}