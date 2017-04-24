using System.Collections.Generic;

namespace PhpLogParser.PhpLog
{
    public interface ILogRepository
    {
        void AddLogCollection(IEnumerable<LogEntry> logEntries);
    }
}
