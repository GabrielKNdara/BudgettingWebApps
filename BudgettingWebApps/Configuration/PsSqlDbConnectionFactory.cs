using Dapper;
using Npgsql;
using System.Data;

namespace BudgettingWebApps.Configuration
{ public interface IPsSqlDbConnectionFactory
    {
        IDbConnection GetDbConnection();
    }
    public class PsSqlDbConnectionFactory : IPsSqlDbConnectionFactory
    {
        private readonly string _connectionString;
        public PsSqlDbConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
        }
        public IDbConnection GetDbConnection()
        {
            var connection = new NpgsqlConnection(_connectionString);
            SimpleCRUD.SetDialect(SimpleCRUD.Dialect.PostgreSQL);
            return connection;
        }
    }
}
