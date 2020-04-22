using System;

namespace HttpModule.Task7.SiteDownloader.Services.EventArgs
{
    public class FileDownloadEvenArgs
    {
        public Uri Uri { get; set; }
        public string NewFilePath { get; set; }

        public FileDownloadEvenArgs(Uri uri, string newFilePath)
        {
            Uri = uri;
            NewFilePath = newFilePath;
        }
    }
}