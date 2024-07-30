using BudgettingWebApps.Models.DbModel;

namespace BudgettingWebApps.Models.Mapper
{
    public static class IncomeMapper
    {
        public static DbIncomeDto Map(IncomeDto income)
        {
            return new DbIncomeDto()
            {
                id = income.Id,
                incomename = income.IncomeName,
                budgetmonth = income.TransactionDate,
                userid = income.UserId,
            };
        }
    }
}
