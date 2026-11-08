namespace Larp.domain.Entities
{
    public class ExpenseCategory
    {
        public int ExpenseCategoryId { get; set; }

        public string CategoryName { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<Expense> Expenses { get; set; }
            = new List<Expense>();
    }
}