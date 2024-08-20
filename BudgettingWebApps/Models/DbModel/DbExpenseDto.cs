namespace BudgettingWebApps.Models.DbModel
{
    public class DbExpenseDto
    {
        public int id { get; set; }
        public string expensename { get; set; } = string.Empty;
        public decimal amount { get; set; }
        public bool ispaid { get; set; } = false;
        public DateTime budgetmonth { get; set; }
       public int userid { get; set; }
    }
}
