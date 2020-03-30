using System;
using BCL.Configuration.Models;

namespace BCL.Services.Abstractions
{
    public interface IFileWorker
    {
        event EventHandler<FileWatcherEventArgs> FileMove;

        void Move(string sourcePath, string destPath);

        string GetNewFilePath(string oldFilePath, TemplateElement rule);
    }
}