using System.Configuration;

namespace PhpLogParser.Settings
{
    internal class Configuration : IFtpSettings, IDatabaseSettings, IPhpLogSettings
    {
        public string FtpHost => ConfigurationManager.AppSettings.Get("Ftp.Host");
        public string FtpUsername => ConfigurationManager.AppSettings.Get("Ftp.Username");
        public string FtpPassword => ConfigurationManager.AppSettings.Get("Ftp.Password");
        public string LogSourceFile => ConfigurationManager.AppSettings.Get("PhpLog.SourceFile");
        public string ConnectionString => ConfigurationManager.AppSettings.Get("ConnectionString");
    }
}
