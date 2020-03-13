using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using FileSystemVisitorLib_V2.EventArgs;

namespace FileSystemVisitorLib_V2
{
    public class FileSystemVisitor2
    {
        private FileSystemObjectFoundEventArgs FireFileSystemObjectFoundEvent(FileSystemInfo fileSystemObject)
        {
            var fileSystemObjectFoundEventArgs = new FileSystemObjectFoundEventArgs(fileSystemObject);
            if (fileSystemObject is DirectoryInfo)
            {
                OnDirectoryFound(fileSystemObjectFoundEventArgs);
            }
            else
            {
                OnFileFound(fileSystemObjectFoundEventArgs);
            }

            return fileSystemObjectFoundEventArgs;
        }

        private FileSystemObjectFilteredEventArgs FireFileSystemObjectFilteredEvent(FileSystemInfo fileSystemObject)
        {
            var fileSystemObjectFilteredEventArgs = new FileSystemObjectFilteredEventArgs(fileSystemObject);
            if (fileSystemObject is DirectoryInfo)
            {
                OnDirectoryFilteredFound(fileSystemObjectFilteredEventArgs);
            }
            else
            {
                OnFileFilteredFound(fileSystemObjectFilteredEventArgs);
            }

            return fileSystemObjectFilteredEventArgs;
        }

        public delegate bool FilterDelegate(FileSystemInfo fileSystemInfo);

        public event EventHandler<FileSystemVisitorEventArgs> Start;

        public event EventHandler<FileSystemVisitorEventArgs> Finish;

        public event EventHandler<FileSystemObjectFoundEventArgs> FileFound;

        public event EventHandler<FileSystemObjectFoundEventArgs> DirectoryFound;

        public event EventHandler<FileSystemObjectFilteredEventArgs> FileFilteredFound;

        public event EventHandler<FileSystemObjectFilteredEventArgs> DirectoryFilteredFound;

        public FilterDelegate Filter { get; set; }

        public FileSystemVisitor2(FilterDelegate filter)
        {
            Filter = filter;
        }

        public FileSystemVisitor2()
        {
        }

        public IEnumerable<FileSystemInfo> GetFileSystemObjects(string path)
        {
            if (!Directory.Exists(path))
            {
                throw new DirectoryNotFoundException();
            }

            OnStart(new FileSystemVisitorEventArgs());

            var directoryInfo = new DirectoryInfo(path);
            foreach (var fileSystemObject in GetFileSystemObjects(directoryInfo))
            {
                yield return fileSystemObject;
            }

            OnFinish(new FileSystemVisitorEventArgs());
        }

        private IEnumerable<FileSystemInfo> GetFileSystemObjects(DirectoryInfo currentDirectoryInfo)
        {
            foreach (var fileSystemObject in currentDirectoryInfo.GetFileSystemInfos())
            {
                var foundEventArgs = FireFileSystemObjectFoundEvent(fileSystemObject);
                if (foundEventArgs.Stop)
                {
                    yield break;
                }

                if (Filter != null)
                {
                    if (Filter(fileSystemObject))
                    {
                        var filteredEventArgs = FireFileSystemObjectFilteredEvent(fileSystemObject);
                        if (!filteredEventArgs.Skip)
                        {
                            foreach (var fso in GetFileSystemObjects(fileSystemObject))
                            {
                                yield return fso;
                            }
                        }
                    }
                    else
                    {
                        if (fileSystemObject is DirectoryInfo)
                        {
                            foreach (var fso in GetFileSystemObjects(fileSystemObject, true))
                            {
                                yield return fso;
                            }
                        }
                    }
                }
                else
                {
                    foreach (var fso in GetFileSystemObjects(fileSystemObject))
                    {
                        yield return fso;
                    }
                }
            }
        }

        private IEnumerable<FileSystemInfo> GetFileSystemObjects(FileSystemInfo fileSystemObject, bool skipDir = false)
        {
            if (fileSystemObject is DirectoryInfo)
            {
                if (!skipDir)
                {
                    yield return fileSystemObject;
                }

                foreach (var fileSystemInfo in GetFileSystemObjects(
                    new DirectoryInfo(fileSystemObject.FullName)))
                {
                    yield return fileSystemInfo;
                }
            }
            else
            {
                yield return fileSystemObject;
            }
        }

        protected virtual void OnStart(FileSystemVisitorEventArgs e)
        {
            Start?.Invoke(this, e);
        }

        protected virtual void OnFinish(FileSystemVisitorEventArgs e)
        {
            Finish?.Invoke(this, e);
        }

        protected virtual void OnFileFound(FileSystemObjectFoundEventArgs e)
        {
            FileFound?.Invoke(this, e);
        }

        protected virtual void OnDirectoryFound(FileSystemObjectFoundEventArgs e)
        {
            DirectoryFound?.Invoke(this, e);
        }

        protected virtual void OnFileFilteredFound(FileSystemObjectFilteredEventArgs e)
        {
            FileFilteredFound?.Invoke(this, e);
        }

        protected virtual void OnDirectoryFilteredFound(FileSystemObjectFilteredEventArgs e)
        {
            DirectoryFilteredFound?.Invoke(this, e);
        }
    }
}