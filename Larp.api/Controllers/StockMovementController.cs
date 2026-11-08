using Larp.infrastructure.Services;
using Larp.domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Larp.api.Controllers
{
    [ApiController]
    [Route("tenant/{companyId}/stock-movements")]
    public class StockMovementController : ControllerBase
    {
        private readonly StockMovementService _stockMovementService;

        public StockMovementController(
            StockMovementService stockMovementService)
        {
            _stockMovementService = stockMovementService;
        }

        [HttpGet("product/{productId:int}")]
        public async Task<ActionResult<List<StockMovement>>> GetByProduct(
            int companyId,
            int productId)
        {
            var movements =
                await _stockMovementService.GetByProductAsync(
                    companyId,
                    productId);

            return Ok(movements);
        }

        [HttpGet("{movementId:int}")]
        public async Task<ActionResult<StockMovement>> GetById(
            int companyId,
            int movementId)
        {
            var movement =
                await _stockMovementService.GetByIdAsync(
                    companyId,
                    movementId);

            if (movement == null)
            {
                return NotFound(new
                {
                    message = "Stock movement not found."
                });
            }

            return Ok(movement);
        }

        [HttpGet("date-range")]
        public async Task<ActionResult<List<StockMovement>>> GetByDateRange(
            int companyId,
            [FromQuery] DateTime from,
            [FromQuery] DateTime to)
        {
            if (from > to)
            {
                return BadRequest(new
                {
                    message =
                        "The start date cannot be later than the end date."
                });
            }

            var movements =
                await _stockMovementService.GetByDateRangeAsync(
                    companyId,
                    from,
                    to);

            return Ok(movements);
        }
    }
}