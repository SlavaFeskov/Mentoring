using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BCL.Rules.Abstractions;
using BCL.Services.Abstractions;

namespace BCL.Services
{
    public class WatchingService : IWatchingService
    {
        private readonly CancellationTokenSource _cancellationTokenSource;
        private readonly CancellationToken _token;
        private readonly IRuleValidator _ruleValidator;
        private readonly IFileWorker _fileWorker;
        private readonly IWatcher _watcher;

        public event EventHandler<FileWatcherEventArgs> FileAdded
        {
            add => _watcher.FileAdded += value;
            remove => _watcher.FileAdded -= value;
        }

        public event EventHandler<FileWatcherEventArgs> RuleFoundNotFound
        {
            add => _ruleValidator.RuleFoundNotFound += value;
            remove => _ruleValidator.RuleFoundNotFound -= value;
        }

        public event EventHandler<FileWatcherEventArgs> FileMove
        {
            add => _fileWorker.FileMove += value;
            remove => _fileWorker.FileMove -= value;
        }

        public WatchingService(IRuleValidator ruleValidator, IFileWorker fileWorker, IWatcher watcher)
        {
            _ruleValidator = ruleValidator;
            _fileWorker = fileWorker;
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