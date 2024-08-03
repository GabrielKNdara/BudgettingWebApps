using BudgettingWebApps.Configuration;
using BudgettingWebApps.Models;
using Dapper;
using System.Runtime.CompilerServices;

namespace BudgettingWebApps.Reposiotories
{
    public interface IincomeStatuRepository
    {
        Task<List<IncomeStatusDto>> GetIncomeStatus();
    }
    public class IncomeStatuRepository : IincomeStatuRepository
    {
        private readonly IPsSqlDbConnectionFactory _psSqlDbConnectionFactory;

        public IncomeStatuRepository(IPsSqlDbConnectionFactory connectionFactory)
        {
            _psSqlDbConnectionFactory = connectionFactory;
        }
        public async Task<List<IncomeStatusDto>> GetIncomeStatus()
        {
           var connection = _psSqlDbConnectionFactory.GetDbConnection();
            var sql = @"SELECT id Id, incomeid IncomeId, ispaid IsPaid, isfullpaid IsFullPaid, budgetdate TransactionDate, comment FROM public.incomestatus;";
            var rawResult = await connection.QueryAsync<IncomeStatusDto>(sql);
            return rawResult.ToList();
        }
    }
}
