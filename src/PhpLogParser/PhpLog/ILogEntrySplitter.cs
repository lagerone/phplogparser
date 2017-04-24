using System.Collections.Generic;

namespace PhpLogParser.PhpLog
{
    public interface ILogEntrySplitter
    {
        IEnumerable<string> Split(string logdata);
    }
}