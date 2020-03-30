using System;
using System.IO;
using FileSystemVisitorLib_V2.EventArgs;
using FileSystemVisitorLib_V2.Models;
using FileSystemVisitorLib_V2.Services.Abstractions;

// ReSharper disable IdentifierTypo

namespace FileSystemVisitorLib_V2.Services
{
    public class FileSystemObjectFiltrator : IFiltrator<FileSystemInfo>
    {
        public event EventHandler<FileSystemObjectFilteredEventArgs<FileSystemInfo>> FileFilteredFound;

        public event EventHandler<FileSystemObjectFilteredEventArgs<FileSystemInfo>> DirectoryFilteredFound;

        private FileSystemObjectFilteredEventArgs<FileSystemInfo> FireFileSystemObjectFilteredEvent(
            BaseObject<FileSystemInfo> fileSystemObject)
        {
            var fileSystemObjectFilteredEventArgs =
                new FileSystemObjectFilteredEventArgs<FileSystemInfo>(fileSystemObject);
            if (fileSystemObject.Type == FileSystemObjectType.Directory)
            {
                OnDirectoryFilteredFound(fileSystemObjectFilteredEventArgs);
            }
            else
            {
                OnFileFilteredFound(fileSystemObjectFilteredEventArgs);
            }

            return fileSystemObjectFilteredEventArgs;
        }

        public Func<BaseObject<FileSystemInfo>, bool> Filter { get; set; }

        public bool PerformFiltering(BaseObject<FileSystemInfo> fileSystemObject)
        {
            if (Filter != null)
            {
                if (Filter(fileSystemObject))
                {
                    var filteredEventArgs = FireFileSystemObjectFilteredEvent(fileSystemObject);
                    return filteredEventArgs.Skip;
                }

                return true;
            }

            return false;
        }

        protected virtual void OnFileFilteredFound(FileSystemObjectFilteredEventArgs<FileSystemInfo> e)
        {
            FileFilteredFound?.Invoke(this, e);
        }

        protected virtual void OnDirectoryFilteredFound(FileSystemObjectFilteredEventArgs<FileSystemInfo> e)
        {
            DirectoryFilteredFound?.Invoke(this, e);
        }
    }
}