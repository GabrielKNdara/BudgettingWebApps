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
        Task<int> UpdateUserPassword(ForgotPasswordDto user);
        Task<List<UserDto>> GetAlluser();
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
            var sql = @"select id,active ,role, passwordhash from public.users where username = @user";
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
        public async Task<int> UpdateUserPassword(ForgotPasswordDto forgotPassword)
        {
            using var connection =_connectionFactory.GetDbConnection();
            var dbuser = ForgotPasswordMapper.Map(forgotPassword);
            var sql = "UPDATE public.users SET  passwordhash=@passwordhash WHERE username= @username ";
            var userId = await connection.ExecuteAsync(sql, new
            {
                username = dbuser.username,
                passwordhash = dbuser.passwordhash
            });
            return (int)userId;
        }

        public async Task<List<UserDto>> GetAlluser()
        {
            using var connection = _connectionFactory.GetDbConnection();
            var sql = "select * from users";
            var result = await connection.QueryAsync<UserDto>(sql);
            return result.ToList();
        }
    }
}
