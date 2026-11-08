using Larp.infrastructure.Services;
using Larp.domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Larp.api.Controllers
{
    [ApiController]
    [Route("tenant/{companyId}/expense-categories")]
    public class ExpenseCategoryController : ControllerBase
    {
        private readonly ExpenseService _expenseService;

        public ExpenseCategoryController(ExpenseService expenseService)
        {
            _expenseService = expenseService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ExpenseCategory>>> GetAll(
            int companyId)
        {
            var categories =
                await _expenseService.GetCategoriesAsync(companyId);

            return Ok(categories);
        }

        [HttpGet("{categoryId:int}")]
        public async Task<ActionResult<ExpenseCategory>> GetById(
            int companyId,
            int categoryId)
        {
            var category =
                await _expenseService.GetCategoryByIdAsync(
                    companyId,
                    categoryId);

            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        [HttpPost]
        public async Task<ActionResult<ExpenseCategory>> Create(
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
                return Conflict(new
                {
                    message = ex.Message
                });
            }
        }
    }
}