using System.Net.Http;
using System.Net.Http.Headers;
using HttpModule.Task7.SiteDownloader.Model.Configuration;
using HttpModule.Task7.SiteDownloader.Services.Abstractions;

namespace HttpModule.Task7.SiteDownloader.Services
{
    public class HttpClientFactory : IHttpClientFactory
    {
        public HttpClient Create()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.UserAgent.Add(ProductInfoHeaderValue.Parse(Configuration.DefaultUserAgent));
            return client;
        }
    }
}