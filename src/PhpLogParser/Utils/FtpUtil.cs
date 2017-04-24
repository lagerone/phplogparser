using System;
using System.IO;
using System.Net;
using PhpLogParser.Settings;

namespace PhpLogParser.Utils
{
    internal class FtpUtil : IFtpUtil
    {
        private readonly IFtpSettings _ftpSettings;

        public FtpUtil(IFtpSettings ftpSettings)
        {
            _ftpSettings = ftpSettings;
        }

        public void RenameFile(string targetDirectory, string currentFilenamn, string newFilename)
        {
            var ftpWebRequest= CreateFtpWebRequest(currentFilenamn, targetDirectory);
            ftpWebRequest.Method = WebRequestMethods.Ftp.Rename;
            ftpWebRequest.RenameTo = newFilename;
            ftpWebRequest.UseBinary = true;
            ftpWebRequest.Credentials = new NetworkCredential(_ftpSettings.FtpUsername, _ftpSettings.FtpPassword);
            var response = (FtpWebResponse) ftpWebRequest.GetResponse();
            response.Close();
        }

        public bool FileExists(string targetDirectory, string targetFile)
        {
            var request = CreateFtpWebRequest(targetFile, targetDirectory);
            request.Credentials = GetNetworkCredential();
            request.Method = WebRequestMethods.Ftp.GetDateTimestamp;

            try
            {
                var response = (FtpWebResponse)request.GetResponse();
            }
            catch (WebException ex)
            {
                var response = (FtpWebResponse)ex.Response;
                if (response.StatusCode == FtpStatusCode.ActionNotTakenFileUnavailable)
                {
                    return false;
                }
            }

            return true;
        }

        public void DownloadFile(string targetDirectory, string targetFile, string destinationFile)
        {
            var request = CreateFtpWebRequest(targetFile, targetDirectory);
            request.Credentials = GetNetworkCredential();
            request.Method = WebRequestMethods.Ftp.DownloadFile;

            using (var response = (FtpWebResponse)request.GetResponse())
            using (var responseStream = response.GetResponseStream())
            using (var reader = new StreamReader(responseStream))
            using (var destination = new StreamWriter(destinationFile))
            {
                destination.Write(reader.ReadToEnd());
                destination.Flush();
            }
        }

        private NetworkCredential GetNetworkCredential()
        {
            return new NetworkCredential(_ftpSettings.FtpUsername, _ftpSettings.FtpPassword);
        }

        private FtpWebRequest CreateFtpWebRequest(string fileName, string targetDirectory)
        {
            var ftpTargetUri = $"ftp://{_ftpSettings.FtpHost}/{targetDirectory}/{fileName}";
            return (FtpWebRequest)WebRequest.Create(ftpTargetUri);
        }

        private static void StreamData(Stream source, Stream target)
        {
            var data = new byte[8192];

            for (var n = source.Length; n > 0;)
            {
                var read = source.Read(data, 0, data.Length);
                target.Write(data, 0, read);
                n -= read;
            }
        }
    }
}