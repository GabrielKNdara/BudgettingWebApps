using BudgettingWebApps.Configuration;
using BudgettingWebApps.Models;
using BudgettingWebApps.Models.Mapper;
using Dapper;

namespace BudgettingWebApps.Reposiotories
{
    public interface IincomeRepository
    {
        Task<List<IncomeDto>> GetIncome(int id);
        Task<int> DeleteIncome(int id);
        Task<int> CreateNewIncome(IncomeDto request);
        Task<int> UpdateIncome(IncomeDto income);
    }
    public class IncomeRepository : IincomeRepository
    {
        private readonly IPsSqlDbConnectionFactory _pgSqlConnectionFactory;
        public IncomeRepository(IPsSqlDbConnectionFactory pgSqlConnectionFactory)
        {
            _pgSqlConnectionFactory = pgSqlConnectionFactory;
        }
        public async Task<List<IncomeDto>> GetIncome(int id)
        {
            var connection = _pgSqlConnectionFactory.GetDbConnection();
            var sql = @"select	id Id, incomename IncomeName,amount Amount, budgetmonth TransactionDate from public.income  where userid= @id";
            var rawResult = await connection.QueryAsync<IncomeDto>(sql, new { id = @id });
            return rawResult.ToList();
        }
        public async Task<int> DeleteIncome(int id)
        {
            var connection = _pgSqlConnectionFactory.GetDbConnection();
            var sql = "delete from public.income where id = @id";
            var result = await connection.ExecuteAsync(sql, new { id = @id });
            return result;
        }
        public async Task<int> CreateNewIncome(IncomeDto income)
        {
            var connection = _pgSqlConnectionFactory.GetDbConnection();
            var dbincomedto = IncomeMapper.Map(income);
            var sql = "INSERT INTO public.income(incomename, amount, budgetmonth, userid) VALUES(@incomename, @amount, @budgetmonth, @userid);";
            var incomeId = await connection.ExecuteAsync(sql, new
            {
                userid = dbincomedto.userid,
                incomename = dbincomedto.incomename,
                amount = dbincomedto.amount,
                budgetmonth = dbincomedto.budgetmonth,
            });
            return incomeId;
        }
        public async Task<int> UpdateIncome(IncomeDto income)
        {
            var connection = _pgSqlConnectionFactory.GetDbConnection();
            var dbincome = IncomeMapper.Map(income);
            var sql = "UPDATE public.income SET incomename=@incomename, amount=@amount, budgetmonth=@budgetmonth WHERE id=@id ;";
            var incomeId = await connection.ExecuteAsync(sql, new
            {
                id = dbincome.id,
                incomename = dbincome.incomename,
                amount = dbincome.amount,
                budgetmonth = dbincome.budgetmonth,
            });
            return incomeId;
        }
    }
}
