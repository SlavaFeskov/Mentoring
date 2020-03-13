using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using BCL.Configuration;
using BCL.Configuration.Models;
using BCL.Resources;

namespace BCL
{
    class Program
    {
        public static Watcher Watcher { get; set; }

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

            Watcher = new Watcher();
            Watcher.FileAdded += Watcher_FileAdded;
            Watcher.FileMove += WatcherFileMove;
            Watcher.RuleFoundNotFound += Watcher_RuleFoundNotFound;
            var directoriesToWatch = new List<string>();
            foreach (DirectoryPathElement directory in configuration.DirectoriesToWatch)
            {
                directoriesToWatch.Add(directory.DirectoryPath);
            }
            var tasks = Watcher.Watch(directoriesToWatch);
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
            Watcher.Interrupt();
            Environment.Exit(0);
        }
    }
}