using Larp.infrastructure.Services;
using Larp.domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Larp.api.Controllers
{
    [ApiController]
    [Route("tenant/{companyId}/expenses")]
    public class ExpenseController : ControllerBase
    {
        private readonly ExpenseService _expenseService;

        public ExpenseController(ExpenseService expenseService)
        {
            _expenseService = expenseService;
        }

        private bool IsAdmin =>
            User.IsInRole("Admin") || User.IsInRole("SuperAdmin");

        // The token may have no employee (for example a SuperAdmin).
        private int? EmployeeId =>
            int.TryParse(User.FindFirst("employeeId")?.Value, out var id)
                ? id
                : null;

        // ---------------- Categories ----------------
        // Needed by the Expenses form. If you already expose these elsewhere,
        // delete these two actions and point the form at your route.

        [HttpGet("categories")]
        public async Task<IActionResult> GetCategories(int companyId)
        {
            var categories =
                await _expenseService.GetCategoriesAsync(companyId);

            return Ok(categories);
        }

        [HttpPost("categories")]
        public async Task<IActionResult> CreateCategory(
            int companyId,
            [FromBody] ExpenseCategory category)
        {
            try
            {
                var created =
                    await _expenseService.CreateCategoryAsync(
                        companyId,
                        category);

                return Ok(created);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // ---------------- Expenses ----------------

        [HttpGet]
        public async Task<ActionResult<List<Expense>>> GetAll(
            int companyId)
        {
            var expenses =
                await _expenseService.GetAllAsync(companyId);

            return Ok(expenses);
        }

        [HttpGet("date-range")]
        public async Task<ActionResult<List<Expense>>> GetByDateRange(
            int companyId,
            [FromQuery] DateTime from,
            [FromQuery] DateTime to)
        {
            if (from > to)
            {
                return BadRequest(new
                {
                    message = "The start date cannot be later than the end date."
                });
            }

            var expenses =
                await _expenseService.GetByDateRangeAsync(
                    companyId,
                    from,
                    to);

            return Ok(expenses);
        }

        [HttpGet("{expenseId:int}")]
        public async Task<ActionResult<Expense>> GetById(
            int companyId,
            int expenseId)
        {
            var expense =
                await _expenseService.GetByIdAsync(
                    companyId,
                    expenseId);

            if (expense == null)
            {
                return NotFound();
            }

            return Ok(expense);
        }

        [HttpPost]
        public async Task<ActionResult<Expense>> Create(
            int companyId,
            [FromBody] Expense expense)
        {
            try
            {
                var created =
                    await _expenseService.CreateAsync(
                        companyId,
                        expense,
                        EmployeeId,
                        IsAdmin);

                return Ok(created);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }

        [HttpPut("{expenseId:int}")]
        public async Task<ActionResult<Expense>> Update(
            int companyId,
            int expenseId,
            [FromBody] Expense expense)
        {
            try
            {
                var updated =
                    await _expenseService.UpdateAsync(
                        companyId,
                        expenseId,
                        expense,
                        IsAdmin);

                if (updated == null)
                {
                    return NotFound();
                }

                return Ok(updated);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }

        [HttpDelete("{expenseId:int}")]
        public async Task<IActionResult> Delete(
            int companyId,
            int expenseId)
        {
            var deleted =
                await _expenseService.DeleteAsync(
                    companyId,
                    expenseId);

            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}