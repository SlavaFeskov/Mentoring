using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Autofac;
using Bogus;
using FileSystemVisitorLib_V2.Container;
using FileSystemVisitorLib_V2.EventArgs;
using FileSystemVisitorLib_V2.Models;
using FileSystemVisitorLib_V2.Services;
using FileSystemVisitorLib_V2.Services.Abstractions;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class TestForVisitor
    {
        private DirectoryInfo CurrentDir { get; set; }
        private List<FileSystemInfo> _fileSystemItems;
        private readonly IContainer _container = ContainerFactory.GetContainer();

        private string GenerateDirName(int length = 7)
        {
            return new Faker().Random.AlphaNumeric(length);
        }

        private string GenerateFileName()
        {
            return new Faker().System.FileName();
        }

        private string CreateDir(string path)
        {
            var dirPath = Path.Combine(path, GenerateDirName());
            Directory.CreateDirectory(dirPath);
            return dirPath;
        }

        private List<string> CreateFilesInDir(string path, int amount = 5)
        {
            var filePaths = new List<string>();
            for (var i = 0; i < amount; i++)
            {
                var filePath = Path.Combine(path, GenerateFileName());
                filePaths.Add(filePath);
                File.Create(filePath).Close();
            }

            return filePaths;
        }

        [OneTimeSetUp]
        public void SetUp()
        {
            _fileSystemItems = new List<FileSystemInfo>();
            var currentDir = Directory.GetCurrentDirectory();
            var newDirPath = CreateDir(currentDir);
            CurrentDir = new DirectoryInfo(newDirPath);
            CreateFilesInDir(newDirPath, 10);
            _fileSystemItems.AddRange(new DirectoryInfo(newDirPath).GetFiles());
            CreateDir(newDirPath);
            newDirPath = CreateDir(newDirPath);
            _fileSystemItems.AddRange(CurrentDir.GetDirectories());
            CreateFilesInDir(newDirPath);
            _fileSystemItems.AddRange(new DirectoryInfo(newDirPath).GetFiles());
        }

        [Test]
        public void VisitorShowsAllItemsInTheInCurrentDirectoryAndSubdirectories()
        {
            var visitor = _container.Resolve<IFileSystemVisitor<FileSystemInfo>>(new NamedParameter("filter", null));
            var foundObjects = visitor.GetFileSystemObjects(CurrentDir.FullName).Select(f => f.Info.FullName).ToList();
            Console.WriteLine(string.Join("\r\n", foundObjects));
            CollectionAssert.AreEquivalent(_fileSystemItems.Select(f => f.FullName),
                foundObjects,
                $"Visitor returned wrong items in {CurrentDir} directory and subdirectories.");
        }

        [Test]
        public void VisitorFilterTest()
        {
            var visitor = _container.Resolve<IFileSystemVisitor<FileSystemInfo>>(new NamedParameter("filter",
                new Func<BaseObject<FileSystemInfo>, bool>(f => f.Name.Length > 8)));
            var expectedObjects = _fileSystemItems.Where(f => f.Name.Length > 8).Select(f => f.FullName).ToList();
            var actualObjects = visitor.GetFileSystemObjects(CurrentDir.FullName).Select(f => f.Info.FullName).ToList();
            CollectionAssert.AreEquivalent(expectedObjects, actualObjects,
                $"Visitor filtered wrong items in {CurrentDir} directory and subdirectories.");
        }

        [Test]
        public void FileSystemVisitorStartEndEventTest()
        {
            var visitor = _container.Resolve<IFileSystemVisitor<FileSystemInfo>>(new NamedParameter("filter", null));
            var startTriggered = false;
            var endTriggered = false;
            EventHandler<FileSystemVisitorEventArgs> startEventHandler = (o, e) => { startTriggered = true; };
            EventHandler<FileSystemVisitorEventArgs> endEventHandler = (o, e) => { endTriggered = true; };
            visitor.Start += startEventHandler;
            visitor.Finish += endEventHandler;

            visitor.GetFileSystemObjects(CurrentDir.FullName).First();
            Assert.True(startTriggered, "Start event wasn't fired in the beginning of the search.");
            Assert.False(endTriggered, "End event was fired in the beginning of the search.");
            startTriggered = false;

            visitor.GetFileSystemObjects(CurrentDir.FullName).Last();
            Assert.True(startTriggered, "Start event was fired in the end.");
            Assert.True(endTriggered, "End event wasn't fired in the end.");
        }

        [Test]
        public void FileSystemObjectFileDirectoryFoundEventTest()
        {
            var visitor = _container.Resolve<IFileSystemVisitor<FileSystemInfo>>(new NamedParameter("filter", null));
            var fileFoundCounter = 0;
            var directoryFoundCounter = 0;
            EventHandler<FileSystemObjectFoundEventArgs<FileSystemInfo>> fileFoundEventHandler = (o, e) =>
            {
                fileFoundCounter++;
            };
            EventHandler<FileSystemObjectFoundEventArgs<FileSystemInfo>> directoryFoundEventHandler = (o, e) =>
            {
                directoryFoundCounter++;
            };
            visitor.FileFound += fileFoundEventHandler;
            visitor.DirectoryFound += directoryFoundEventHandler;

            visitor.GetFileSystemObjects(CurrentDir.FullName).ToList();
            var expectedFileFoundCounter = _fileSystemItems.Count(f => f is FileInfo);
            var expectedDirectoryFoundCounter = _fileSystemItems.Count(f => f is DirectoryInfo);

            Assert.AreEqual(expectedFileFoundCounter, fileFoundCounter,
                "File found event was fired wrong amount of times.");
            Assert.AreEqual(expectedDirectoryFoundCounter, directoryFoundCounter,
                "Directory found event was fired wrong amount of times.");
        }

        [Test]
        public void FileSystemObjectFileDirectoryFilteredEventTest()
        {
            bool Filter(FileSystemInfo f) => f.Name.Contains("x");
            var visitor = _container.Resolve<IFileSystemVisitor<FileSystemInfo>>(new NamedParameter("filter",
                new Func<BaseObject<FileSystemInfo>, bool>(f => f.Name.Contains("x"))));
            var fileFilteredCounter = 0;
            var directoryFilteredCounter = 0;
            EventHandler<FileSystemObjectFilteredEventArgs<FileSystemInfo>> fileFilteredEventHandler = (o, e) =>
            {
                fileFilteredCounter++;
            };
            EventHandler<FileSystemObjectFilteredEventArgs<FileSystemInfo>> directoryFilteredEventHandler = (o, e) =>
            {
                directoryFilteredCounter++;
            };
            visitor.FileFilteredFound += fileFilteredEventHandler;
            visitor.DirectoryFilteredFound += directoryFilteredEventHandler;

            visitor.GetFileSystemObjects(CurrentDir.FullName).ToList();
            var expectedFileFoundCounter = _fileSystemItems.Count(f => f is FileInfo && Filter(f));
            var expectedDirectoryFoundCounter = _fileSystemItems.Count(f => f is DirectoryInfo && Filter(f));

            Assert.AreEqual(expectedFileFoundCounter, fileFilteredCounter,
                "File filtered event was fired wrong amount of times.");
            Assert.AreEqual(expectedDirectoryFoundCounter, directoryFilteredCounter,
                "Directory filtered event was fired wrong amount of times.");
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            Directory.Delete(CurrentDir.FullName, true);
        }
    }
}