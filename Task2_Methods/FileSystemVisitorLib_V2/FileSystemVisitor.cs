using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FileSystemVisitorLib_V2.EventArgs;

namespace FileSystemVisitorLib_V2
{
    public class FileSystemVisitor : IEnumerable<FileSystemInfo>
    {
        private readonly string _entryPoint;

        public bool Cancel
        {
            get => Enumerator.Cancel;
            set => Enumerator.Cancel = value;
        }

        public bool SkipCurrentElement
        {
            get => Enumerator.SkipCurrentElement;
            set => Enumerator.SkipCurrentElement = value;
        }

        public event EventHandler<FileSystemVisitorEventArgs> Start
        {
            add => Enumerator.Start += value;
            remove => Enumerator.Start -= value;
        }

        public event EventHandler<FileSystemVisitorEventArgs> Finish
        {
            add => Enumerator.Finish += value;
            remove => Enumerator.Finish -= value;
        }

        public event EventHandler<FileSystemVisitorEventArgs> FileFound
        {
            add => Enumerator.FileFound += value;
            remove => Enumerator.FileFound -= value;
        }

        public event EventHandler<FileSystemVisitorEventArgs> DirectoryFound
        {
            add => Enumerator.DirectoryFound += value;
            remove => Enumerator.DirectoryFound -= value;
        }

        public event EventHandler<FileSystemVisitorEventArgs> FileFilteredFound
        {
            add => Enumerator.FileFilteredFound += value;
            remove => Enumerator.FileFilteredFound -= value;
        }

        public event EventHandler<FileSystemVisitorEventArgs> DirectoryFilteredFound
        {
            add => Enumerator.DirectoryFilteredFound += value;
            remove => Enumerator.DirectoryFilteredFound -= value;
        }

        public delegate bool FilterDelegate(FileSystemInfo fileSystemInfo);

        public FileSystemVisitor(FilterDelegate filter, string entryPoint)
        {
            Enumerator.Filter = filter;
            _entryPoint = entryPoint;
        }

        public FileSystemVisitor(string entryPoint)
        {
            _entryPoint = entryPoint;
        }

        public IEnumerator<FileSystemInfo> GetEnumerator()
        {
            return new Enumerator(_entryPoint);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public sealed class Enumerator : IEnumerator<FileSystemInfo>
        {
            private readonly IReadOnlyCollection<FileSystemInfo> _fileSystemInfos;
            private int _counter = -1;

            public static bool Cancel { get; set; }
            public static bool SkipCurrentElement { get; set; }

            public static event EventHandler<FileSystemVisitorEventArgs> Start;

            public static event EventHandler<FileSystemVisitorEventArgs> Finish;

            public static event EventHandler<FileSystemVisitorEventArgs> FileFound;

            public static event EventHandler<FileSystemVisitorEventArgs> DirectoryFound;

            public static event EventHandler<FileSystemVisitorEventArgs> FileFilteredFound;

            public static event EventHandler<FileSystemVisitorEventArgs> DirectoryFilteredFound;

            public static FilterDelegate Filter { get; set; }

            public Enumerator(string entryPoint)
            {
                _fileSystemInfos = GetFileSystemInfos(entryPoint).ToList();
            }

            private IEnumerable<FileSystemInfo> GetFileSystemInfos(string path)
            {
                var dirInfo = new DirectoryInfo(path);
                foreach (var fileSystemInfo in dirInfo.GetFileSystemInfos())
                {
                    if (fileSystemInfo is DirectoryInfo)
                    {
                        yield return fileSystemInfo;
                        foreach (var fsi in GetFileSystemInfos(fileSystemInfo.FullName))
                        {
                            yield return fsi;
                        }
                    }
                    else
                    {
                        yield return fileSystemInfo;
                    }
                }
            }

            public bool MoveNext()
            {
                _counter++;
                if (Cancel)
                {
                    _counter = _fileSystemInfos.Count;
                }

                if (_counter == 0)
                {
                    OnStart(new FileSystemVisitorEventArgs());
                }

                if (_counter < _fileSystemInfos.Count)
                {
                    var current = _fileSystemInfos.ElementAt(_counter);
                    if (current is FileInfo)
                    {
                        OnFileFound(new FileSystemVisitorEventArgs(current));
                    }

                    if (current is DirectoryInfo)
                    {
                        OnDirectoryFound(new FileSystemVisitorEventArgs(current));
                    }

                    if (Filter != null)
                    {
                        if (Filter(current))
                        {
                            if (current is FileInfo)
                            {
                                OnFileFilteredFound(new FileSystemVisitorEventArgs(current));
                            }

                            if (current is DirectoryInfo)
                            {
                                OnDirectoryFilteredFound(new FileSystemVisitorEventArgs(current));
                            }
                        }
                        else
                        {
                            MoveNext();
                        }
                    }

                    if (SkipCurrentElement)
                    {
                        SkipCurrentElement = false;
                        MoveNext();
                    }

                    Current = current;
                    return true;
                }

                OnFinish(new FileSystemVisitorEventArgs());
                return false;
            }

            public FileSystemInfo Current { get; private set; }

            object IEnumerator.Current => Current;

            public void Dispose()
            {
            }

            public void Reset()
            {
                throw new System.NotImplementedException();
            }

            private void OnStart(FileSystemVisitorEventArgs e)
            {
                Start?.Invoke(this, e);
            }

            private void OnFinish(FileSystemVisitorEventArgs e)
            {
                Finish?.Invoke(this, e);
            }

            private void OnFileFound(FileSystemVisitorEventArgs e)
            {
                FileFound?.Invoke(this, e);
            }

            private void OnDirectoryFound(FileSystemVisitorEventArgs e)
            {
                DirectoryFound?.Invoke(this, e);
            }

            private void OnFileFilteredFound(FileSystemVisitorEventArgs e)
            {
                FileFilteredFound?.Invoke(this, e);
            }

            private void OnDirectoryFilteredFound(FileSystemVisitorEventArgs e)
            {
                DirectoryFilteredFound?.Invoke(this, e);
            }
        }
    }
}