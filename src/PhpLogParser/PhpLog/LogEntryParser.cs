using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace PhpLogParser.PhpLog
{
    public class LogEntryParser : ILogEntryParser
    {
        private const string DatePattern = @"\d\d-\w\w\w-\d\d\d\d\s\d\d:\d\d:\d\d";
        
        public LogEntry Parse(string input, string sourceFilename = "")
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                throw new ArgumentNullException(nameof(input));
            }

            var dateStringMatch = Regex.Match(input, DatePattern);
            if (string.IsNullOrWhiteSpace(dateStringMatch?.Value))
            {
                throw new ArgumentException(nameof(input));
            }

            var rawInputBody = Regex.Split(input, @"\[" + DatePattern)
                .FirstOrDefault(s => !string.IsNullOrWhiteSpace(s));
            if (string.IsNullOrWhiteSpace(rawInputBody))
            {
                throw new ArgumentException(nameof(input));
            }

            var logDate = GetLogDate(dateStringMatch.Value);
            var logTypeAndMessageParts = SplitToTypeAndMessage(rawInputBody).ToList();
            var logType = GetLogType(logTypeAndMessageParts.First());
            var logMessage = GetLogMessage(logTypeAndMessageParts.Skip(1));

            return new LogEntry
            {
                LogDate = logDate,
                LogType = logType,
                LogMessage = logMessage,
                SourceFile = sourceFilename
            };
        }

        private static IEnumerable<string> SplitToTypeAndMessage(string input)
        {
            var illegalParts = new[]
            {
                "Europe/Berlin]",
                "Europe/Stockholm]",
                "UTC]"
            };
            var sanitizedInput = input;
            foreach (var illegalPart in illegalParts)
            {
                sanitizedInput = sanitizedInput.Replace(illegalPart, string.Empty);
            }
            return sanitizedInput.Trim().Split(':');
        }

        private static string GetLogMessage(IEnumerable<string> input)
        {
            return string.Join(":", input).Trim();
        }

        private static string GetLogType(string input)
        {
            return !string.IsNullOrWhiteSpace(input) ? input.Trim() : string.Empty;
        }

        private static DateTime GetLogDate(string input)
        {
            var dateString = input.Trim();
            return DateTime.ParseExact(dateString, "dd-MMM-yyyy HH:mm:ss", CultureInfo.InvariantCulture);
        }
    }
}