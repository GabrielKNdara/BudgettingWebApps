namespace BudgettingWebApps.Models
{
    public class IncomeDto
    {
        public int Id{ get; set; }
        public string IncomeName { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public int UserId { get; set; }
    }
}
