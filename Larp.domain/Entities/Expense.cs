namespace Larp.domain.Entities
{
    public class Expense
    {
        public int ExpenseId { get; set; }

        public int ExpenseCategoryId { get; set; }

        public string Description { get; set; } = string.Empty;

        public decimal Amount { get; set; }

        public DateTime ExpenseDate { get; set; } = DateTime.UtcNow;

        public string PaymentMethod { get; set; } = "Cash";

        public string Status { get; set; } = "Paid";

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Branch this cost belongs to. Null = Unassigned (older records).
        public int? BranchId { get; set; }

        public Branch? Branch { get; set; }

        public ExpenseCategory? ExpenseCategory { get; set; }
    }
}