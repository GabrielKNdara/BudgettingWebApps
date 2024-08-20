namespace BudgettingWebApps.Models
{
    public class ExpenseDto
    {
        public int Id { get; set; }
        public string ExpenseName { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public bool IsPaid { get; set; } = false;
        public DateTime BudgetMonth { get; set; }
        public int UserId { get; set; }
    }
}
