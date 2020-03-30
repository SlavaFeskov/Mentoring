using System;
using System.Collections.Generic;
using System.IO;
using FileSystemVisitorLib_V2.EventArgs;
using FileSystemVisitorLib_V2.Models;
using FileSystemVisitorLib_V2.Services.Abstractions;

// ReSharper disable IdentifierTypo

namespace FileSystemVisitorLib_V2.Services
{
    public class FileSystemVisitor2 : IFileSystemVisitor<FileSystemInfo>
    {
        private readonly IFileSystemObjectsProvider<FileSystemInfo> _fileSystemObjectsProvider;
        private readonly IFiltrator<FileSystemInfo> _filtrator;

        public FileSystemVisitor2(
            IFileSystemObjectsProvider<FileSystemInfo> fileSystemObjectsProvider,
            IFiltrator<FileSystemInfo> filtrator, Func<BaseObject<FileSystemInfo>, bool> filter = null)
        {
            _fileSystemObjectsProvider = fileSystemObjectsProvider;
            _filtrator = filtrator;
            if (filter != null)
            {
                _filtrator.Filter = filter;
            }
        }

        #region events

        public event EventHandler<FileSystemVisitorEventArgs> Start;

        public event EventHandler<FileSystemVisitorEventArgs> Finish;

        public event EventHandler<FileSystemObjectFoundEventArgs<FileSystemInfo>> FileFound;

        public event EventHandler<FileSystemObjectFoundEventArgs<FileSystemInfo>> DirectoryFound;

        public event EventHandler<FileSystemObjectFilteredEventArgs<FileSystemInfo>> FileFilteredFound
        {
            add => _filtrator.FileFilteredFound += value;
            remove => _filtrator.FileFilteredFound -= value;
        }

        public event EventHandler<FileSystemObjectFilteredEventArgs<FileSystemInfo>> DirectoryFilteredFound
        {
            add => _filtrator.DirectoryFilteredFound += value;
            remove => _filtrator.DirectoryFilteredFound -= value;
        }

        #endregion

        #region eventInvokers

        protected virtual void OnStart(FileSystemVisitorEventArgs e)
        {
            Start?.Invoke(this, e);
        }

        protected virtual void OnFinish(FileSystemVisitorEventArgs e)
        {
            Finish?.Invoke(this, e);
        }

        protected virtual void OnFileFound(FileSystemObjectFoundEventArgs<FileSystemInfo> e)
        {
            FileFound?.Invoke(this, e);
        }

        protected virtual void OnDirectoryFound(FileSystemObjectFoundEventArgs<FileSystemInfo> e)
        {
            DirectoryFound?.Invoke(this, e);
        }

        #endregion

        private FileSystemObjectFoundEventArgs<FileSystemInfo> FireFileSystemObjectFoundEvent(
            BaseObject<FileSystemInfo> fileSystemObject)
        {
            var fileSystemObjectFoundEventArgs =
                new FileSystemObjectFoundEventArgs<FileSystemInfo>(fileSystemObject);
            if (fileSystemObject.Type == FileSystemObjectType.Directory)
            {
                OnDirectoryFound(fileSystemObjectFoundEventArgs);
            }
            else
            {
                OnFileFound(fileSystemObjectFoundEventArgs);
            }

            return fileSystemObjectFoundEventArgs;
        }

        public IEnumerable<BaseObject<FileSystemInfo>> GetFileSystemObjects(string path)
        {
            OnStart(new FileSystemVisitorEventArgs());

            foreach (var fileSystemObject in IterateFileSystemObjects(path))
            {
                yield return fileSystemObject;
            }

            OnFinish(new FileSystemVisitorEventArgs());
        }

        private IEnumerable<BaseObject<FileSystemInfo>> IterateFileSystemObjects(string path)
        {
            foreach (var fileSystemObject in _fileSystemObjectsProvider.GetFileSystemObjects(path))
            {
                var foundEventArgs = FireFileSystemObjectFoundEvent(fileSystemObject);
                if (foundEventArgs.Stop)
                {
                    yield break;
                }

                if (!_filtrator.PerformFiltering(fileSystemObject))
                {
                    yield return fileSystemObject;
                }

                if (fileSystemObject.Type == FileSystemObjectType.Directory)
                {
                    foreach (var fso in IterateFileSystemObjects(fileSystemObject.Path))
                    {
                        yield return fso;
                    }
                }
            }
        }
    }
}