using System.Net.Http;
using HttpModule.Task7.SiteDownloader.Services.Abstractions;

namespace HttpModule.Task7.SiteDownloader.Services
{
    public class HttpClientFactory : IHttpClientFactory
    {
        public HttpClient Create()
        {
            return new HttpClient();
        }
    }
}