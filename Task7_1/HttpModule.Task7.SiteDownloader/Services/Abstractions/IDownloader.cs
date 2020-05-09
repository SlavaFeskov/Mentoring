using System;
using System.Threading.Tasks;
using HttpModule.Task7.SiteDownloader.Services.EventArgs;

namespace HttpModule.Task7.SiteDownloader.Services.Abstractions
{
    public interface IDownloader
    {
        event EventHandler<UriFoundEventArgs> UriFound;

        event EventHandler<ErrorEventArgs> ErrorOccured;

        Task DownloadFromAsync(string url);
    }
}