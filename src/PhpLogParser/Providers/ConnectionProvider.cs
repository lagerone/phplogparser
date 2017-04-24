using System.Data;
using MySql.Data.MySqlClient;
using PhpLogParser.Settings;

namespace PhpLogParser.Providers
{
    public class ConnectionProvider : IConnectionProvider
    {
        private readonly IDatabaseSettings _databaseSettings;

        public ConnectionProvider(IDatabaseSettings databaseSettings)
        {
            _databaseSettings = databaseSettings;
        }

        public IDbConnection CreateConnection()
        {
            return CreateAndOpenConnection(_databaseSettings.ConnectionString);
        }
        
        private static IDbConnection CreateAndOpenConnection(string connectionString)
        {
            var connection = new MySqlConnection { ConnectionString = connectionString };
            connection.Open();
            return connection;
        }
    }
}