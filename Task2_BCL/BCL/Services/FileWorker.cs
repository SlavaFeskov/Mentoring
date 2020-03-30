using System;
using System.IO;
using BCL.Configuration;
using BCL.Configuration.Models;
using BCL.Resources;
using BCL.Services.Abstractions;

namespace BCL.Services
{
    public class FileWorker : IFileWorker
    {
        public event EventHandler<FileWatcherEventArgs> FileMove;

        public void Move(string sourcePath, string destPath)
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

        public string GetNewFilePath(string oldFilePath, TemplateElement rule)
        {
            var newFileName = Path.GetFileName(oldFilePath);
            var targetDirectory = rule?.DestinationDirectory ?? ConfigFactory.GetGeneralSection().Templates.DefaultDirectory;
            if (rule != null)
            {
                if (rule.AddIndex)
                {
                    var fileIndex =
                        new DirectoryInfo(targetDirectory).GetFiles().Length + 1;
                    newFileName = $"{fileIndex}_{newFileName}";
                }

                if (rule.AddDate)
                {
                    newFileName =
                        $"{Path.GetFileNameWithoutExtension(newFileName)}_" +
                        $"{DateTime.Now.ToString(rule.DateFormat)}{Path.GetExtension(newFileName)}";
                }
            }

            return Path.Combine(targetDirectory, newFileName);
        }

        protected virtual void OnFileMoved(FileWatcherEventArgs e)
        {
            FileMove?.Invoke(this, e);
        }
    }
}