using System;
using FileSystemVisitorLib_V2.EventArgs;
using FileSystemVisitorLib_V2.Models;

// ReSharper disable IdentifierTypo

namespace FileSystemVisitorLib_V2.Services.Abstractions
{
    public interface IFiltrator<TObject>
    {
        event EventHandler<FileSystemObjectFilteredEventArgs<TObject>> FileFilteredFound;
        event EventHandler<FileSystemObjectFilteredEventArgs<TObject>> DirectoryFilteredFound;

        Func<BaseObject<TObject>, bool> Filter { get; set; }

        bool PerformFiltering(BaseObject<TObject> fileSystemObject);
    }
}