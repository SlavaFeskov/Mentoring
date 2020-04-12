using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using Autofac;
using BCL.Configuration;
using BCL.Configuration.Models;
using BCL.Resources;
using BCL.Services.Abstractions;

namespace BCL
{
    class Program
    {
        private static IWatchingService _watchingService;
        private static readonly IContainer Container = ContainerFactory.ContainerFactory.Create();

        static void Main(string[] args)
        {
            var configuration = ConfigFactory.GetGeneralSection();
            var culture = CultureInfo.GetCultureInfo(configuration.Culture.CultureName);
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;

            Console.CancelKeyPress += Console_CancelKeyPress;
            if (configuration.DirectoriesToWatch.Count == 0)
            {
                Console.WriteLine(StringResources.NoDirecrotiesSpecified);
                Console.ReadKey();
            }

            _watchingService = Container.Resolve<IWatchingService>();
            _watchingService.FileAdded += Watcher_FileAdded;
            _watchingService.FileMove += WatcherFileMove;
            _watchingService.RuleFoundNotFound += Watcher_RuleFoundNotFound;
            var directoriesToWatch = new List<string>();
            foreach (DirectoryPathElement directory in configuration.DirectoriesToWatch)
            {
                directoriesToWatch.Add(directory.DirectoryPath);
            }

            var tasks = _watchingService.Watch(directoriesToWatch);
            Task.WaitAll(tasks.ToArray());
        }

        private static void Watcher_RuleFoundNotFound(object sender, FileWatcherEventArgs e)
        {
            Console.WriteLine(e.Message);
        }

        private static void WatcherFileMove(object sender, FileWatcherEventArgs e)
        {
            Console.WriteLine(e.Message);
        }

        private static void Watcher_FileAdded(object sender, FileWatcherEventArgs e)
        {
            Console.WriteLine(e.Message);
        }

        private static void Console_CancelKeyPress(object sender, ConsoleCancelEventArgs e)
        {
            _watchingService.Interrupt();
            Environment.Exit(0);
        }
    }
}