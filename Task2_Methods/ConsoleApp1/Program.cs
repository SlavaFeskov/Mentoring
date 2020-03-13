using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FileSystemVisitorLib_V2;
using FileSystemVisitorLib_V2.EventArgs;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var visitor = new FileSystemVisitor2(f => true);
            visitor.Start += OnStart;
            visitor.Finish += OnEnd;
            visitor.FileFound += OnFileFound;
            visitor.DirectoryFound += OnDirFound;
            visitor.FileFilteredFound += OnFileFilteredFound;
            visitor.DirectoryFilteredFound += OnDirFilteredFound;
            var result = new List<FileSystemInfo>();
            foreach (var fileSystemObject in visitor.GetFileSystemObjects(Directory.GetCurrentDirectory()))
            {
                result.Add(fileSystemObject);
            }

            Console.WriteLine(string.Join("\r\n", result.Select(f => f.Name)));
            Console.ReadKey();
        }

        static void OnFileFilteredFound(object o, FileSystemObjectFilteredEventArgs e)
        {
            Console.WriteLine($"[Filtered] File {e.FileSystemObject.Name} was found.");
            var line = Console.ReadLine()?.ToLower();
            if (line == "skip")
            {
                e.Skip = true;
            }
        }

        static void OnDirFilteredFound(object o, FileSystemObjectFilteredEventArgs e)
        {
            Console.WriteLine($"[Filtered] Directory {e.FileSystemObject.Name} was found.");
        }

        static void OnFileFound(object o, FileSystemObjectFoundEventArgs e)
        {
            Console.WriteLine($"File {e.FileSystemObject.Name} was found.");
            var line = Console.ReadLine()?.ToLower();
            if (line == "stop")
            {
                e.Stop = true;
            }
        }

        static void OnDirFound(object o, FileSystemObjectFoundEventArgs e)
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