using BudgettingWebApps.Models.DbModel;

namespace BudgettingWebApps.Models.Mapper
{
    public static class ExpenseMapper
    {
        public static DbExpenseDto Map(ExpenseDto expense)
        {
            return new DbExpenseDto()
            {
                id = expense.Id,
                expensename = expense.ExpenseName,
                amount = expense.Amount,
                budgetmonth = expense.BudgetMonth,
                userid = expense.UserId,
            };
        }
    }
}
