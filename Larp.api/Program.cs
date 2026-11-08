using Larp.domain.DTO;
using Larp.domain.Entities;
using Larp.infrastructure.Data;
using Larp.infrastructure.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Net.NetworkInformation;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

public partial class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers(options =>
        {
            options.Filters.Add<Larp.api.Security.TenantAccessFilter>();
        });
        builder.Services.AddOpenApi();

        builder.Services.AddDbContext<MasterErpDbContext>(options =>
            options.UseSqlServer(
                builder.Configuration.GetConnectionString("MasterErp")));

        builder.Services.AddScoped<ITenantDBResolver, TenantDBResolver>();
        builder.Services.AddScoped<ITenantDBContextFactory, TenantDBContextFactory>();
        builder.Services.AddScoped<IReportService, ReportService>();
        builder.Services.AddScoped<PayrollService>();
        builder.Services.AddScoped<EmployeeService>();
        builder.Services.AddScoped<ProductService>();
        builder.Services.AddScoped<InventoryService>();
        builder.Services.AddScoped<CustomerService>();
        builder.Services.AddScoped<SalesService>();
        builder.Services.AddScoped<StockMovementService>();
        builder.Services.AddScoped<PurchaseService>();
        builder.Services.AddScoped<SupplierService>();
        builder.Services.AddScoped<ExpenseService>();
        builder.Services.AddScoped<BranchService>();
        builder.Services.AddSingleton<IBackupStorage, LocalBackupStorage>();
        builder.Services.AddScoped<BackupService>();
        builder.Services.AddScoped<ApprovalService>();
        builder.Services.AddSingleton<PayrollCalculator>();
        builder.Services.AddScoped<BenefitsService>();
        builder.Services.AddMemoryCache();
        builder.Services.AddScoped<PlanService>();
        builder.Services.AddScoped<TermsAndConditionsService>();
        builder.Services.AddScoped<BranchReportService>();
        builder.Services.AddScoped<TenantSeederService>();

        builder.Services.AddIdentityCore<ApplicationUser>().AddRoles<IdentityRole>().AddEntityFrameworkStores<MasterErpDbContext>();

        builder.Services.AddIdentityCore<ApplicationUser>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<MasterErpDbContext>();
        builder.Services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)),
                    ClockSkew = TimeSpan.FromMinutes(1)
                };
            });

        builder.Services.AddAuthorization();

        var app = builder.Build();

        IdentitySeeder.SeedAsync(app.Services).GetAwaiter().GetResult();

        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        // app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();


    }
}