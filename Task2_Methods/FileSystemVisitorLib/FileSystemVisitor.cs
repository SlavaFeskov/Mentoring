using System;
using System.Collections.Generic;
using System.IO;

namespace FileSystemVisitorLib
{
    public class FileSystemVisitor
    {
        public delegate bool FilterDelegate(FileSystemInfo fileSystemInfo);

        public event EventHandler<FileSystemVisitorEventArgs> Start;

        public event EventHandler<FileSystemVisitorEventArgs> Finish;

        public event EventHandler<FileSystemVisitorEventArgs> FileFound;

        public event EventHandler<FileSystemVisitorEventArgs> DirectoryFound;

        public event EventHandler<FileSystemVisitorEventArgs> FileFilteredFound;

        public event EventHandler<FileSystemVisitorEventArgs> DirectoryFilteredFound;

        public FilterDelegate Filter { get; set; }

        public FileSystemVisitor(FilterDelegate filter)
        {
            Filter = filter;
        }

        public FileSystemVisitor() { }

        public IEnumerable<FileSystemInfo> GetFileSystemItems(string path)
        {
            if (!Directory.Exists(path))
            {
                throw new DirectoryNotFoundException();
            }

            OnStart(new FileSystemVisitorEventArgs());

            var temp = GetFsInfos(path);

            OnFinish(new FileSystemVisitorEventArgs());

            return temp;
        }

        private IEnumerable<FileSystemInfo> GetFsInfos(string path)
        {
            var rootDirectory = new DirectoryInfo(path);
            foreach (var fileSystemInfo in rootDirectory.GetFileSystemInfos())
            {
                if (fileSystemInfo is FileInfo)
                {
                    OnFileFound(new FileSystemVisitorEventArgs(fileSystemInfo));
                }

                if (fileSystemInfo is DirectoryInfo)
                {
                    OnDirectoryFound(new FileSystemVisitorEventArgs(fileSystemInfo));
                    foreach (var fileSystemItem in GetFsInfos(fileSystemInfo.FullName))
                        yield return fileSystemItem;
                }

                if (Filter != null)
                {
                    if (Filter(fileSystemInfo))
                    {
                        if (fileSystemInfo is FileInfo)
                        {
                            OnFileFilteredFound(new FileSystemVisitorEventArgs(fileSystemInfo));
                        }

                        if (fileSystemInfo is DirectoryInfo)
                        {
                            OnDirectoryFilteredFound(new FileSystemVisitorEventArgs(fileSystemInfo));
                        }

                        yield return fileSystemInfo;
                    }
                }
                else
                {
                    yield return fileSystemInfo;
                }
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

        protected virtual void OnFileFound(FileSystemVisitorEventArgs e)
        {
            FileFound?.Invoke(this, e);
        }

        protected virtual void OnDirectoryFound(FileSystemVisitorEventArgs e)
        {
            DirectoryFound?.Invoke(this, e);
        }

        protected virtual void OnFileFilteredFound(FileSystemVisitorEventArgs e)
        {
            FileFilteredFound?.Invoke(this, e);
        }

        protected virtual void OnDirectoryFilteredFound(FileSystemVisitorEventArgs e)
        {
            DirectoryFilteredFound?.Invoke(this, e);
        }
    }
}