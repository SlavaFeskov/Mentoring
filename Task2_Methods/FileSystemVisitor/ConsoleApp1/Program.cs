using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Autofac;
using Autofac.Core;
using FileSystemVisitorLib_V2.Container;
using FileSystemVisitorLib_V2.EventArgs;
using FileSystemVisitorLib_V2.Models;
using FileSystemVisitorLib_V2.Services;
using FileSystemVisitorLib_V2.Services.Abstractions;
using Console = System.Console;
using IContainer = Autofac.IContainer;

namespace ConsoleApp1
{
    class Program
    {
        private const string AskForUserStopInput =
            "Enter 'stop' to interrupt searching process or press 'Enter' to continue: ";

        private const string AskForUserSkipInput =
            "Enter 'skip' to skip filtered file system object or press 'Enter' to add filtered object into result list: ";

        private static readonly IContainer Container = ContainerFactory.GetContainer();

        static void Main(string[] args)
        {
            var visitor = Container.Resolve<IFileSystemVisitor<FileSystemInfo>>(new NamedParameter("filter",
                new Func<BaseObject<FileSystemInfo>, bool>(f => true)));
            visitor.Start += OnStart;
            visitor.Finish += OnEnd;
            visitor.FileFound += OnFileFound;
            visitor.DirectoryFound += OnDirFound;
            visitor.FileFilteredFound += OnFileFilteredFound;
            visitor.DirectoryFilteredFound += OnDirFilteredFound;
            var result = new List<FileSystemInfo>();
            foreach (var fileSystemObject in visitor.GetFileSystemObjects(
                @"D:\Mentoring\Task2_Methods\NUnitTestProject1\bin\Debug\netcoreapp3.1\de3jq82"))
            {
                result.Add(fileSystemObject.Info);
            }

            Console.WriteLine(string.Join("\r\n", result.Select(f => f.Name)));
            Console.ReadKey();
        }

        static void OnFileFilteredFound(object o, FileSystemObjectFilteredEventArgs<FileSystemInfo> e)
        {
            Console.WriteLine($"[Filtered] File {e.FileSystemObject.Name} was found.");
            Console.Write(AskForUserSkipInput);
            var line = Console.ReadLine()?.ToLower();
            if (line == "skip")
            {
                e.Skip = true;
            }
        }

        static void OnDirFilteredFound(object o, FileSystemObjectFilteredEventArgs<FileSystemInfo> e)
        {
            Console.WriteLine($"[Filtered] Directory {e.FileSystemObject.Name} was found.");
            Console.Write(AskForUserSkipInput);
            var line = Console.ReadLine()?.ToLower();
            if (line == "skip")
            {
                e.Skip = true;
            }
        }

        static void OnFileFound(object o, FileSystemObjectFoundEventArgs<FileSystemInfo> e)
        {
            Console.WriteLine($"File {e.FileSystemObject.Name} was found.");
            Console.Write(AskForUserStopInput);
            var line = Console.ReadLine()?.ToLower();
            if (line == "stop")
            {
                e.Stop = true;
            }
        }

        static void OnDirFound(object o, FileSystemObjectFoundEventArgs<FileSystemInfo> e)
        {
            Console.WriteLine($"Directory {e.FileSystemObject.Name} was found.");
        }

        static void OnStart(object o, FileSystemVisitorEventArgs e)
        {
            Console.WriteLine("Started!");
        }

        static void OnEnd(object o, FileSystemVisitorEventArgs e)
        {
            Console.WriteLine("Finished!");
        }
    }
}