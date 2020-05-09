using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ConcurrentCollections;
using HtmlAgilityPack;
using HttpModule.Task7.SiteDownloader.Model.Constraint;
using HttpModule.Task7.SiteDownloader.Services.Abstractions;
using HttpModule.Task7.SiteDownloader.Services.EventArgs;

namespace HttpModule.Task7.SiteDownloader.Services
{
    public class Downloader : IDownloader
    {
        public const string HtmlMediaType = "text/html";
        private readonly IEnumerable<IConstraint> _constraints;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ISaver _saver;
        private readonly int _maxDepth;

        public Downloader(int maxDepth, IHttpClientFactory httpClientFactory, ISaver saver,
            IEnumerable<IConstraint> constraints)
        {
            _maxDepth = maxDepth;
            _httpClientFactory = httpClientFactory;
            _saver = saver;
            _constraints = constraints;
        }

        public event EventHandler<UriFoundEventArgs> UriFound;
        public event EventHandler<ErrorEventArgs> ErrorOccured;

        public async Task DownloadFromAsync(string url)
        {
            var uri = new Uri(url);
            var downloadedLinks = new ConcurrentHashSet<Uri>();
            using var client = _httpClientFactory.Create();
            client.BaseAddress = uri;
            await DownloadFromAsync(client, uri, downloadedLinks, 0);
        }

        private async Task DownloadFromAsync(HttpClient client, Uri url, ConcurrentHashSet<Uri> downloadedLinks,
            int depth)
        {
            if (depth > _maxDepth || !downloadedLinks.Add(url))
            {
                return;
            }

            OnUriFound(new UriFoundEventArgs(url));

            HttpResponseMessage response;
            try
            {
                response = await client.SendAsync(new HttpRequestMessage(HttpMethod.Head, url));
            }
            catch (Exception e)
            {
                OnErrorOccured(e.InnerException?.Message ?? e.Message);
                return;
            }

            if (!response.IsSuccessStatusCode)
            {
                OnErrorOccured($"Failed to get Headers from {url}");
                return;
            }

            if (response.Content?.Headers?.ContentType?.MediaType == HtmlMediaType)
            {
                await DownloadHtmlAsync(client, url, downloadedLinks, depth);
            }
            else
            {
                await DownloadFileAsync(client, url);
            }
        }

        private async Task DownloadHtmlAsync(HttpClient client, Uri url, ConcurrentHashSet<Uri> downloadedLinks,
            int depth)
        {
            if (!IsValid(url, ConstraintType.Uri))
            {
                return;
            }

            var response = await client.GetAsync(url);
            var document = new HtmlDocument();
            document.Load(await response.Content.ReadAsStreamAsync());

            await _saver.SaveHtmlAsync(url, document);

            var internalLinks = document.DocumentNode.Descendants()
                .SelectMany(d => d.Attributes.Where(IsAttributeWithLink));
            var internalLinkTasks = internalLinks.Select(internalLink =>
                DownloadFromAsync(client, new Uri(client.BaseAddress, internalLink.Value), downloadedLinks,
                    depth + 1));

            await Task.WhenAll(internalLinkTasks);
        }

        private async Task DownloadFileAsync(HttpClient client, Uri url)
        {
            if (!IsValid(url, ConstraintType.File))
            {
                return;
            }

            var response = await client.GetAsync(url);
            var content = await response.Content.ReadAsStreamAsync();

            await _saver.SaveFileAsync(url, content);
        }

        private bool IsAttributeWithLink(HtmlAttribute attribute)
        {
            return attribute.Name == "src" || attribute.Name == "href";
        }

        private bool IsValid(Uri uri, ConstraintType constraintType) =>
            _constraints.Where(c => (c.Type & constraintType) != 0).All(c => c.IsValid(uri));

        protected virtual void OnUriFound(UriFoundEventArgs e)
        {
            UriFound?.Invoke(this, e);
        }

        protected virtual void OnErrorOccured(string message)
        {
            ErrorOccured?.Invoke(this, new ErrorEventArgs(message));
        }
    }
}