using System;
using System.Threading;

namespace BCL.Services.Abstractions
{
    public interface IWatcher
    {
        event EventHandler<FileWatcherEventArgs> FileAdded;

        void Watch(string path, CancellationToken token);
    }
}