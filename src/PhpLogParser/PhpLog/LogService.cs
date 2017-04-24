using System;
using System.IO;
using System.Linq;
using PhpLogParser.Settings;
using PhpLogParser.Utils;

namespace PhpLogParser.PhpLog
{
    internal class LogService : ILogService
    {
        private readonly IPhpLogSettings _settings;
        private readonly ILogEntrySplitter _logEntrySplitter;
        private readonly ILogRepository _logRepository;
        private readonly ILogEntryParser _logEntryParser;
        private readonly IFtpUtil _ftpUtil;
        private readonly string _logFilename;
        private readonly string _logFileDir;

        public LogService(IPhpLogSettings settings, 
            ILogEntryParser logEntryParser, 
            ILogEntrySplitter logEntrySplitter,
            ILogRepository logRepository, 
            IFtpUtil ftpUtil)
        {
            _settings = settings;
            _logEntryParser = logEntryParser;
            _logEntrySplitter = logEntrySplitter;
            _logRepository = logRepository;
            _ftpUtil = ftpUtil;
            _logFilename = "httpd-error.log";
            _logFileDir = "";
        }

        public void ImportPhpLog()
        {
            if (!RemoteFileExists())
            {
                return;
            }

            var newFileName = GetNewImportFilename();

            RenameRemoteLogFile(newFileName);

            _ftpUtil.DownloadFile(_logFileDir, newFileName, _settings.LogSourceFile);

            var fileContent = File.ReadAllText(_settings.LogSourceFile);
            var fileEntries = _logEntrySplitter.Split(fileContent);
            var logEntries = fileEntries.Select(fe => _logEntryParser.Parse(fe, newFileName)).ToList();

            _logRepository.AddLogCollection(logEntries);
        }
        
        private bool RemoteFileExists()
        {
            return _ftpUtil.FileExists(_logFileDir, _logFilename);
        }

        private void RenameRemoteLogFile(string newFileName)
        {
            _ftpUtil.RenameFile(_logFileDir, _logFilename, newFileName);
        }

        private string GetNewImportFilename()
        {
            var name = Path.GetFileNameWithoutExtension(_logFilename);
            var extension = Path.GetExtension(_logFilename);
            var formattedDate = DateTime.UtcNow.ToString("yyyMMddHHmmss");
            return $"{name}-{formattedDate}{extension}";
        }
    }
}