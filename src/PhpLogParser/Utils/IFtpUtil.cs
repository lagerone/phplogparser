namespace PhpLogParser.Utils
{
    internal interface IFtpUtil
    {
        void RenameFile(string targetDirectory, string currentFilenamn, string newFilename);
        bool FileExists(string targetDirectory, string targetFile);
        void DownloadFile(string targetDirectory, string targetFile, string destinationFile);
    }
}