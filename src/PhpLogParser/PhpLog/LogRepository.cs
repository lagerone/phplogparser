using System.Collections.Generic;
using System.Linq;
using Dapper;
using PhpLogParser.Providers;

namespace PhpLogParser.PhpLog
{
    public class LogRepository : ILogRepository
    {
        private readonly IConnectionProvider _connectionProvider;

        public LogRepository(IConnectionProvider connectionProvider)
        {
            _connectionProvider = connectionProvider;
        }

        public void AddLogCollection(IEnumerable<LogEntry> logEntries)
        {
            const string sql = @"
INSERT INTO `PhpLog` (LogDate, LogType, LogMessage, SourceFile)
VALUES (@LogDate, @LogType, @LogMessage, @SourceFile)
";
            using (var connection = _connectionProvider.CreateConnection())
            {
                connection.Execute(sql, logEntries.ToList());
            }    
        }
    }
}