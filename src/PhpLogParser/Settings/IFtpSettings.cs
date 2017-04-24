namespace PhpLogParser.Settings
{
    internal interface IFtpSettings
    {
        string FtpHost { get; }
        string FtpUsername { get; }
        string FtpPassword { get; }
    }
}