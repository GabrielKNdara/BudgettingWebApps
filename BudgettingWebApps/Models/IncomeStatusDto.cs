namespace BudgettingWebApps.Models
{
    public class IncomeStatusDto
    {
        public int Id { get; set; }
        public int IncomeId { get; set; }
        public bool IsPaid { get; set; }
        public bool IsFullPaid { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Comment { get; set; }
    }
}
