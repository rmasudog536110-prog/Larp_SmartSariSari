using Larp.domain.Entities;
using Larp.infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace Larp.api.Controllers;

[ApiController]
public class CompanyDatabasesController : ControllerBase
{
    [HttpPost("/company-databases")]
    public async Task<IActionResult> Create(
        CompanyDatabase companyDatabase,
        MasterErpDbContext db)
    {
        db.CompanyDatabases.Add(companyDatabase);
        await db.SaveChangesAsync();

        return Created(
            $"/company-databases/{companyDatabase.CompanyDatabaseId}",
            companyDatabase);
    }
}