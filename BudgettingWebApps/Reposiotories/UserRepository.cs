using BudgettingWebApps.Configuration;
using BudgettingWebApps.Models;
using BudgettingWebApps.Models.DbModel;
using BudgettingWebApps.Models.Mapper;
using Dapper;

namespace BudgettingWebApps.Reposiotories
{
    public interface IUserRepository
    {
        Task<int> CreateNewUser(UserDto user);
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
        public async Task<int> CreateNewUser(UserDto user)
        {
            using var connection = _connectionFactory.GetDbConnection();    
            var dbuser = UserMapper.Map(user);
            var sql = @"INSERT INTO public.users
                        ( username, email, firstname, surname, passwordhash)
                        VALUES( @username, @email, @firstname, @surname, @passwordhash);";

            var userId = await connection.ExecuteAsync(sql, new
            {
                email = dbuser.email,
                firstname = dbuser.firstname,
                surname = dbuser.lastname,
                passwordhash = dbuser.passwordhash,
                username = dbuser.username
            });
            return (int)userId;
        }
    }
}
