using System;
using System.Collections.Generic;
using HttpModule.Task7.SiteDownloader.Model.Constraint;
using HttpModule.Task7.SiteDownloader.Services;
using HttpModule.Task7.SiteDownloader.Services.Abstractions;
using HttpModule.Task7.SiteDownloader.Services.EventArgs;
using McMaster.Extensions.CommandLineUtils;
using Console = System.Console;

namespace HttpModule.Task7_1.SampleApp
{
    public class Program
    {
        private string GetFailedToExecuteAppMessage(string message) =>
            $"Unable to execute program. {message}.\r\nTo see more options enter '-h'.";

        [Option(CommandOptionType.SingleValue, ShortName = "u")]
        public string SiteUrl { get; set; }

        [Option(CommandOptionType.SingleValue, ShortName = "o")]
        public string OutputDirectory { get; set; }

        [Option(CommandOptionType.SingleValue, ShortName = "d")]
        public int MaxDepth { get; set; } = 1;

        [Option(CommandOptionType.SingleValue, ShortName = "c")]
        public CrossDomainConstraintType CrossDomainConstraintType { get; set; } =
            CrossDomainConstraintType.OnlyCurrentDomain;

        [Option(CommandOptionType.SingleValue, ShortName = "l")]
        public bool Logging { get; set; } = true;

        [Option(CommandOptionType.MultipleValue, ShortName = "e")]
        public IEnumerable<string> AllowedExtensions { get; set; } = new List<string>();

        public static int Main(string[] args)
            => CommandLineApplication.Execute<Program>(args);

        private void OnExecute()
        {
            if (!CheckArgs())
            {
                return;
            }

            var saver = new Saver(OutputDirectory);
            var httpClientFactory = new HttpClientFactory();
            var constraints = new List<IConstraint>
            {
                new FileConstraint(AllowedExtensions),
                new UriConstraint(SiteUrl, CrossDomainConstraintType)
            };
            var downloader = new Downloader(MaxDepth, httpClientFactory, saver, constraints);

            if (Logging)
            {
                saver.FileSaved += Saver_FileSaved;
                downloader.UriFound += Downloader_UriFound;
                downloader.ErrorOccured += Downloader_ErrorOccured;
            }

            Console.WriteLine("Started!");
            try
            {
                downloader.DownloadFromAsync(SiteUrl).Wait();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Fatal Error: {e.InnerException?.Message ?? e.Message}");
            }

            Console.WriteLine("Finished!");
        }

        private void Downloader_ErrorOccured(object sender, ErrorEventArgs e)
        {
            Console.WriteLine($"Error occured: {e.Message}");
        }

        private void Downloader_UriFound(object sender, UriFoundEventArgs e)
        {
            Console.WriteLine($"Uri {e.Uri} was found.");
        }

        private void Saver_FileSaved(object sender, FileDownloadEvenArgs e)
        {
            Console.WriteLine($"File from {e.Uri} was saved.");
        }

        private bool CheckArgs()
        {
            if (string.IsNullOrEmpty(SiteUrl))
            {
                Console.WriteLine(GetFailedToExecuteAppMessage("Please specify SiteUrl (-u)"));
                return false;
            }

            if (string.IsNullOrEmpty(OutputDirectory))
            {
                Console.WriteLine(GetFailedToExecuteAppMessage("Please specify OutputDirectory (-o)"));
                return false;
            }

            return true;
        }
    }
}