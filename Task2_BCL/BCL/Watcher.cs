using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using BCL.Resources;

namespace BCL
{
    public class Watcher
    {
        private readonly string _defaultFolderPath;

        private readonly object _lockObject = new object();

        private readonly CancellationTokenSource _cancellationTokenSource;

        private readonly CancellationToken _token;

        public event EventHandler<FileWatcherEventArgs> FileAdded;

        public event EventHandler<FileWatcherEventArgs> RuleFoundNotFound;

        public event EventHandler<FileWatcherEventArgs> FileMove;

        public Watcher()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            _token = _cancellationTokenSource.Token;
            _defaultFolderPath = Directory.GetCurrentDirectory();
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
                tasks.Add(Task.Run(() => RunWatcher(path, _token), _token));
            }

            return tasks;
        }

        private void RunWatcher(string path, CancellationToken token)
        {
            using (var fsWatcher = new FileSystemWatcher())
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
            var ruleToApply = RuleTracker.CheckRulesForFile(e.Name);
            string destinationFolder = _defaultFolderPath;
            if (ruleToApply != null)
            {
                OnRuleFoundNotFound(new FileWatcherEventArgs
                    {Message = string.Format(StringResources.RuleWasFound, ruleToApply, e.Name)});
                destinationFolder = ruleToApply.DestinationDirectory;
            }
            else
            {
                OnRuleFoundNotFound(new FileWatcherEventArgs
                    {Message = string.Format(StringResources.RuleWasNotFound, e.Name)});
            }

            lock (_lockObject)
            {
                var newFileName = e.Name;
                if (ruleToApply != null)
                {
                    if (ruleToApply.AddIndex)
                    {
                        var fileIndex = new DirectoryInfo(destinationFolder).GetFiles().Length + 1;
                        newFileName = $"{fileIndex}_{newFileName}";
                    }

                    if (ruleToApply.AddDate)
                    {
                        newFileName =
                            $"{Path.GetFileNameWithoutExtension(newFileName)}_" +
                            $"{DateTime.Now.ToString(ruleToApply.DateFormat)}{Path.GetExtension(newFileName)}";
                    }
                }

                MoveFile(e.FullPath, Path.Combine(destinationFolder, newFileName));
            }
        }

        private void MoveFile(string sourcePath, string destPath)
        {
            if (File.Exists(destPath))
            {
                OnFileMoved(new FileWatcherEventArgs
                {
                    Message = string.Format(StringResources.FileAlreadyExists, Path.GetFileName(destPath),
                        Path.GetDirectoryName(destPath))
                });
                return;
            }

            File.Move(sourcePath, destPath);

            OnFileMoved(new FileWatcherEventArgs
            {
                Message = string.Format(StringResources.FileWasMoved, Path.GetFileName(sourcePath), sourcePath,
                    destPath)
            });
        }

        protected virtual void OnFileAdded(FileWatcherEventArgs e)
        {
            FileAdded?.Invoke(this, e);
        }

        protected virtual void OnRuleFoundNotFound(FileWatcherEventArgs e)
        {
            RuleFoundNotFound?.Invoke(this, e);
        }

        protected virtual void OnFileMoved(FileWatcherEventArgs e)
        {
            FileMove?.Invoke(this, e);
        }
    }
}