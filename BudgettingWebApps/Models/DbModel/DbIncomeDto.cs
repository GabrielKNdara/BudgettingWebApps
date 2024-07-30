namespace BudgettingWebApps.Models.DbModel
{
    public class DbIncomeDto
    {
        public int id { get; set; }
        public string incomename { get; set; }
        public decimal amount { get; set; }
        public DateTime budgetmonth { get; set; }
        public int userid { get; set; }
    }
}
