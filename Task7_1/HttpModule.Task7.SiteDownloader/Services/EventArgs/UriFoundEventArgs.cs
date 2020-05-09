using System;

namespace HttpModule.Task7.SiteDownloader.Services.EventArgs
{
    public class UriFoundEventArgs
    {
        public Uri Uri { get; set; }

        public UriFoundEventArgs(Uri uri)
        {
            Uri = uri;
        }
    }
}