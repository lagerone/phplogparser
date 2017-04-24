using System;

namespace PhpLogParser.PhpLog
{
    public class LogEntry
    {
        public int Id { get; set; }
        public DateTime LogDate { get; set; }
        public string LogType { get; set; }
        public string LogMessage { get; set; }
        public string SourceFile { get; set; }
    }
}