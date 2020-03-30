using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BCL.Services.Abstractions
{
    public interface IWatchingService
    {
        event EventHandler<FileWatcherEventArgs> FileAdded;

        event EventHandler<FileWatcherEventArgs> RuleFoundNotFound;

        event EventHandler<FileWatcherEventArgs> FileMove;

        List<Task> Watch(List<string> paths);

        void Interrupt();
    }
}