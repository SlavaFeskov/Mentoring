using System;
using System.Collections.Generic;
using System.IO;
using FileSystemVisitorLib_V2.EventArgs;
using FileSystemVisitorLib_V2.Models;

namespace FileSystemVisitorLib_V2.Services.Abstractions
{
    public interface IFileSystemVisitor<TObject>
    {
        event EventHandler<FileSystemVisitorEventArgs> Start;
        event EventHandler<FileSystemVisitorEventArgs> Finish;
        event EventHandler<FileSystemObjectFoundEventArgs<FileSystemInfo>> FileFound;
        event EventHandler<FileSystemObjectFoundEventArgs<FileSystemInfo>> DirectoryFound;
        event EventHandler<FileSystemObjectFilteredEventArgs<FileSystemInfo>> FileFilteredFound;
        event EventHandler<FileSystemObjectFilteredEventArgs<FileSystemInfo>> DirectoryFilteredFound;

        IEnumerable<BaseObject<TObject>> GetFileSystemObjects(string path);
    }
}