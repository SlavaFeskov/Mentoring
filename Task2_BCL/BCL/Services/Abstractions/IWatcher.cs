using System;
using System.Threading;

namespace BCL.Services.Abstractions
{
    public interface IWatcher
    {
        event EventHandler<FileWatcherEventArgs> FileAdded;

        event EventHandler<FileWatcherEventArgs> RuleFoundNotFound;

        event EventHandler<FileWatcherEventArgs> FileMove;

        void Watch(string path, CancellationToken token);
    }
}