using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace PhpLogParser.PhpLog
{
    public class LogEntrySplitter : ILogEntrySplitter
    {
        public IEnumerable<string> Split(string logdata)
        {
            var splitResult = Regex.Split(logdata, @"(\[\d\d-\w.*-\d\d\d\d \d\d:\d\d:\d\d \w.+]\s\w.*)")
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .ToList();

            var result = new List<string>();

            foreach (var current in splitResult)
            {
                if (current.StartsWith("["))
                {
                    result.Add(current);
                    continue;
                }
                var previousResult = result.Last();
                result = result.Where(r => !r.Equals(previousResult)).ToList();
                result.Add(string.Join(Environment.NewLine, new [] {previousResult, current}));
            }
            
            return result;
        }
    }
}