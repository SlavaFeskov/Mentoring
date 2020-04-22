using System;
using System.IO;
using System.Threading.Tasks;
using HtmlAgilityPack;
using HttpModule.Task7.SiteDownloader.Services.EventArgs;

namespace HttpModule.Task7.SiteDownloader.Services.Abstractions
{
    public interface ISaver
    {
        event EventHandler<FileDownloadEvenArgs> FileSaved;

        Task SaveHtmlAsync(Uri url, HtmlDocument document);

        Task SaveFileAsync(Uri url, Stream dataStream);
    }
}