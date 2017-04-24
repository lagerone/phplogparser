namespace PhpLogParser.PhpLog
{
    public interface ILogEntryParser
    {
        LogEntry Parse(string input, string sourceFilename = "");
    }
}