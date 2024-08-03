using BudgettingWebApps.Configuration;
using BudgettingWebApps.Models;
using BudgettingWebApps.Models.DbModel;
using Dapper;

namespace BudgettingWebApps.Reposiotories
{
    public interface IUserRepository
    {
        Task<DbUserDto> GetUser(string userName);
    }
    public class UserRepository : IUserRepository
    {
        private readonly IPsSqlDbConnectionFactory _connectionFactory;
        public UserRepository(IPsSqlDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

       public async Task<DbUserDto> GetUser(string userName)
        {
            var connection = _connectionFactory.GetDbConnection();
           // var sql = @"select id as userid, concat(firstname,' ',surname) as fullname, passwordhash from public.users where username = @user";
            var sql = @"select id, passwordhash from public.users where username = @user";
            var result = await connection.QueryFirstOrDefaultAsync<DbUserDto>(sql, new { user = userName });
            return result;
        }
    }
}
