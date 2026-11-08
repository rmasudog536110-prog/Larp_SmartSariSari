using Larp.infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Larp.api.Controllers
{
    [ApiController]
    [Route("admin/seeder")]
    [Authorize(Roles = "SuperAdmin")]
    public class TenantSeederController : ControllerBase
    {
        private readonly TenantSeederService _seeder;

        public TenantSeederController(TenantSeederService seeder)
        {
            _seeder = seeder;
        }

        [HttpPost("products/{companyId:int}")]
        public async Task<IActionResult> SeedProducts(int companyId)
        {
            try
            {
                var result =
                    await _seeder.SeedProductsForTenantAsync(companyId);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Success = false,
                    Message = ex.Message
                });
            }
        }

        [HttpPost("purchases/{companyId:int}")]
        public async Task<IActionResult> SeedPurchases(int companyId)
        {
            try
            {
                var result =
                    await _seeder.SeedPurchasesForTenantAsync(companyId);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Success = false,
                    Message = ex.Message
                });
            }
        }

        [HttpPost("sales/{companyId:int}")]
        public async Task<IActionResult> SeedSales(int companyId)
        {
            try
            {
                var result =
                    await _seeder.SeedSalesForTenantAsync(companyId);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Success = false,
                    Message = ex.Message
                });
            }
        }
    }

}