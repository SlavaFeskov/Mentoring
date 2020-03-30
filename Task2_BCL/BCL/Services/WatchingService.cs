using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BCL.Services.Abstractions;

namespace BCL.Services
{
    public class WatchingService : IWatchingService
    {
        private readonly CancellationTokenSource _cancellationTokenSource;
        private readonly CancellationToken _token;
        private readonly IWatcher _watcher;

        public event EventHandler<FileWatcherEventArgs> FileAdded
        {
            add => _watcher.FileAdded += value;
            remove => _watcher.FileAdded -= value;
        }

        public event EventHandler<FileWatcherEventArgs> RuleFoundNotFound
        {
            add => _watcher.RuleFoundNotFound += value;
            remove => _watcher.RuleFoundNotFound -= value;
        }

        public event EventHandler<FileWatcherEventArgs> FileMove
        {
            add => _watcher.FileMove += value;
            remove => _watcher.FileMove -= value;
        }

        public WatchingService(IWatcher watcher)
        {
            _watcher = watcher;
            _cancellationTokenSource = new CancellationTokenSource();
            _token = _cancellationTokenSource.Token;
        }

        public void Interrupt()
        {
            _cancellationTokenSource.Cancel();
        }

        public List<Task> Watch(List<string> paths)
        {
            var tasks = new List<Task>();
            foreach (var path in paths)
            {
                tasks.Add(Task.Run(() => _watcher.Watch(path, _token), _token));
            }

            return tasks;
        }
    }
}