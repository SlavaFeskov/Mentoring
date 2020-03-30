using System;
using System.IO;
using System.Threading;
using BCL.Resources;
using BCL.Rules.Abstractions;
using BCL.Services.Abstractions;

namespace BCL.Services
{
    public class FileSystemWatcher : IWatcher
    {
        private readonly IRuleValidator _ruleValidator;
        private readonly IFileWorker _fileWorker;

        public FileSystemWatcher(IRuleValidator ruleValidator, IFileWorker fileWorker)
        {
            _ruleValidator = ruleValidator;
            _fileWorker = fileWorker;
        }

        public event EventHandler<FileWatcherEventArgs> FileAdded;

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

        public void Watch(string path, CancellationToken token)
        {
            using (var fsWatcher = new System.IO.FileSystemWatcher())
            {
                fsWatcher.Path = path;
                fsWatcher.Created += FsWatcher_Created;
                fsWatcher.EnableRaisingEvents = true;
                while (!token.IsCancellationRequested)
                {
                }
            }
        }

        private void FsWatcher_Created(object sender, FileSystemEventArgs e)
        {
            OnFileAdded(new FileWatcherEventArgs
                {Message = string.Format(StringResources.FileWasAdded, e.Name, Path.GetDirectoryName(e.FullPath))});

            var ruleToApply = _ruleValidator.CheckForRuleToApply(e.Name);
            var newFilePath = _fileWorker.GetNewFilePath(e.FullPath, ruleToApply);
            _fileWorker.Move(e.FullPath, newFilePath);
        }

        protected virtual void OnFileAdded(FileWatcherEventArgs e)
        {
            FileAdded?.Invoke(this, e);
        }
    }
}