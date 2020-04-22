using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;
using HttpModule.Task7.SiteDownloader.Services.Abstractions;
using HttpModule.Task7.SiteDownloader.Services.EventArgs;

namespace HttpModule.Task7.SiteDownloader.Services
{
    public class Saver : ISaver
    {
        private readonly string _rootFolder;

        public Saver(string rootFolder)
        {
            _rootFolder = rootFolder;
        }

        public event EventHandler<FileDownloadEvenArgs> FileSaved;

        public async Task SaveHtmlAsync(Uri uri, HtmlDocument document)
        {
            var directoryPath = GetLocation(uri);
            Directory.CreateDirectory(directoryPath);
            var htmlFileName = GeDocumentFileName(document);
            htmlFileName = GetValidFileName(htmlFileName);

            var htmlFullPath = Path.Combine(directoryPath, htmlFileName);
            using var memoryStream = new MemoryStream();
            document.Save(memoryStream);
            memoryStream.Seek(0, SeekOrigin.Begin);
            await SaveToFileAsync(htmlFullPath, memoryStream);
            OnFileSaved(new FileDownloadEvenArgs(uri, htmlFullPath));
        }

        public async Task SaveFileAsync(Uri uri, Stream fileStream)
        {
            var fileFullPath = GetLocation(uri);

            var directoryPath = Path.GetDirectoryName(fileFullPath);
            Directory.CreateDirectory(directoryPath);

            await SaveToFileAsync(fileFullPath, fileStream);
            OnFileSaved(new FileDownloadEvenArgs(uri, fileFullPath));
        }

        private string GetLocation(Uri uri)
        {
            return $"{Path.Combine(_rootFolder, uri.Host)}{uri.LocalPath.Replace("/", @"\")}";
        }

        private async Task SaveToFileAsync(string fileFullPath, Stream fileStream)
        {
            using var fs = new FileStream(fileFullPath, FileMode.Create);
            await fileStream.CopyToAsync(fs);
        }

        private string GeDocumentFileName(HtmlDocument document)
        {
            return document.DocumentNode.Descendants("title").FirstOrDefault()?.InnerText + ".html";
        }

        private string GetValidFileName(string filename)
        {
            var invalidFileNameChars = Path.GetInvalidFileNameChars();
            var newFileNameChars = filename.Where(c => !invalidFileNameChars.Contains(c))
                .ToArray();

            return new string(newFileNameChars);
        }

        protected virtual void OnFileSaved(FileDownloadEvenArgs e)
        {
            FileSaved?.Invoke(this, e);
        }
    }
}