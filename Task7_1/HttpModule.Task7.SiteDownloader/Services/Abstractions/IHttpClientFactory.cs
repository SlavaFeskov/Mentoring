using System.Net.Http;

namespace HttpModule.Task7.SiteDownloader.Services.Abstractions
{
    public interface IHttpClientFactory
    {
        HttpClient Create();
    }
}