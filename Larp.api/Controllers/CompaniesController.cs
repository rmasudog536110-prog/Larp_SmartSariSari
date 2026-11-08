using Larp.domain.Entities;
using Larp.infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace Larp.api.Controllers;

[ApiController]
public class CompaniesController : ControllerBase
{
    [HttpPost("/companies")]
    public async Task<IActionResult> Create(
        Company company,
        MasterErpDbContext db)
    {
        db.Companies.Add(company);
        await db.SaveChangesAsync();

        return Created($"/companies/{company.CompanyId}", company);
    }
}