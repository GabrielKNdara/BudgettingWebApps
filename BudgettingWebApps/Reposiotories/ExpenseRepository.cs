using BudgettingWebApps.Configuration;
using BudgettingWebApps.Models;
using BudgettingWebApps.Models.Mapper;
using Dapper;

namespace BudgettingWebApps.Reposiotories
{
    public interface IExpenseRepository
    {
        Task<List<ExpenseDto>> GetExpenses(int id);
        Task<int> AddExpense(ExpenseDto expense);
        Task<int> UpdateExpense(ExpenseDto expense);
        Task<int> DeleteExpense(int id);
    }
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly IPsSqlDbConnectionFactory psSqlDbConnectionFactory;
        public ExpenseRepository(IPsSqlDbConnectionFactory connectionFactory)
        {
            psSqlDbConnectionFactory = connectionFactory;
        }
        public async Task<List<ExpenseDto>> GetExpenses(int id)
        {
            var connection = psSqlDbConnectionFactory.GetDbConnection();
            var sql = "select id, expensename, amount, ispaid, budgetmonth from expense where userid = @id";
            var rawResult = await connection.QueryAsync<ExpenseDto>(sql, new { id = id });
            return rawResult.ToList();
        }
        public async Task<int> AddExpense(ExpenseDto expense)
        {
            var connection = psSqlDbConnectionFactory.GetDbConnection();
           var dbExpense = ExpenseMapper.Map(expense);
            var sql = "INSERT INTO public.expense (expensename, amount, ispaid, budgetmonth, userid) VALUES (@expensename, @amount, @ispaid, @budgetmonth, @userid);";
            var expenseId = await connection.ExecuteAsync(sql, new
            {
                expensename = dbExpense.expensename,
                amount = dbExpense.amount,
                ispaid = dbExpense.ispaid,
                budgetmonth = dbExpense.budgetmonth,
                userid = dbExpense.userid
            });
            return expenseId;
        }
        public async Task<int> UpdateExpense(ExpenseDto expense)
        {
            var connection = psSqlDbConnectionFactory.GetDbConnection();
            var dbExpense = ExpenseMapper.Map(expense);
            var sql = "UPDATE public.expense SET expensename=@expensename, amount=@amount, ispaid=@ispaid, budgetmonth=@budgetmonth WHERE id=@id;";
            var expenseId = await connection.ExecuteAsync(sql, new
            {
                expensename = dbExpense.expensename,
                amount = dbExpense.amount,
                ispaid = dbExpense.ispaid,
                budgetmonth = dbExpense.budgetmonth,
                id = dbExpense.id
            });
            return expenseId;
        }
        public async Task<int> DeleteExpense(int id)
        {
            var connection = psSqlDbConnectionFactory.GetDbConnection();
            var sql = "delete from public.expense where id = @id";
            var expenseId = await connection.ExecuteAsync(sql, new { id = id });
            return expenseId;
        }
    }
}
